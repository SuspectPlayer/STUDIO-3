using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Echo_Airbag_Collider : MonoBehaviour
{
    public Animator airbags;

    PhotonView photonView;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            photonView.RPC("Puz3Bool", RpcTarget.Others);
        }
    }

    [PunRPC]
    public void Puz3Bool()
    {
        airbags.SetBool("puz3", true);
    }
}
