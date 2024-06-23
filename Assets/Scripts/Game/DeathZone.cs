using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                if (respawnPoint)
                {
                    player.ResetPosition(respawnPoint);
                }
                else
                {
                    player.ResetPosition();
                }
                player.SetAnimation("IsHurt", true);
                GameController.Instance.SubtractHeart(1);
            }
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
