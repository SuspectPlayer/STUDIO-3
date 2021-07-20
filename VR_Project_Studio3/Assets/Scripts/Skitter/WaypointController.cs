using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointController : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    Transform targetWaypoint;
    int targetWaypointIndex = 0;
    public float minDistance = 2f;
    int lastWaypointIndex;

    public float moveSpeed = 5f;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        targetWaypoint = waypoints[targetWaypointIndex];
        lastWaypointIndex = waypoints.Count - 1;
        agent.SetDestination(targetWaypoint.position);
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        //Debug.Log(distance);
        CheckDistanceToWaypoint(distance);
    }

    void CheckDistanceToWaypoint(float currentDistance)
    {
        if(currentDistance <= minDistance)
        {
            targetWaypointIndex += 1;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint()
    {
        if(targetWaypointIndex > lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }
        targetWaypoint = waypoints[targetWaypointIndex];
        agent.SetDestination(targetWaypoint.position);
    }
}
