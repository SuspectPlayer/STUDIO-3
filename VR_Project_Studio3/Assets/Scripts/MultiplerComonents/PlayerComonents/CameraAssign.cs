using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraAssign : MonoBehaviour
{
    public GameObject cam;
    public AudioListener audioListener;
    PhotonView photonView;
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            cam.SetActive(true);
            audioListener.enabled = true;
        }
    }
}
