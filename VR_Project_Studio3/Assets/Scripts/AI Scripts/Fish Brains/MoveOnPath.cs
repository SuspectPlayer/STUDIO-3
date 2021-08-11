using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    SingleFlock pathFlock;

    public int startFromNode;
    [Space(10)]
    public NodePath designatedNodePath;
    [Space(10)]
    [HideInInspector]public int movingTowards = 0;
    [Space(10)]
    public float moveSpeed;
    public float distanceTillSwitch = 1.0f;
    public float rotationSpeed = 0.5f;

    void Awake()
    {
        StartFromNode();

        pathFlock = GetComponentInParent<SingleFlock>();
        pathFlock.pathLeader = gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        float distToDest = Vector3.Distance(designatedNodePath.nodes[movingTowards].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, designatedNodePath.nodes[movingTowards].position, Time.deltaTime * moveSpeed);

        Quaternion rotation = Quaternion.LookRotation(designatedNodePath.nodes[movingTowards].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if(distToDest <= distanceTillSwitch)
        {
            movingTowards++;
        }

        if (movingTowards >= designatedNodePath.nodes.Count)
        {
            movingTowards = 0;
        }
    }

    void StartFromNode()
    {
        movingTowards = startFromNode;
        transform.position = designatedNodePath.nodes[startFromNode].position;
    }
}
