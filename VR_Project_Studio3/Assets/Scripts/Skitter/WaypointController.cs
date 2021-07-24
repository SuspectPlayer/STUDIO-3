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

    public float fieldOfViewAngle = 100f;
    public float losePlayerTimer = 5;
    bool losingPlayer;
    bool playerFound;
    public GameObject player;

    public GameObject redLight;
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
        if (!playerFound)
        {
            float distance = Vector3.Distance(transform.position, targetWaypoint.position);
            CheckDistanceToWaypoint(distance);
        }
        else
        {
            agent.SetDestination(player.transform.position);
        }
        
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

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfViewAngle * 0.5f)
            {
                Debug.Log("playerFound");
                
                playerFound = true;
                redLight.SetActive(true);
                //Put something here to play when the player is found
                if (losingPlayer)
                {
                    StopCoroutine("LosePlayer");
                    losingPlayer = false;
                }
            }
            else
            {
                StartCoroutine("LosePlayer");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine("LosePlayer");
        
    }

    IEnumerator LosePlayer()
    {
        losingPlayer = true;
        yield return new WaitForSeconds(losePlayerTimer);
        playerFound = false;
        UpdateTargetWaypoint();
        redLight.SetActive(false);
    }
}
