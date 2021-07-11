using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class ClickOnSymbols : MonoBehaviour
{
    Sprite clickedSymbol;

    GameObject currentPuzzle, currentDoor;

    [SerializeField]
    GameObject[] puzzles;

    public void ClickOnSymbolsMethod(Button clickedButton)                    
    {
        clickedSymbol = clickedButton.GetComponent<Image>().sprite; //finding the symbol of the clicked button      

        currentDoor = GetComponent<DoorControl>().door;

        switch (currentDoor.name)
        {
            case "Door 1":
                {
                    currentPuzzle = puzzles[0];
                    currentPuzzle.GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    break;
                }
            case "Door 2":
                {
                    GameObject.Find("Inside").GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    GameObject.Find("Outside").GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    break;
                }
            case "Door 3":
                {
                    currentPuzzle = puzzles[2];
                    currentPuzzle.GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    break;
                }
        }
    }
}
