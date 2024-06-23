using UnityEngine;

public class Cherry : MonoBehaviour
{
    public AudioClip pickupSound;
    public GameObject pickupEffect;
    public float pickupEffectDuration = 0.333f;
    private bool isPickedUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPickedUp) return;

        if (collision.CompareTag("Player"))
        {
            isPickedUp = true;
            Destroy(gameObject);

            if (pickupEffect != null)
            {
                GameObject effect = Instantiate(pickupEffect, transform.position, Quaternion.identity);
                Destroy(effect, pickupEffectDuration);
            }

            GameController.Instance.AddHeart(1);
            AudioController.Instance.PlayOneShot(pickupSound);
        }
    }
}
