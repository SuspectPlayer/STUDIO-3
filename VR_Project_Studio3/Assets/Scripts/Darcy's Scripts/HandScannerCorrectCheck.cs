using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover, Jasper von Riegen, and Jack Hobbs

public class HandScannerCorrectCheck : MonoBehaviour
{
    PhotonView photonView;

    TimeController timeController;

    public Sprite scannedSymbol;

    [SerializeField]
    Sprite[] localScannedSymbols = new Sprite[4];

    [SerializeField]
    GameObject dashboard;

    [SerializeField]
    GameObject[] assignedSymbols;

    public bool isCorrect = false;

    string masterPlayerName, otherPlayerName;

    public void HandScanSymbolCheck() //this is for the handscanners to check if the symbols have been scanned in the right order
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }
        photonView.RPC("RPC_HandScanSymbolCheck", RpcTarget.All);
    }

    void ResetPuzzle()
    {
        if (photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }
        photonView.RPC("RPC_ResetPuzzle", RpcTarget.All);
    }

    [PunRPC]
    void RPC_HandScanSymbolCheck()
    {
        timeController = FindObjectOfType<TimeController>();

        timeController.EndBetweenTimer();

        localScannedSymbols[GetComponentInParent<HandScannerCorrectCount>().realCount] = scannedSymbol;

        Debug.Log("started " + scannedSymbol.ToString());

        for (int x = 0; x < 4; x++)
        {
            //Debug.Log(localScannedSymbols[x].ToString() + " " + gameObject.name + " " + x.ToString());
            if (localScannedSymbols[x] == assignedSymbols[x].GetComponent<SpriteRenderer>().sprite) //checks the symbol against the static array, if they are the same it means they are in the right order
            {
                //photonView.RPC("RPC_Correct", RpcTarget.Others);
                Correct();
                break;
            }
            else if (localScannedSymbols[x] != null && !isCorrect)
            {
                Debug.Log("calling fail " + x.ToString());
                //photonView.RPC("RPC_Fail", RpcTarget.Others);
                Fail();
                break;
            }
        }

        if (GetComponentInParent<HandScannerCorrectCount>().realCount == 3)
        {
            GameObject.Find("4 - Lights").GetComponent<LightManager>().TurnOffAllLights(); //turning off lights
            GameObject.Find("Skitter Trigger").GetComponent<SkitterEventP3Collisions>().TurnTriggerOn(); //turning on the trigger for when the player steps back into the other room to start the skitter event
        }

        if (GetComponentInParent<HandScannerCorrectCount>().realCount >= 4)
        {
            if(!PhotonNetwork.IsMasterClient)
            {
                DarcyAnalyticMethods analyticMethods = FindObjectOfType<DarcyAnalyticMethods>();

                timeController.EndMainTimer();

                masterPlayerName = PhotonNetwork.MasterClient.NickName;
                otherPlayerName = PhotonNetwork.LocalPlayer.NickName;

                float mainElapsedTime = timeController.GetTime(0);

                string names = masterPlayerName + " and " + otherPlayerName;

                int level = FindObjectOfType<PuzzleCompletionManager>().GetCurrentLevel();

                analyticMethods.PuzzleThreeCompletion(mainElapsedTime, names, level);

                int puzzleThreeAttempts = dashboard.GetComponent<PuzzleCompletionManager>().puzzleThreeAttempts;

                analyticMethods.PuzzleThreeAttemptsMade(puzzleThreeAttempts, level, names);

                dashboard.GetComponent<DoorControl>().UnlockDoor();
            }

            LightManager lightManager = FindObjectOfType<LightManager>();
            lightManager.SendLightUsage();
        }
    }

    [PunRPC]
    void RPC_ResetPuzzle()
    {
        for (int i = 0; i < 4; i++) //resetting the scanned symbols back to default
        {
            localScannedSymbols[i] = null;
        }
        isCorrect = false;
    }

    //[PunRPC]
    void Fail()
    {
        Debug.Log("called fail");
        if(!isCorrect)
        {
            Debug.Log("failed " + gameObject.name);
            GetComponentInParent<HandScannerCorrectCount>().ResetCount(); //resetting the count and the puzzle as a whole after an incorrect sequence
            ResetPuzzle();
            PuzzleCompletionManager puzzleCompletionManager = FindObjectOfType<PuzzleCompletionManager>();
            puzzleCompletionManager.PuzzleAttempt();
            StartCoroutine(FailScan());
        }
    }

    //[PunRPC]
    void Correct()
    {
        Debug.Log("correct");
        GetComponent<ScannerFXBehaviours>().ScanFinishedMat();
        GetComponentInParent<HandScannerCorrectCount>().CountUp();
    }

    IEnumerator FailScan()
    {
        GetComponent<ScannerFXBehaviours>().ScanFailedMat();

        yield return new WaitForSeconds(2);

        Debug.Log("reset");
        GetComponentInParent<HandScannerCorrectCount>().ResetAllScanners();
        StopAllCoroutines();
    }
}
