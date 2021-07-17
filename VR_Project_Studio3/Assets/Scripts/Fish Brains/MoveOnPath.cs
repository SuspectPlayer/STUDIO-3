using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPath : MonoBehaviour
{
    public NodePath designatedNodePath;
    [Space(10)]
    public int movingTowards = 0;
    public float moveSpeed;
    public float distanceTillSwitch = 1.0f;
    public float rotationSpeed = 0.5f;


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
}
