//Written by Jack
using UnityEngine;
using Photon.Pun;

public class CameraAssign : MonoBehaviour
{
    public Camera cam;
    public AudioListener audioListener;
    PhotonView photonView;
    //Used to turn on certain components just for the player who owns the controller as so the other player cant interact with them
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            cam.enabled = true;
            audioListener.enabled = true;
        }
    }
}
