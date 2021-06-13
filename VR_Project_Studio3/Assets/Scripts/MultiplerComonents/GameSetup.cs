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

    public void OnClickStartGame()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        

        PhotonNetwork.LoadLevel("TestJoiningScene");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TestJoiningScene")
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
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
        photonView.RPC("RPC_LoadGameOthers", RpcTarget.Others);
    }
    private void NonMasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.MasterClient);
    }

    [PunRPC]
    void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel("TestJoiningScene");
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
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Test XR Rig"), Vector3.up, Quaternion.identity);
        }
        else if (!isVRPlayer)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Test PC player"), Vector3.up, Quaternion.identity);
        }
    }
}
