//Written by Jack
using UnityEngine;
using Photon.Pun;

public class CameraAssign : MonoBehaviour
{
    public GameObject cam;
    public AudioListener audioListener;
    PhotonView photonView;
    //Used to turn on certain components just for the player who owns the controller as so the other player cant interact with them
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            cam.SetActive(true);
            cam.GetComponent<Camera>().enabled = true;
            audioListener.enabled = true;
        }
    }
}
