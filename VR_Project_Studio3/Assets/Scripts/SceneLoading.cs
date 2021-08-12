//Written by Jack
using System.IO;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    GameSetup setup;
    PhotonView photonView;
    public string sceneName;
    public GameObject loadingScreen;
    public Vector3 diverPos;
    public Vector3 intelPos;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading2;
    }

    private void Start()
    {
        setup = FindObjectOfType<GameSetup>();
    }

    //Needs to be triggered by only one player
    public void LoadScene()
    {
        StartCoroutine(WaitChanges());     
    }

    IEnumerator WaitChanges()
    {
        yield return new WaitForSeconds(5f);

        photonView.RPC("RPC_TurnOnLoadingChanges", RpcTarget.All);
        StartCoroutine(WaitToLoad());
    }


    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(10f);

        //Begins loading the next scene
        photonView.RPC("RPC_LoadGameOthers2", RpcTarget.All);
    }


    //Changes to the scene when the game starts loading
    [PunRPC]
    void RPC_TurnOnLoadingChanges()
    {
        if(loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }
    }

    //starts the joining of the level after it has finished loading for both players
    private void OnSceneFinishedLoading2(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == sceneName)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                //MasterLoadedGame2();
            }
            else
            {
                NonMasterLoadedGame2();
            }
        }
    }
    //tells the none-hosting player to start joining the next level
    private void MasterLoadedGame2()
    {
        photonView.RPC("RPC_LoadGameOthers2", RpcTarget.Others);
    }
    //tells the master client (the host) to create all the players so there is only one instance of each player
    private void NonMasterLoadedGame2()
    {
        photonView.RPC("RPC_LoadedGameScene2", RpcTarget.MasterClient);
    }
    //loads the next level for the none hosting player
    [PunRPC]
    void RPC_LoadGameOthers2()
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    //Creates the players for the game
    [PunRPC]
    void RPC_LoadedGameScene2()
    {
        photonView.RPC("RPC_CreatePlayer2", RpcTarget.All);
    }

    //Instantiates a player controller into both players games depending on the type of player each has chosen 
    [PunRPC]
    void RPC_CreatePlayer2()
    {
        if (setup.isVRPlayer && !setup.isFlatScreen)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "VR Player (XR Rig)"), diverPos, Quaternion.identity);
        }
        else if (!setup.isVRPlayer)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "Intel First Person Controller"), intelPos, Quaternion.identity);
        }
        else if (setup.isVRPlayer && setup.isFlatScreen)
        {
            PhotonNetwork.Instantiate(Path.Combine("Prefabs", "First Person Controller"), diverPos, Quaternion.identity);
        }
    }
}
