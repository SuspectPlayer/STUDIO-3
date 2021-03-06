using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseLook : MonoBehaviour
{
    public FPSUIOpener fpsUI;

    public float mouseSensitivity = 150f;

    public Transform playerBody;

    float xRotation = 0f;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponentInParent<PhotonView>();
    }

    void Update()
    {
        if(fpsUI == null)
        {
            fpsUI = GameObject.Find("UIHolder").GetComponent<FPSUIOpener>();
        }

        if (!FindObjectOfType<GameSetup>().isVRPlayer)
        {
            if (photonView.IsMine && !GetComponent<IntelCameraSwap>().zoomedIn)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (photonView.IsMine && GetComponent<IntelCameraSwap>().zoomedIn)
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (photonView.IsMine && !GetComponent<IntelCameraSwap>().zoomedIn)
            {
                float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -90f, 90f); //locks the player to only be able to look forward and not snap their characters neck

                transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                playerBody.Rotate(Vector3.up * mouseX);
            }
        }
        else if(FindObjectOfType<GameSetup>().isVRPlayer)
        {
            if (photonView.IsMine && !fpsUI.isTrue)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if(fpsUI.isTrue && photonView.IsMine)
            {
                Cursor.lockState = CursorLockMode.None;
            }    

            if (photonView.IsMine)
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
}
