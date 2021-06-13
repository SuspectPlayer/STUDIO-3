using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LoginManager : MonoBehaviourPunCallbacks
{
    #region Unity Metods

    public TMP_InputField playerNameInputField;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region UI Callback Methods

    public void ConnecteToPhotonServer()
    {
        if(playerNameInputField != null)
        {
            PhotonNetwork.NickName = playerNameInputField.text;
            PhotonNetwork.ConnectUsingSettings();
        }
        
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
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinedLobby");
    }

    #endregion
}
