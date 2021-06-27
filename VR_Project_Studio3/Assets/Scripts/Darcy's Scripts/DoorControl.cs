using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class DoorControl : MonoBehaviour
{
    public GameObject door;

    PhotonView photonView;

    void Start()
    {
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
        door.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
        door.GetComponentInChildren<Animator>().SetBool("Unlock", false);
    }

    [PunRPC]
    void RPC_UnlockDoor()
    {
        door.GetComponentInChildren<MeshRenderer>().material.SetColor("_EmissionColor", Color.green);
        door.GetComponentInChildren<Animator>().SetBool("Unlock", true);
        GetComponent<PuzzleManager>().whichPuzzle++;
        GetComponent<PuzzleManager>().ActivatePuzzle();
    }
}
