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

    public void VRJoinLobby()
    {
        PhotonNetwork.JoinRoom("Room_" + roomCode);
    }

    public void PCJoinLobby()
    {
        PhotonNetwork.JoinRoom("Room_" + roomCode);
        player2.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
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
        roomNumber.text = randomRoomName;
    }

    public void PCStartLobby()
    {
        string randomRoomName = "Room_" + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;


        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

        player1.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.NickName;
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
    }

    //public override void OnRoomListUpdate(List<RoomInfo> roomList)
    //{
    //    if(roomList.Count == 2)
    //    {
    //        if(player2.activeSelf == false)
    //        {
    //            player2.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = PhotonNetwork.PlayerList.;
    //        }
            
    //    }
    //}
}
