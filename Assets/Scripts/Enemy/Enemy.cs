using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    public float deathEffectDuration = 0.5f;
    public bool facingRight = true;
    public Transform left;
    public Transform right;

    protected Animator m_Animator;
    protected Rigidbody2D m_Rigidbody2D;
    protected AudioSource deathSource;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        deathSource = GetComponent<AudioSource>();
    }

    public void JumpedOn()
    {
        Death();
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDuration);
        }
    }

    protected void Death()
    {
        Destroy(gameObject);
    }

    protected void FlipCharacter(float move)
    {
        if (move > 0 && !facingRight || move < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
