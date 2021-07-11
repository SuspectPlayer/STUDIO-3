using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;

public class GameSetup : MonoBehaviourPunCallbacks
{
    public bool isVRPlayer;

    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public void SetIsVRPlayer()
    {
        isVRPlayer = true;
    }
    public void SetIsNotVRPlayer()
    {
        isVRPlayer = false;
    }

    public void OnClickStartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        

        PhotonNetwork.LoadLevel("Darcy_Main_Level");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Darcy_Main_Level")
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

    private void MasterLoadedGame()
    {
        //photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        photonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }
    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
    }

    [PunRPC]
    void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel("Darcy_Main_Level");
    }
    [PunRPC]
    void RPC_LoadedGameScene()
    {
        photonView.RPC("RPC_CreatePlayer", RpcTarget.All);
    }

    [PunRPC]
    void RPC_CreatePlayer()
    {
        //PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Test PC player"), Vector3.up, Quaternion.identity);
        if (isVRPlayer)
        { 
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "First Person Controller"), new Vector3(29.1312f, 55.27f, 272.6982f), Quaternion.identity);
        }
        else if (!isVRPlayer)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ViewerCam Variant"), new Vector3(113.9993f, 81.38101f, 111.2712f), new Quaternion(0.2233047f, 0.2072154f, -0.05212184f, 0.9510416f));
        }
    }
}
