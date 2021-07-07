using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

//Written by Darcy Glover

public class CheckpointControl : MonoBehaviour
{
    static CheckpointControl instance;

    [SerializeField]
    GameObject vrPlayer;

    public Vector3 lastCheckpointPos;

    PhotonView photonView;

    void Awake()
    {
        if (instance == null) //need to keep checkpoint control in between scene loads
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject); //making sure there arent multiple checkpoint controller
        }
    }

    void Update()
    {
        if(vrPlayer == null) //assigning the vr aplyer once they have been instantiated
        {
            vrPlayer = GameObject.Find("First Person Controller(Clone)");
        }
    }

    public void LoadCheckpoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        vrPlayer.transform.position = lastCheckpointPos;
        //photonView.RPC("RPC_LoadCheckpoint", RpcTarget.All, lastCheckpointPos);
    }

    [PunRPC]
    void RPC_LoadCheckpoint(Vector3 lastCheckpointPos)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        vrPlayer.transform.position = lastCheckpointPos;
    }
}
