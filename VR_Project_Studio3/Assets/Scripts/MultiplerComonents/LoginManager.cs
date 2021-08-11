//Written by Jack
using UnityEngine;
using Photon.Pun;

public class LoginManager : MonoBehaviourPunCallbacks
{
    public GameObject[] disableOnConnect;
    public GameObject[] enableOnConnect;

    #region UI Callback Methods

    //connects the to photon sever once passed through the play fab authentication
    public static void ConnectToPhotonServer(string username)
    {
        PhotonNetwork.NickName = username;
        PhotonNetwork.ConnectUsingSettings();
        
    }

    #endregion

    #region Photon Callback Methods

    public override void OnConnected()
    {
        Debug.Log("The Server is Available");
    }

    //sets variables once connected to photon servers
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master Server with player name: " + PhotonNetwork.NickName);
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.JoinLobby(Photon.Realtime.TypedLobby.Default);
        foreach (GameObject g in disableOnConnect)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in enableOnConnect)
        {
            g.SetActive(true);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinedLobby");
    }

    #endregion
}
