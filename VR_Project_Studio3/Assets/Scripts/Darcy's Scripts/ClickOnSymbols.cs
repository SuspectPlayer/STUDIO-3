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
    GameObject[] puzzles, visualButtons;

    [SerializeField]
    Material[] greens, oranges;

    bool firstCondition, secondCondition;

    public void ClickOnSymbolsMethod(Button clickedButton)                    
    {
        clickedSymbol = clickedButton.GetComponent<Image>().sprite; //finding the symbol of the clicked button      

        currentDoor = GetComponent<DoorControl>().door;

        switch (currentDoor.name)
        {
            case "Door 1":
                {
                    currentPuzzle = puzzles[0];
                    firstCondition = currentPuzzle.GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    if(!firstCondition)
                    {
                        OrangeButtonColours();
                    }
                    break;
                }
            case "Door 2":
                {
                    firstCondition = GameObject.Find("Inside").GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    if(!firstCondition)
                    {
                       secondCondition = GameObject.Find("Outside").GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    }
                
                    if(!secondCondition)
                    {
                       OrangeButtonColours();
                    }
                    break;
                }
            case "Door 3":
                {
                    currentPuzzle = puzzles[2];
                    firstCondition = currentPuzzle.GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    if(!firstCondition)
                    {
                        OrangeButtonColours();
                    }
                    break;
                }
        }
    }

    public void GreenButtonColours(GameObject visualButton)
    {
        visualButton.GetComponent<MeshRenderer>().materials = greens;
    }

    public void OrangeButtonColours() //this method just changes the colour of the buttons for feedback to the player
    {
        for (int i = 0; i < visualButtons.Length; i++)
        {
            MeshRenderer rend = visualButtons[i].GetComponent<MeshRenderer>();

            if (rend.materials != oranges)
            {
                rend.materials = oranges;
            }
        }
    }
}
