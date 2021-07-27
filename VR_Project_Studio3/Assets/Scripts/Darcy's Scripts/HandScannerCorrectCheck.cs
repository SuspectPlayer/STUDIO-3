using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class HandScannerCorrectCheck : MonoBehaviour
{
    public Sprite scannedSymbol;

    Sprite[] scannedSymbols;

    [SerializeField]
    GameObject dashboard;

    [SerializeField]
    GameObject[] assignedSymbols;

    int correctCount;

    public void HandScanSymbolCheck() //this is for the handscanners to check if the symbols have been scanned in the right order
    {
        for (int i = 0; i < 4; i++)
        {
            if(scannedSymbols[i] == null) //check to see which position in the array it needs to apply the newest scanned symbol
            {
                scannedSymbols[i] = scannedSymbol;
                break;
            }
        }

        for (int x = 0; x < 4; x++)
        {
            if (scannedSymbols[x] == null) //if the array position returns null, the players havent reached that part yet and the check doesnt need to be done
            {
                break;
            }

            if (scannedSymbols[x] == assignedSymbols[x]) //checks the symbol against the static array, if they are the same it means they are in the right order
            {
                correctCount++;
            }
            else
            {
                Debug.Log("failed");
                correctCount = 0; //resetting the count and the puzzle as a whole after an incorrect sequence
                ResetPuzzle();
                StartCoroutine(FailScan());
                break;
            }
        }

        if (correctCount == 3)
        {
            GameObject.Find("4 - Lights").GetComponent<LightManager>().TurnOffAllLights(); //turning off lights
            GameObject.Find("Skitter Trigger").GetComponent<SkitterEventP3Collisions>().TurnTriggerOn(); //turning on the trigger for when the player steps back into the other room to start the skitter event
        }

        if(correctCount >= 4)
        {
            dashboard.GetComponent<DoorControl>().UnlockDoor();
            //other win condition stuff in here
        }
    }

    void ResetPuzzle()
    {
        for(int i = 0; i < 4; i++) //resetting the scanned symbols back to default
        {
            if(scannedSymbols[i] != null)
            {
                scannedSymbols[i] = null;
            }
        }
    }

    IEnumerator FailScan()
    {
        Debug.Log("red");
        GetComponent<ScannerFXBehaviours>().ScanFailedMat();

        yield return new WaitForSeconds(2);


        Debug.Log("reset");
        GetComponent<ScannerFXBehaviours>().ResetFromFinished();
        StopAllCoroutines();
    }
}
