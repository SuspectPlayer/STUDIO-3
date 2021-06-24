using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class DoorControl : MonoBehaviour
{
    [SerializeField]
    GameObject puzzleManager;

    public GameObject door;

    [SerializeField]
    Material green, red, yellow;

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
        door.GetComponentInChildren<MeshRenderer>().material = red;
    }

    [PunRPC]
    void RPC_UnlockDoor()
    {
        door.GetComponentInChildren<MeshRenderer>().material = green;
        puzzleManager.GetComponent<PuzzleManager>().ActivatePuzzle();
    }
}
