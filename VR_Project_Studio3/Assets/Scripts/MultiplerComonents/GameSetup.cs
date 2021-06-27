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
        

        PhotonNetwork.LoadLevel("Darcy_Abyss_Greybox");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Darcy_Abyss_Greybox")
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
        PhotonNetwork.LoadLevel("Darcy_Abyss_Greybox");
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
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "First Person Controller"), new Vector3(29.74253f, 54.87f, 272.5221f), Quaternion.identity);
        }
        else if (!isVRPlayer)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "ViewerCam Variant"), new Vector3(31.72129f, 88.86247f, 232.3835f), new Quaternion(-0.1970741f, 0.01013365f, 0.001838408f, -0.9803345f));
        }
    }
}
