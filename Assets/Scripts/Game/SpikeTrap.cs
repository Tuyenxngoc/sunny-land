using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public float fallSpeed = 5f;
    public LayerMask playerLayer;
    private bool isFalling = false;

    void Update()
    {
        // Kiểm tra nếu người chơi đang ở dưới SpikeTrap
        if (!isFalling)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer);
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {

                isFalling = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isFalling)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Gây sát thương cho người chơi
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetAnimation("IsHurt", true);
                GameController.Instance.SubtractHeart(1);
            }
            // Hủy SpikeTrap sau khi gây sát thương
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            // Hủy SpikeTrap khi chạm đất
            Destroy(gameObject);
        }
    }
}
