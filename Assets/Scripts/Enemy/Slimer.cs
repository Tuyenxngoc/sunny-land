using UnityEngine;

public class Slimer : Enemy
{
    public Transform player;
    public float moveSpeed = 2f;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (IsPlayerInBoundary())
        {
            HandleMove(player.position);
        }
        else if(Vector2.Distance(transform.position, originalPosition) > 1)
        {
            HandleMove(originalPosition);
        }

        m_Animator.SetFloat("Speed", Mathf.Abs(m_Rigidbody2D.velocity.x));
    }

    private void HandleMove(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        m_Rigidbody2D.velocity = new Vector2(direction.x * moveSpeed, m_Rigidbody2D.velocity.y);
        FlipCharacter(direction.x);
    }

    bool IsPlayerInBoundary()
    {
        return player.position.x >= left.position.x && player.position.x <= right.position.x;
    }
}
