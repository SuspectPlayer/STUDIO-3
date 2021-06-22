using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class ClickOnSymbols : MonoBehaviour
{
    Sprite clickedSymbol;

    public void ClickOnSymbolsMethod(Button clickedButton)                    
    {
        clickedSymbol = clickedButton.GetComponent<Image>().sprite; //finding the symbol of the clicked button 

        gameObject.GetComponent<CorrectSymbolCheck>().CorrectSymbolCheckMethod(clickedSymbol); //calling the "check" script

        clickedButton.tag = "Untagged"; //setting the tag back to default
    }
}
