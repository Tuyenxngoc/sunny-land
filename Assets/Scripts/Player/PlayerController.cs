using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 550f;
    [SerializeField] private float hurtForce = 25f;
    [SerializeField] private float hurtDelay = 1f; // Duration of delay when hurt
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Transform m_SpawnPoint;
    [SerializeField] private Collider2D m_CrouchDisableCollider;

    private const float k_GroundedRadius = .03f;
    private const float k_CeilingRadius = .03f;
    private bool m_Grounded;
    private bool m_OnLadder;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private enum State { idle, running, jumping, falling, hurt, climb };
    private State state = State.idle;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        m_FacingRight = (transform.localScale.x > 0);
        transform.position = m_SpawnPoint.position;
    }

    private void Update()
    {
        //Kiểm tra nhân vật có đang trên mặt đất hay không
        m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);

        UpdateState();
        SetAnimation();
    }

    private void UpdateState()
    {
        if (state == State.jumping)
        {
            if (m_Rigidbody2D.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (m_Grounded)
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(m_Rigidbody2D.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(m_Rigidbody2D.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void SetAnimation()
    {
        m_Animator.SetBool("IsFalling", state == State.falling);
        m_Animator.SetBool("IsJumping", state == State.jumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !m_Animator.GetBool("IsHurt"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce/2));

                //Tăng số lần giết quái trong game
                GameController.Instance.enemiesKilled++;
            }
            else
            {
                state = State.hurt;
                m_Animator.SetBool("IsHurt", true);
                if (collision.gameObject.transform.position.x > transform.position.x) // enemy is to my right therefore i should be damaged and move left
                {
                    m_Rigidbody2D.velocity = new Vector2(-hurtForce, m_Rigidbody2D.velocity.y);
                }
                else // enemy is to my left therefore i should be damaged and move right 
                {
                    m_Rigidbody2D.velocity = new Vector2(hurtForce, m_Rigidbody2D.velocity.y);
                }
                GameController.Instance.SubtractHeart(1);
                StartCoroutine(HurtDelay());
            }
        }
    }

    private IEnumerator HurtDelay()
    {
        yield return new WaitForSeconds(hurtDelay);
        m_Animator.SetBool("IsHurt", false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            m_OnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            m_OnLadder = false;
            m_Animator.SetBool("IsClimbing", false);
        }
    }

    public void Move(Vector2 move, bool jump, bool crouch)
    {
        if (m_Animator.GetBool("IsHurt"))
        {
            move.x = 0;
            jump = false;
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(move.x));

        if (m_OnLadder)
        {
            m_Animator.SetBool("IsClimbing", (Mathf.Abs(m_Rigidbody2D.velocity.y) > 0.1f));
            HandleLadderMovement(move.y);
            MoveCharacter(move.x);
        }
        else
        {
            HandleCrouch(ref move.x, crouch);
            MoveCharacter(move.x);
            FlipCharacter(move.x);
            HandleJump(jump);
        }
    }
    private void HandleLadderMovement(float verticalMove)
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, verticalMove * 10f);
    }

    private void HandleCrouch(ref float move, bool crouch)
    {
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround) && m_Grounded)
            {
                crouch = true;
            }
        }

        if (crouch)
        {
            move *= m_CrouchSpeed;

            if (m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = false;
        }
        else
        {
            if (m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = true;
        }

        m_Animator.SetBool("IsCrouching", crouch);
    }

    private void MoveCharacter(float horizontalMove)
    {
        Vector3 targetVelocity = new Vector2(horizontalMove * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void FlipCharacter(float move)
    {
        if (move > 0 && !m_FacingRight || move < 0 && m_FacingRight)
        {
            Flip();
        }
    }

    private void HandleJump(bool jump)
    {
        bool isCeilingAbove = Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
        bool shouldJump = m_Grounded && jump && !isCeilingAbove;

        if (shouldJump)
        {
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            state = State.jumping;
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void ResetPosition()
    {
        transform.position = m_SpawnPoint.position;
    }
    public void ResetPosition(Transform dropPoint)
    {
        transform.position = dropPoint.position;
    }

    public void SetAnimation(string name, bool value)
    {
        m_Animator.SetBool(name, value);
        if (name == "IsHurt")
        {
            StartCoroutine(HurtDelay());
        }
    }
}
