using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using FMODUnity;


//Written by Darcy Glover

public class SkitterEventP3Collisions : MonoBehaviour
{
    PhotonView photonView;
    public StudioEventEmitter skitterMusic;
    public GameObject skitterCursor;
    [SerializeField]
    GameObject skitter;
    public Animator airbags;

    //[HideInInspector]
    public bool canTrigger = false;

    //this script is the collision detection to spawn the skitter and to end the event for puzzle 3

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if(photonView == null)
        {
            Start();
        }
    }

    public void TurnTriggerOn()
    {
        photonView.RPC("RPC_TurnTriggerOn", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnTriggerOn()
    {
        canTrigger = true;
    }

    [PunRPC]
    void RPC_Triggered()
    {
        Debug.Log("trigger " + PhotonNetwork.IsMasterClient.ToString());
        gameObject.SetActive(false); //turn off the collider to prevent from happening more than once
        skitter.GetComponent<SkitterEventP3>().SpawnSkitter();
        skitterMusic.Play();
        skitterCursor.SetActive(true);
        airbags.SetTrigger("skit");
    }


    void OnTriggerEnter(Collider other) //this collider is positioned across the entrance to the second room, which means the player will have to step through it.
    {
        if(canTrigger && other.CompareTag("Player"))
        {
            photonView.RPC("RPC_Triggered", RpcTarget.All);
        }
    }
}
