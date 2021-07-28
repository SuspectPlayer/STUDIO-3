using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HandScannerCorrectCount : MonoBehaviour
{
    public int correctCount = 0;

    public Sprite[] totalScannedSymbols = new Sprite[4];

    PhotonView photonView;

    public void CountUp()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        correctCount++;
        //photonView.RPC("RPC_CountUp", RpcTarget.All);
    }

    public void ResetCount()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        correctCount = 0;
        //photonView.RPC("RPC_ResetCount", RpcTarget.All);
    }

    [PunRPC]
    void RPC_CountUp()
    {
        correctCount++;
    }

    [PunRPC]
    void RPC_ResetCount()
    {
        correctCount = 0;
    }
}
