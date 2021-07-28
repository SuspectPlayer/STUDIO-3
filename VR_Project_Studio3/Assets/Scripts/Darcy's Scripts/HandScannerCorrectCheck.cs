using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class HandScannerCorrectCheck : MonoBehaviour
{
    PhotonView photonView;

    public Sprite scannedSymbol;

    [SerializeField]
    Sprite[] localScannedSymbols = new Sprite[4];

    [SerializeField]
    GameObject dashboard;

    [SerializeField]
    GameObject[] assignedSymbols;

    public void HandScanSymbolCheck() //this is for the handscanners to check if the symbols have been scanned in the right order
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        localScannedSymbols = GetComponentInParent<HandScannerCorrectCount>().totalScannedSymbols;

        Debug.Log("started");

        for (int i = 0; i < 4; i++)
        {
            if (localScannedSymbols[i] == null) //check to see which position in the array it needs to apply the newest scanned symbol
            {
                localScannedSymbols[i] = scannedSymbol;
                Debug.Log("applied");
                break;
            }
        }

        for (int x = 0; x < 4; x++)
        {
            if (localScannedSymbols[x] == null) //if the array position returns null, the players havent reached that part yet and the check doesnt need to be done
            {
                Debug.Log("breaking");
                break;
            }

            if (localScannedSymbols[x] == assignedSymbols[x].GetComponent<SpriteRenderer>().sprite) //checks the symbol against the static array, if they are the same it means they are in the right order
            {
                Debug.Log("correct");
                GetComponent<ScannerFXBehaviours>().ScanFinishedMat();
                GetComponentInParent<HandScannerCorrectCount>().CountUp();
            }
            else
            {
                Debug.Log("failed");
                GetComponentInParent<HandScannerCorrectCount>().ResetCount(); //resetting the count and the puzzle as a whole after an incorrect sequence
                ResetPuzzle();
                StartCoroutine(FailScan());
                break;
            }
        }

        if (GetComponentInParent<HandScannerCorrectCount>().correctCount == 3)
        {
            GameObject.Find("4 - Lights").GetComponent<LightManager>().TurnOffAllLights(); //turning off lights
            GameObject.Find("Skitter Trigger").GetComponent<SkitterEventP3Collisions>().TurnTriggerOn(); //turning on the trigger for when the player steps back into the other room to start the skitter event
        }

        if (GetComponentInParent<HandScannerCorrectCount>().correctCount >= 4)
        {
            dashboard.GetComponent<DoorControl>().UnlockDoor();
            //other win condition stuff in here
        }

        //photonView.RPC("RPC_HandScanSymbolCheck", RpcTarget.All);
    }

    void ResetPuzzle()
    {
        for (int i = 0; i < 4; i++) //resetting the scanned symbols back to default
        {
            localScannedSymbols[i] = null;
        }
        //photonView.RPC("RPC_ResetPuzzle", RpcTarget.All);
    }

    [PunRPC]
    void RPC_HandScanSymbolCheck()
    {

    }

    [PunRPC]
    void RPC_ResetPuzzle()
    {
        
    }

    IEnumerator FailScan()
    {
        Debug.Log("red");
        GetComponent<ScannerFXBehaviours>().ScanFailedMat();

        yield return new WaitForSeconds(2);

        Debug.Log("reset");
        GetComponent<ScannerFXBehaviours>().ResetFromFinished();
        GetComponent<HandScannerTouchPad>().ResetBools();
        StopAllCoroutines();
    }
}
