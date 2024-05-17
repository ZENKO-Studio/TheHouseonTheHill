using UnityEngine;
using UnityEngine.AI;

//Used with NavMesh
public class PathFollow : ArriveSteeringBehaviour
{
    public float waypointDistance = 0.5f; // Distance at which the waypoint is considered "reached"
    public int cornerIndex = 0;
    public int wayPointIndex = 0;
    private NavMeshPath path;
    public Transform[] waypoints; // The waypoints to follow

    public void Start()
    {
        path = new NavMeshPath();
        // Assuming the first Transform is the parent 'Waypoints' GameObject itself and should be skipped



        NavMesh.CalculatePath(transform.position, waypoints[wayPointIndex].position, NavMesh.AllAreas, path);


        target = waypoints[0].position;
    }

    public override Vector3 CalculateForce()
    {
        if (cornerIndex < path.corners.Length && (target - transform.position).magnitude < waypointDistance)
        {
            if (cornerIndex < path.corners.Length)
            {
                target = path.corners[cornerIndex];
                cornerIndex++;
            }
            //else if ((target - transform.position).magnitude < waypointDistance)
            //{
            //    if (cornerIndex >= path.corners.Length)
            //    {
            //        cornerIndex = 0;
            //        target = path.corners[cornerIndex];
            //        cornerIndex++;
            //    }

            //}
        }



        if ((waypoints[wayPointIndex].position - transform.position).magnitude < waypointDistance)
        {
            wayPointIndex++;
            cornerIndex = 0;
            NavMesh.CalculatePath(transform.position, waypoints[wayPointIndex].position, NavMesh.AllAreas, path);

            if (wayPointIndex >= waypoints.Length - 1)
            {
                wayPointIndex = 0;

                NavMesh.CalculatePath(transform.position, waypoints[wayPointIndex].position, NavMesh.AllAreas, path);
            }

        }
        return CalculateArriveForce();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        DebugExtension.DrawCircle(target, Color.magenta, waypointDistance);
        if (path != null && path.corners.Length > 0)
        {
            DebugExtension.DrawCircle(path.corners[path.corners.Length - 1], Color.magenta, 1f);

            for (int i = 1; i < path.corners.Length; i++)
            {
                Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.black);
                DebugExtension.DrawCircle(path.corners[i - 1], Color.magenta, 0.2f);
            }
        }
       
    }
}
