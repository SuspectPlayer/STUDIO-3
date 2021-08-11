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

    PuzzleCompletionManager puzzleCompletionManager;

    LightManager lightManager;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        puzzleCompletionManager = FindObjectOfType<PuzzleCompletionManager>();
        lightManager = FindObjectOfType<LightManager>();
    }

    void Update()
    {
        if(vrPlayer == null) //assigning the vr aplyer once they have been instantiated
        {
            vrPlayer = GameObject.Find("VR Player (XR Rig)(Clone)");
            if (vrPlayer == null) 
            {
                vrPlayer = GameObject.Find("First Person Controller(Clone)"); //if its still unassigned, it means the player it using the first person controller
            }
        }

        if(photonView == null)
        {
            Awake();
        }
    }

    public void LoadCheckpoint()
    {
        photonView.RPC("RPC_LoadCheckpoint", RpcTarget.All);
    }

    [PunRPC]
    void RPC_LoadCheckpoint()
    {
        puzzleCompletionManager.PuzzleAttempt();

        lightManager.TurnOffAllLights();

        vrPlayer.transform.position = checkpoint.position;
                                                              
        skitter.GetComponent<SkitterEventP3>().playersLose = false; //resetting skitter event
        trigger.SetActive(true);

        Debug.Log("loaded " + PhotonNetwork.IsMasterClient.ToString());
    }
}
