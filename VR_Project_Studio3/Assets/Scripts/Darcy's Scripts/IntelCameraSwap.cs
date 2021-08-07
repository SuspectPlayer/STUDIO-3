using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

//Written by Darcy Glover

public class IntelCameraSwap : MonoBehaviour
{
    [SerializeField, Tooltip("The Virtual Camera used for FPC")]
    CinemachineVirtualCamera intelVCam;

    CinemachineVirtualCamera newVCam;

    Transform intelHead;

    [HideInInspector]
    public bool zoomedIn = false;

    void Start()
    {
        intelHead = transform;
        Debug.Log("Intel head is the " + transform.gameObject.name + " object");
    }

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(intelHead.position, Vector3.forward, out hit, 3f) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Raycast has hit: " + hit.transform.gameObject.name);
            if(!zoomedIn && hit.transform.gameObject.GetComponent<VirtualCameraAssign>())
            {
                Debug.Log("It has hit a cinemachine camera object");
                newVCam = hit.transform.gameObject.GetComponent<VirtualCameraAssign>().cam;
                int head = 0;
                int screen = 10;
                Debug.Log(head + " " + screen);
                SwapPriority(head, screen);
                zoomedIn = true;
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && zoomedIn)
        {
            int head = 10;
            int screen = 0;
            SwapPriority(head, screen);
            zoomedIn = false;
        }
    }

    void SwapPriority(int head, int screen)
    {
        Debug.Log("Attempting to swap priority");
        intelVCam.Priority = head;
        newVCam.Priority = screen;
    }
}
