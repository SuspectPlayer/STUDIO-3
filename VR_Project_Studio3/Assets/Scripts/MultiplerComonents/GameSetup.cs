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
        

        PhotonNetwork.LoadLevel("Darcy Test Scene");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Darcy Test Scene")
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
        PhotonNetwork.LoadLevel("Darcy Test Scene");
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
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "First Person Controller"), new Vector3(18.798f, 4.5787f, -46.436f), Quaternion.identity);
        }
        else if (!isVRPlayer)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ViewerCam Variant"), new Vector3(110.442f, 38.952f, 46.117f), new Quaternion(19.463f, -90.986f, -0.443f, 0.6913717f));
        }
    }
}
