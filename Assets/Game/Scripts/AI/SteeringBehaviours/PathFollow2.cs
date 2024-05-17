using UnityEngine;

//Good for patrolling
public class PathFollow2 : ArriveSteeringBehaviour
{
    public Transform[] waypoints; // Array of waypoints to follow
    private int currentWaypointIndex = 0; // Index of the current waypoint the agent is heading towards
    public float waypointThreshold = 1f; // Distance threshold to consider a waypoint reached


    public override Vector3 CalculateForce()
    {
        if (waypoints.Length == 0)
            return Vector3.zero;

        Vector3 force = Vector3.zero;

        // Calculate the steering force to follow the path
        force += FollowPath();

      

        return force;
    }

    Vector3 FollowPath()
    {
        if (currentWaypointIndex >= waypoints.Length)
        {
            // Reset to start of the waypoints array when the end is reached
            currentWaypointIndex = 0;
        }

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 toTarget = targetPosition - transform.position;

        // Check if the current waypoint has been reached
        if (toTarget.magnitude < waypointThreshold)
        {
            currentWaypointIndex++; // Move to the next waypoint
            if (currentWaypointIndex >= waypoints.Length)
            {
                // Ensure looping by resetting index and calculating force to first waypoint
                currentWaypointIndex = 0;
            }
            targetPosition = waypoints[currentWaypointIndex].position;
            toTarget = targetPosition - transform.position;
        }

        // Use the Arrive behavior to approach the current waypoint smoothly
        target = targetPosition; // Set the target for ArriveSteeringBehaviour
        return CalculateArriveForce(); // N
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

    }
}
