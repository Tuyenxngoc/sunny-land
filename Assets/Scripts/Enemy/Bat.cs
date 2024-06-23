using UnityEngine;

public class Bat : Enemy
{
    public Transform player;
    public float flyingSpeed = 5f;
    public Vector2 boxMax;
    public Vector2 boxMin;

    private Vector3 originalPosition;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        if (player != null)
        {
            player.position = new Vector3(player.position.x, player.position.y + 0.9f, player.position.z);
        }
        originalPosition = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (IsPlayerInBoundary())
        {
            HandleFly(player.position);
        }
        else if (Vector2.Distance(transform.position, originalPosition) > 2)
        {
            HandleFly(originalPosition);
        }

        if (m_Rigidbody2D.velocity != Vector2.zero)
        {
            m_Animator.SetBool("IsFly", true);
            boxCollider.size = boxMin;
        }
        else
        {
            m_Animator.SetBool("IsFly", false);
            boxCollider.size = boxMax;
        }
    }

    bool IsPlayerInBoundary()
    {
        return player.position.x >= left.position.x &&
            player.position.x <= right.position.x &&
            Mathf.Abs(player.position.y - originalPosition.y) <= 5;
    }

    private void HandleFly(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        m_Rigidbody2D.velocity = new Vector2(direction.x * flyingSpeed, direction.y * flyingSpeed);
        FlipCharacter(direction.x);
    }
}
