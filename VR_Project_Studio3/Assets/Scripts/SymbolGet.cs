using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SymbolGet : MonoBehaviour
{
    public int number;

    PhotonView photonView;

    public void GetSymbol()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        switch (name)
        {
            case "HandScanner 1":
                {
                    number = 0;
                    photonView.RPC("RPC_SymbolGet", RpcTarget.All, number);
                    break;
                }
            case "HandScanner 2":
                {
                    number = 1;
                    photonView.RPC("RPC_SymbolGet", RpcTarget.All, number);
                    break;
                }
            case "HandScanner 3":
                {
                    number = 2;
                    photonView.RPC("RPC_SymbolGet", RpcTarget.All, number);
                    break;
                }
            case "HandScanner 4":
                {
                    number = 3;
                    photonView.RPC("RPC_SymbolGet", RpcTarget.All, number);
                    break;
                }
        }
    }

    [PunRPC]
    void RPC_SymbolGet(int number)
    {
        GetComponent<HandScannerCorrectCheck>().scannedSymbol = GetComponentInParent<RandomiseSymbols>().symbols[number].GetComponent<SpriteRenderer>().sprite;
    }
}
    
