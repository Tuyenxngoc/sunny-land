using UnityEngine;

public class Frog : Enemy
{
    public LayerMask ground;
    public float jumpForce = 350f; // Lực nhảy của ếch
    public float jumpDelay = 4f;   // Độ trễ giữa các lần nhảy
    public float sightRange = 5f; // Phạm vi tầm nhìn của ếch
    public float maxJumpDistance = 3f; // Khoảng cách tối đa mà ếch có thể nhảy

    private bool m_Grounded = false;
    private bool m_PlayerInSight = false;
    private float m_JumpTimer = 0f;
    private float m_CenterPoint;
    private Transform player;
    private Collider2D m_Collider2D;

    private void Start()
    {
        m_Collider2D = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        m_CenterPoint = Mathf.Round((left.position.x + right.position.x) / 2);
    }

    private void Update()
    {
        m_PlayerInSight = Vector2.Distance(transform.position, player.position) <= sightRange;
        m_Grounded = m_Collider2D.IsTouchingLayers(ground);

        m_Animator.SetBool("IsFalling", m_Rigidbody2D.velocity.y < 0.1 && !m_Grounded);
        m_Animator.SetBool("IsJumping", m_Rigidbody2D.velocity.y > 0.1 && !m_Grounded);
    }

    private void FixedUpdate()
    {
        m_JumpTimer += Time.fixedDeltaTime;

        if (m_Grounded && m_PlayerInSight && m_JumpTimer >= jumpDelay)
        {
            HandleMovement();
            m_JumpTimer = 0f;
            m_Grounded = false;
        }
    }

    private void HandleMovement()
    {
        float currentXFrog = transform.position.x;
        float currentXPlayer = player.position.x;
        float jumpDistance = currentXPlayer - currentXFrog;

        if ((currentXFrog + jumpDistance < left.position.x) || (currentXFrog + jumpDistance > right.position.x))
        {
            jumpDistance = m_CenterPoint - currentXFrog;
        }

        if (Mathf.Abs(jumpDistance) > maxJumpDistance)
        {
            if (jumpDistance > 0)
            {
                jumpDistance = maxJumpDistance;
            }
            else
            {
                jumpDistance = -maxJumpDistance;
            }
        }

        Jump(jumpDistance);
        FlipCharacter(jumpDistance);
    }

    private void Jump(float jumpDistance)
    {
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x + jumpDistance, 0f);
        m_Rigidbody2D.AddForce(Vector2.up * jumpForce);
    }
}