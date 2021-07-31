using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class LobbySetup : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomCode;
    public TextMeshProUGUI roomNumber;
    public GameObject player1;
    public GameObject player2;
    public GameObject lobbySetupPannel;
    public GameObject lobbyPannel;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinRoom("Room Code: " + roomCode.text);
    }


    public void StartLobby()
    {
        string randomRoomName = "Room Code: " + Random.Range(1000, 9999);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;


        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

        player1.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
        if (FindObjectOfType<GameSetup>().isVRPlayer)
        {
            player1.transform.Find("Platform").GetComponent<TMP_Dropdown>().value = 1;
        }
        else
        {
            player1.transform.Find("Platform").GetComponent<TMP_Dropdown>().value = 0;
        }
        roomNumber.text = randomRoomName;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Unavalable");
    }

    public override void OnJoinedRoom()
    {
        lobbySetupPannel.SetActive(false);
        lobbyPannel.SetActive(true);
        if (!PhotonNetwork.IsMasterClient)
        {
            roomNumber.text = PhotonNetwork.CurrentRoom.Name;
            photonView.RPC("RPC_SetName", RpcTarget.Others, PhotonNetwork.NickName.ToString());
            player1.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
            player2.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.MasterClient.NickName;
            photonView.RPC("RPC_PlatformRequest", RpcTarget.Others);
            if (FindObjectOfType<GameSetup>().isVRPlayer)
            {
                player1.transform.Find("Platform").GetComponent<TMP_Dropdown>().value = 1;
                photonView.RPC("RPC_SetPlatform", RpcTarget.Others, 1);
            }
            else
            {
                player1.transform.Find("Platform").GetComponent<TMP_Dropdown>().value = 0;
                photonView.RPC("RPC_SetPlatform", RpcTarget.Others, 0);
            }
            
        }
       
    }


    [PunRPC]
    void RPC_SetName(string nickName)
    {
        player2.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = nickName;
    }

    [PunRPC]
    void RPC_SetPlatform(int platform)
    {
        player2.transform.Find("Platform").GetComponent<TMP_Dropdown>().value = platform;
    }

    [PunRPC]
    void RPC_PlatformRequest()
    {
        if (FindObjectOfType<GameSetup>().isVRPlayer)
        {
            photonView.RPC("RPC_SetPlatform", RpcTarget.Others, 1);
        }
        else
        {
            photonView.RPC("RPC_SetPlatform", RpcTarget.Others, 0);
        }
    }
}
