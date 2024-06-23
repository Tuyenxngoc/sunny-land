using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy
{
    public float runSpeed;
    public bool isRunningLeft = true;

    void Update()
    {
        if (transform.position.x <= left.position.x)
        {
            isRunningLeft = false;
        }

        if (transform.position.x >= right.position.x)
        {
            isRunningLeft = true;
        }

    }

    private void FixedUpdate()
    {
        if (isRunningLeft)
        {
            m_Rigidbody2D.velocity = new Vector2(-runSpeed * 10f * Time.fixedDeltaTime, m_Rigidbody2D.velocity.y);
            FlipCharacter(1);
        }
        else
        {
            m_Rigidbody2D.velocity = new Vector2(runSpeed * 10f * Time.fixedDeltaTime, m_Rigidbody2D.velocity.y);
            FlipCharacter(-1);
        }

    }
}
