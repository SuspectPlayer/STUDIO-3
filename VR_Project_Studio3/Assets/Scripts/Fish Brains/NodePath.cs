using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Slight adaptation of "WaypointPath" from the "Poly Nature Series"
//Will add author of that pack here soon

public class NodePath : MonoBehaviour
{
    public float nodeViewSize;
    public Color pathColour = Color.cyan;
    Transform[] path;
    public List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmos()
    {
        path = GetComponentsInChildren<Transform>();
        Gizmos.color = pathColour;
        nodes.Clear();

        foreach (Transform node in path)
        {
            if (node != gameObject.transform)
            {
                nodes.Add(node);
            }
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            if (i > 0)
            {
                Vector3 currNode = nodes[i-1].position;
                Vector3 nextNode = nodes[i].position;

                Gizmos.DrawSphere(currNode, nodeViewSize);
                Gizmos.DrawLine(currNode, nextNode);
            }
        }
    }
}
