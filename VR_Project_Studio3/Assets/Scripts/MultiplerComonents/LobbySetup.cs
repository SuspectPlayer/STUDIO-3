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
        //DontDestroyOnLoad(this);
    }

    public void VRJoinLobby()
    {
        PhotonNetwork.JoinRoom("Room_" + roomCode.text);
    }

    public void PCJoinLobby()
    {
        PhotonNetwork.JoinRoom("Room_" + roomCode.text);
        player1.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
    }

    public void VRStartLobby()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);
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

    //public void PCStartLobby()
    //{
    //    string randomRoomName = "Room_" + Random.Range(0, 10000);
    //    RoomOptions roomOptions = new RoomOptions();
    //    roomOptions.IsOpen = true;
    //    roomOptions.IsVisible = true;
    //    roomOptions.MaxPlayers = 2;


    //    PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    //    player1.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
    //    roomNumber.text = randomRoomName;
    //}

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
            photonView.RPC("RPC_SetName", RpcTarget.Others, PhotonNetwork.NickName.ToString());
            player1.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
            player2.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.MasterClient.NickName;

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


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (roomList.Count == 2)
        {
            if (player2.activeSelf == false)
            {

            }

        }
        //photonView.RPC("RPC_SetName", RpcTarget.Others/*, PhotonNetwork.NickName.ToString()*/);
    }

    //public override void OnJoinedLobby()
    //{
    //    photonView.RPC("RPC_SetName", RpcTarget.Others, PhotonNetwork.NickName.ToString());
    //}


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
}
