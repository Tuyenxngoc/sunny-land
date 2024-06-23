using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bunny : Enemy
{
    // Reference to waypoints
    public List<Transform> points;
    // The int value for next point index
    public int nextID;
    // The value that applies to ID for changing
    int idChangeValue = 1;
    public float speed = 2f;

    private void Reset()
    {
        Init();
    }

    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;

        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);

        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1");
        p1.transform.position = root.transform.position;
        p1.transform.SetParent(waypoints.transform);

        GameObject p2 = new GameObject("Point2");
        p2.transform.position = root.transform.position;
        p2.transform.SetParent(waypoints.transform);

        points = new List<Transform> { p1.transform, p2.transform };
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];

        if (goalPoint.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(4, 4, 1);
        }
        else
        {
            transform.localScale = new Vector3(-4, 4, 1);
        }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, goalPoint.position) < 1f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            if (nextID == 0)
                idChangeValue = 1;

            nextID += idChangeValue;
        }
    }
}
