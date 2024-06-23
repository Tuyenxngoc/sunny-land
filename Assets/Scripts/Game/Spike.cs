using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private Transform dropPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Gây sát thương cho người chơi
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetAnimation("IsHurt", true);
                GameController.Instance.SubtractHeart(1);

                if (dropPoint)
                {
                    player.ResetPosition(dropPoint);
                }
            }
        }
    }
}
