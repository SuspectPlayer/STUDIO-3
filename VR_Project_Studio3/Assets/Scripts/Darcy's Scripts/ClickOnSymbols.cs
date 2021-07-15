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

    bool condition;

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
                    condition = GameObject.Find("Inside").GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    if (!condition)
                    {
                        Debug.Log(condition);
                        GameObject.Find("Outside").GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol);
                    }
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
