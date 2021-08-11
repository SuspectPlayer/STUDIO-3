using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class HandScannerCorrectCount : MonoBehaviour
{
    [SerializeField]
    GameObject[] allScanners;

    public int correctCount = 0, realCount = 0;

    PhotonView photonView;

    public void CountUp()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        correctCount++;
        photonView.RPC("RPC_UpdateCount", RpcTarget.All);
    }

    public void ResetCount()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        correctCount = 0;
        photonView.RPC("RPC_UpdateCount", RpcTarget.All);
        //photonView.RPC("RPC_ResetCount", RpcTarget.All);
    }

    public void ResetAllScanners()
    {
        if(photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        for(int i = 0; i < 4; i++)
        {
            allScanners[i].GetComponent<ScannerFXBehaviours>().ResetFromFinished();
            allScanners[i].GetComponent<HandScannerTouchPad>().ResetBools();
        }
    }

    [PunRPC]
    void RPC_UpdateCount()
    {
        realCount = correctCount;
    }

    [PunRPC]
    void RPC_ResetCount()
    {
        correctCount = 0;
    }
}
