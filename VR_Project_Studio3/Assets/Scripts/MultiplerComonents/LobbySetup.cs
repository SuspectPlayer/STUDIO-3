//Written by Jack
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

    //Creates the room (lobby) for the other player to join with the correct options
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

    //logs when the room is created
    public override void OnCreatedRoom()
    {
        Debug.Log("A room is created with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    //logs when failing to join room 
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Unavalable");
    }

    //calls when joining the room (lobby)
    public override void OnJoinedRoom()
    {
        lobbySetupPannel.SetActive(false);
        lobbyPannel.SetActive(true);
        //sets the information in the room for the joining player
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

    //sets the joining player name for the room host
    [PunRPC]
    void RPC_SetName(string nickName)
    {
        player2.transform.Find("Player Name").GetComponent<TextMeshProUGUI>().text = nickName;
    }

    //sets the joining player platfrom for the room host
    [PunRPC]
    void RPC_SetPlatform(int platform)
    {
        player2.transform.Find("Platform").GetComponent<TMP_Dropdown>().value = platform;
    }

    //sets the platform of the host to see for the joining player
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

    //For leaving a room and going back to the main menu
    public void CloseRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
