using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraAssign : MonoBehaviour
{
    public GameObject cam;
    public Camera camera;
    public AudioListener audioListener;
    PhotonView photonView;
    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            cam.SetActive(true);
            camera.enabled = true;
            audioListener.enabled = true;
        }
    }
}
