//Written by Jack
using System.IO;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameSetup : MonoBehaviourPunCallbacks
{
    public bool isVRPlayer;
    public bool isFlatScreen;

    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    //sets if the player is the diver or the intel
    public void SetIsVRPlayer()
    {
        isVRPlayer = true;
    }
    public void SetIsNotVRPlayer()
    {
        isVRPlayer = false;
    }

    //sets if the diver is flatscreen or not
    public void SetIsFlatScreen() { isFlatScreen = true; }
    public void SetIsNotFlatScreen() { isFlatScreen = false; }

    public void OnClickStartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        
        //loads the level for the hosting player
        PhotonNetwork.LoadLevel("Main_Level");
    }

    //starts the joining of the level after it has finished loading for both players
    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main_Level")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }
    //tells the none-hosting player to start joining the next level
    private void MasterLoadedGame()
    {
        photonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }
    //tells the master client (the host) to create all the players so there is only one instance of each player
    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
    }
    //loads the next level for the none hosting player
    [PunRPC]
    void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel("Main_Level");
    }
    //Creates the players for the game
    [PunRPC]
    void RPC_LoadedGameScene()
    {
        photonView.RPC("RPC_CreatePlayer", RpcTarget.All);
    }

    //Instantiates a player controller into both players games depending on the type of player each has chosen 
    [PunRPC]
    void RPC_CreatePlayer()
    {
        if (isVRPlayer && !isFlatScreen)
        { 
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "VR Player (XR Rig)"), new Vector3(29.1312f, 55.27f, 272.6982f), Quaternion.identity);
        }
        else if (!isVRPlayer)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ViewerCam Variant"), new Vector3(113.118f, 81.136f, 111.906f), new Quaternion(0.04510248f, 0.2137172f, -0.0101695f, 0.9758009f));
        }
        else if (isVRPlayer && isFlatScreen)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "First Person Controller"), new Vector3(29.1312f, 55.27f, 272.6982f), Quaternion.identity);
        }
    }
}
