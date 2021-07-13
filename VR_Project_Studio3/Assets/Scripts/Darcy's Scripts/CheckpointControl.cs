using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover

public class CheckpointControl : MonoBehaviour
{
    [HideInInspector]
    public GameObject vrPlayer;

    [HideInInspector]
    public Vector3 lastCheckpointPos;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(vrPlayer == null) //assigning the vr aplyer once they have been instantiated
        {
            vrPlayer = GameObject.Find("First Person Controller(Clone)");
        }
        if(photonView == null)
        {
            Awake();
        }

        if(Input.GetKeyDown(KeyCode.Y))
        {
            LoadCheckpoint();
        }
    }

    public void LoadCheckpoint()
    {
        photonView.RPC("RPC_LoadCheckpoint", RpcTarget.All);
    }

    [PunRPC]
    void RPC_LoadCheckpoint()
    {
        vrPlayer.GetComponent<CharacterController>().enabled = false;
        vrPlayer.transform.position = lastCheckpointPos;
        vrPlayer.GetComponent<CharacterController>().enabled = true;


        Debug.Log("loaded");
    }
}
