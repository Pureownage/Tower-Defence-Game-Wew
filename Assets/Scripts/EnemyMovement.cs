using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // Stuff to do with the enemy which is going to be about the enemy itself. Speed, health and that jazz
    public float enemyHealth = 10;

    GameObject waypoints;
    Transform targetWaypointNode;
    int waypointsIndex = 0;

    public Transform[] path;
    public float speed = 0.1f;
    public float reachDist = 1.0f;
    public int currentPoint = 0;
    public Vector3 direction;
    // Color color = Color.red;

    private void Start()
    {
        waypoints = GameObject.Find("Waypoints");
        GetNextWayPointsNode();
    }
    // Here, we use our list of waypoints and get the next waypoints position.
    void GetNextWayPointsNode()
    {
        // If the waypoints is less than the child count, we move onwards
        if(waypointsIndex < waypoints.transform.childCount)
        {
            targetWaypointNode = waypoints.transform.GetChild(waypointsIndex);
            waypointsIndex++;
        }
        else
        // If not, then we set targetWaypointNode to null, as we have no more waypoints to go to. The movement code in the update funciton deals with this.
        {
            targetWaypointNode = null;
        }
            
        
    }
    // TO DO:
    // At some point, this will wanted to be changed to getting the parent of the waypoints, and using the children as the waypoints.
    // That way, I could change the system so that it can use infinate waypoints instead of adding them manually, just as long as they are in the parent game object.

    // Okay so I'm going to need to use the system described above. To spawn enemys they need to have the waypoints added in at a core, which I can't do my way.
    //
    void Update()
    {
        // If the distance to the next waypoint is less than 0.1
        if (Vector3.Distance(targetWaypointNode.position, transform.position) <= 0.1)
        {
            // Then we have reached the end of our path, and we need a new node.
            GetNextWayPointsNode();
            // If the node we are attempting to reach is null, then we have reached the end of the path and need to deal with that.
            if (targetWaypointNode == null)
            {
                // Delete self and remove a life.
                GameController controller = GameObject.FindObjectOfType<GameController>();
                controller.Lives--;
                Destroy(gameObject);
                return;
            }
        }
        // After all the checks to see how we want to see where we want to move, move to that current waypoint.
        // That way, we only move if we have a current waypoint, and we need to move to it.
        direction = this.transform.position - targetWaypointNode.position;
        transform.LookAt(targetWaypointNode);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Here, something has hit us, so we are going to take some damage.
    public void BeHit(float d)
    {
        enemyHealth -= d;
        // If the damage we take, takes us below 0, we destory ourselves.
        if (enemyHealth <= 0)
        {
            GameController controller = GameObject.FindObjectOfType<GameController>();
            controller.money = controller.money + 20;
            Destroy(gameObject);
        }
    }

}
