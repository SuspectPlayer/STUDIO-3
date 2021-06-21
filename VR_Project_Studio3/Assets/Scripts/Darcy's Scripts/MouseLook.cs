using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 150f;

    public Transform playerBody;

    float xRotation = 0f;

    PhotonView photonview;

    void Awake()
    {
        photonview = GetComponentInParent<PhotonView>();
    }

    void Start()
    {
        if (photonview.IsMine)
        {
           Cursor.lockState = CursorLockMode.Locked;
        }

    }


    void Update()
    {
        if(photonview.IsMine)
        {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //locks the player to only be able to look forward and not snap their characters neck

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
