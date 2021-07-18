using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover

public class CheckpointControl : MonoBehaviour
{
    //[HideInInspector]
    public GameObject vrPlayer;
    
    [SerializeField]
    Transform checkpoint;

    public GameObject skitter, trigger;

    //[HideInInspector]
    //public Vector3 lastCheckpointPos;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(vrPlayer == null) //assigning the vr aplyer once they have been instantiated
        {
            vrPlayer = GameObject.FindGameObjectWithTag("Player");
        }
        if(photonView == null)
        {
            Awake();
        }

        //if(Input.GetKeyDown(KeyCode.Y)) //dev tool
        //{
        //    LoadCheckpoint();
        //}
    }

    public void LoadCheckpoint()
    {
        photonView.RPC("RPC_LoadCheckpoint", RpcTarget.All);
    }

    [PunRPC]
    void RPC_LoadCheckpoint()
    {
        vrPlayer.GetComponent<CharacterController>().enabled = false; //moving the player back to the checkpoint
        vrPlayer.transform.position = checkpoint.position;
        vrPlayer.GetComponent<CharacterController>().enabled = true;
                                                              
        skitter.GetComponent<SkitterEventP3>().playersLose = false; //resetting skitter event
        trigger.SetActive(true);

        Debug.Log("loaded");
    }
}
