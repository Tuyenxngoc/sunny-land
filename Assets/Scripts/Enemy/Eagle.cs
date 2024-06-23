using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : Enemy
{
    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if ((player == null))
        {
            return;
        }
        if ((chase))
        {
            Chase();
        }
        else
        {
            ReturnStartPoint();
        }
        Flip();
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed*Time.deltaTime); 
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
        {
            //change speed,shoot, animation
        }
        else
        {
            //reset vaiable
        }
    }
    private void Flip()
    {
        if(transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

}
