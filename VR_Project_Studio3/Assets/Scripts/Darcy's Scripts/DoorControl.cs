using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using FMODUnity;


//Written by Darcy Glover

public class DoorControl : MonoBehaviour
{
    public GameObject door;

    public bool doorTwoLocked = false; //this bool is for puzzle 3, to deactivate the skitter if the door is locked

    PhotonView photonView;
    
    void Start()
    {
        door.GetComponent<DoorLamp>().lampy.SetLamp(false, Color.white, false);
        photonView = GetComponent<PhotonView>();
    }

    public void LockDoor()
    {
        photonView.RPC("RPC_LockDoor", RpcTarget.All);
    }

    public void UnlockDoor()
    {
        photonView.RPC("RPC_UnlockDoor", RpcTarget.All);
        //doorOpenSounds[0].Play();
        //doorOpenSounds[1].Play();
        //doorOpenSounds[2].Play();
    }

    [PunRPC]
    void RPC_LockDoor()
    {
        door.GetComponent<DoorLamp>().lampy.SetLamp(true, Color.red, true); //the light for the door
        door.GetComponentInChildren<Animator>().SetBool("Unlock", false);
        if(door.name == "Door 2") //if it is the second door, will need to change the bool for puzzle 3
        {
            doorTwoLocked = true;
        }
    }

    [PunRPC]
    void RPC_UnlockDoor()
    {
        door.GetComponent<DoorLamp>().lampy.SetLamp(true, Color.green, true);
        door.GetComponentInChildren<Animator>().SetBool("Unlock", true);

        switch(door.name)
        {
            case "Door 1":
                {
                    GetComponent<PuzzleManager>().whichPuzzle = 1;
                    GetComponent<PuzzleManager>().ActivatePuzzle();
                    break;
                }
            case "Door 2":
                {
                    GetComponent<PuzzleManager>().whichPuzzle = 2;
                    GetComponent<PuzzleManager>().ActivatePuzzle();
                    doorTwoLocked = false;
                    break;
                }
        }
    }
}
