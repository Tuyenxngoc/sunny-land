using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChaseController : Eagle
{
    // Start is called before the first frame update
    public Eagle[] enemyArray;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var enemy in enemyArray)
            {
                enemy.chase = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (var enemy in enemyArray)
            {
                enemy.chase = false;
            }
        }
    }
}
