//Written by Jack
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;


public class VoiceChatToggle : MonoBehaviour
{
    PhotonView photonView;
    public Recorder recorder;
    
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    //should be called by one player
    public void ToggleChatOff()
    {
        photonView.RPC("RPC_ToggleChatOff", RpcTarget.All);
    }
    //should be called by one player
    public void ToggleChatOn()
    {
        photonView.RPC("RPC_ToggleChatOn", RpcTarget.All);
    }

    [PunRPC]
    void RPC_ToggleChatOff()
    {
        recorder.TransmitEnabled = false;
    }

    [PunRPC]
    void RPC_ToggleChatOn()
    {
        recorder.TransmitEnabled = true;
    }
}
