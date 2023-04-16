using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointsIndex = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool facingRight = true;


    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointsIndex].transform.position, transform.position) < 0.1f)
        {

            currentWaypointsIndex++;
            if (currentWaypointsIndex >= waypoints.Length)
            {

                currentWaypointsIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointsIndex].transform.position, Time.deltaTime * speed);
        if (gameObject.CompareTag("Enemy"))
        {
            if (transform.position.x < waypoints[currentWaypointsIndex].transform.position.x && facingRight)
            {
                Flip();
            }
            else if (transform.position.x > waypoints[currentWaypointsIndex].transform.position.x && !facingRight)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    
}
