using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
public class LoginManager : MonoBehaviourPunCallbacks
{
    public GameObject[] disableOnConnect;
    public GameObject[] enableOnConnect;

    #region UI Callback Methods

    public static void ConnecteToPhotonServer(string username)
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
