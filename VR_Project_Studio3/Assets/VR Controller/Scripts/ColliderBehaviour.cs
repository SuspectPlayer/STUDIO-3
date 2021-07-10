using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehaviour : MonoBehaviour
{
    public Transform head;
    public Transform feet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(head.position.x, feet.position.y, head.position.z);
        //collider.height = (head.position.y - feet.position.y) + heightOffset;
    }
}
