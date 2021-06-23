using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    GameObject door, puzzleManager;

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void UnlockDoorMethod()
    {
        photonView.RPC("RPC_UnlockDoor", RpcTarget.All);
    }

    [PunRPC]
    void RPC_UnlockDoor()
    {
        door.SetActive(true);
        puzzleManager.GetComponent<PuzzleManager>().ActivatePuzzle();
    }
}
