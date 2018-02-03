using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // Stuff to do with the enemy which is going to be about the enemy itself. Speed, health and that jazz
    public float enemyHealth = 10;



    public Transform[] path;
    public float speed = 0.1f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
    private Vector3 direction;
    // Color color = Color.red;

    private void Start()
    {
        Debug.Log(path.Length);
        Debug.Log(currentPoint);

        
    }
    // TO DO:
    // At some point, this will wanted to be changed to getting the parent of the waypoints, and using the children as the waypoints.
    // That way, I could change the system so that it can use infinate waypoints instead of adding them manually, just as long as they are in the parent game object.
    void Update()
    {
        // If the distance to the next waypoint is less than 0.1
        if (Vector3.Distance(path[currentPoint].position, transform.position) <= 0.1)
        {
            // and the waypoint is the last in the chain.
            if(currentPoint >= (path.Length - 1))
            {
                // Delete self and remove a life.
                GameController controller = GameObject.FindObjectOfType<GameController>();
                Debug.Log(controller.Lives);
                controller.Lives--;
                Debug.Log(controller.Lives);
                Destroy(gameObject);
            }
            // Otherwise find the next waypoint and begin moving to there.
            else
            {
                currentPoint++;
                Debug.Log(currentPoint);
                
            }
        }
        // After all the checks to see how we want to see where we want to move, move to that current waypoint.
        // That way, we only move if we have a current waypoint.
        direction = this.transform.position - path[currentPoint].position;
        transform.LookAt(path[currentPoint]);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Here, something has hit us, so we are going to take some damage.
    public void BeHit(float d)
    {
        enemyHealth = enemyHealth - d;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}
