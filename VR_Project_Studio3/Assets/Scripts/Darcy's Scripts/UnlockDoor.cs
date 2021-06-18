﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    PhotonView photonView;

    void Start()
    {
        photonView = GameObject.Find("GameSetup").GetComponent<PhotonView>();
    }

    public void UnlockDoorMethod()
    {
        door.SetActive(true);
        photonView.RPC("RPC_UnlockDoor", RpcTarget.All);
    }

    [PunRPC]
    void RPC_UnlockDoor()
    {
        door.SetActive(true);
    }
}
