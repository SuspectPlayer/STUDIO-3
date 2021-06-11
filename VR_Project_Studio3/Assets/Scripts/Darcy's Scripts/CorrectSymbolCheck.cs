using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class CorrectSymbolCheck : MonoBehaviour
{
    [SerializeField]
    GameObject[] symbols;

    [SerializeField]
    Text correctCount;

    Color[] clickedSymbols = new Color[4];

    [SerializeField]
    Color neutral;

    int correctSymbolCount = 0, incorrectSymbolCount = 0;

    public void CorrectSymbolCheckMethod(Color clickedSymbol)
    {
        for (int i = 0; i < 4; i++) //checking to see if the clicked colour is one of the ones in the "spawned" symbols on the map
        {
            if (clickedSymbol == symbols[i].GetComponent<Image>().color && correctSymbolCount < 4)
            {
                if(clickedSymbol == clickedSymbols[i])
                {
                    correctSymbolCount = 0;
                    for(int x = 0; x < 4; x++) //refreshing the array after a failed attempt
                    {
                        clickedSymbols[x] = neutral;
                    }
                    break;
                }

                clickedSymbols[i] = clickedSymbol;
                correctSymbolCount++;

                if(correctSymbolCount >= 4) //"win" condition
                {
                    gameObject.GetComponent<UnlockDoor>().UnlockDoorMethod();
                }
            }
            else
            {
                incorrectSymbolCount++;
                if(incorrectSymbolCount >= 4)
                {
                    correctSymbolCount = 0;
                    for (int x = 0; x < 4; x++) //refreshing the array after a failed attempt
                    {
                        clickedSymbols[x] = neutral;
                    }
                }
            }
        }
        correctCount.text = correctSymbolCount.ToString();
        incorrectSymbolCount = 0;
    }
}
