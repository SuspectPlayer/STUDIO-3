using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class DoorControl : MonoBehaviour
{
    public GameObject door;

    public bool doorTwoLocked = false; //this bool is for puzzle 3, to deactivate the skitter if the door is locked

    PhotonView photonView;

    void Start()
    {
        door.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
        GameObject.Find("Door 3").GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
        photonView = GetComponent<PhotonView>();
    }

    public void LockDoor()
    {
        photonView.RPC("RPC_LockDoor", RpcTarget.All);
    }

    public void UnlockDoor()
    {
        photonView.RPC("RPC_UnlockDoor", RpcTarget.All);
    }

    [PunRPC]
    void RPC_LockDoor()
    {
        door.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.red); //the light for the door
        door.GetComponentInChildren<Animator>().SetBool("Unlock", false);
        if(door.name == "Door 2") //if it is the second door, will need to change the bool for puzzle 3
        {
            doorTwoLocked = true;
        }
    }

    [PunRPC]
    void RPC_UnlockDoor()
    {
        door.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
        door.GetComponentInChildren<Animator>().SetBool("Unlock", true);
        if (door.name == "Door 2") //if it is the second door, will need to change the bool for puzzle 3
        {
            doorTwoLocked = false;
        }
        if (door.name != "Door 3")
        {
            GetComponent<PuzzleManager>().whichPuzzle++;
            GetComponent<PuzzleManager>().ActivatePuzzle(); //activating the next puzzle, only if it isnt the last door
        }
    }
}
