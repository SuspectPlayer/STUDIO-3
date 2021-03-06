using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using Photon.Pun;

//Written by Darcy Glover

public class IntelCameraSwap : MonoBehaviour
{
    [SerializeField, Tooltip("The Virtual Camera used for FPC")]
    CinemachineVirtualCamera intelVCam;

    [SerializeField, Tooltip("The default Camera used for FPC")]
    Camera intelCam;

    [SerializeField, Tooltip("The text to prompt the player to press E or Esc")]
    GameObject pressText;

    CinemachineVirtualCamera newVCam;

    Camera newCam;

    GraphicRaycaster graphic;

    PhotonView photonView;

    //[HideInInspector]
    public bool zoomedIn = false;

    void Start()
    {
        if(!FindObjectOfType<GameSetup>().isVRPlayer)
        {
            pressText.SetActive(true);
        }
    }

    void Update()
    {
        if(photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        RaycastHit hit;

        if(photonView.IsMine && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f) && Input.GetKeyDown(KeyCode.E)) //pressing e to zoom in on a screen
        { 
            if(photonView.IsMine && !zoomedIn && hit.transform.gameObject.GetComponent<VirtualCameraAssign>()) //checking to see if the hit object has a virtual camera component
            {
                Debug.Log("It has hit a cinemachine camera object");
                pressText.SetActive(false);

                newVCam = hit.transform.gameObject.GetComponent<VirtualCameraAssign>().vCam;
                newCam = hit.transform.gameObject.GetComponent<VirtualCameraAssign>().cam;
                graphic = hit.transform.gameObject.GetComponentInChildren<GraphicRaycaster>();

                zoomedIn = true; //changed to false before the zoom in so that the player cant look around while zooming in
                StartCoroutine(ZoomIn());
            }
        }

        if(photonView.IsMine && Input.GetKeyDown(KeyCode.Escape) && zoomedIn) //pressing escape to zoom back out
        {
            StartCoroutine(ZoomOut());
        }
    }

    IEnumerator ZoomIn() //changes the priority of the virtual cameras to start the blending animation, waits for the blend to finish, and then changes to the assigned regular camera for canvas events
    {
        Debug.Log("Attempting to zoom in");

        intelVCam.Priority = 0;
        newVCam.Priority = 10;

        yield return new WaitForSeconds(2.2f);

        pressText.GetComponent<TextMeshProUGUI>().text = "Press ESC to exit";
        pressText.SetActive(true);

        newCam.enabled = true;
        intelCam.enabled = false;
        graphic.enabled = true;

        StopCoroutine(ZoomIn());
    }

    IEnumerator ZoomOut() //changes the priority of the virtual cameras to start the blending animation, waits for the blend to finish, and then changes back to the first person controller camera
    {
        Debug.Log("Attempting to zoom out");

        intelVCam.Priority = 10;
        newVCam.Priority = 0;

        intelCam.enabled = true;

        pressText.GetComponent<TextMeshProUGUI>().text = "Press E to interact with screens";

        yield return new WaitForSeconds(2.2f);

        newCam.enabled = false;
        graphic.enabled = false;

        zoomedIn = false; //changed to false after the zoom out so that the player cant look around while zooming out

        StopCoroutine(ZoomOut());
    }
}
