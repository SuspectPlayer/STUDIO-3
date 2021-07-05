using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class ClickOnSymbols : MonoBehaviour
{
    Sprite clickedSymbol;

    GameObject currentPuzzle;

    [SerializeField]
    GameObject[] puzzles;

    public void ClickOnSymbolsMethod(Button clickedButton)                    
    {
        clickedSymbol = clickedButton.GetComponent<Image>().sprite; //finding the symbol of the clicked button      

        currentPuzzle = puzzles[GetComponent<PuzzleManager>().whichPuzzle];

        if(currentPuzzle.name == "Puzzle 2") //if on puzzle 2, there are 2 sets of symbols so it needs to differentiate
        {
            currentPuzzle.GetComponentInChildren<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
        }
        else
        {
            currentPuzzle.GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
        }
    }
}
