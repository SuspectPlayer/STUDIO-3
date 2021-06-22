﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class CorrectSymbolCheck : MonoBehaviour
{
    [SerializeField]
    GameObject[] symbols;

    [SerializeField]
    GameObject dashboard;

    Sprite[] clickedSymbols = new Sprite[4];

    [SerializeField]
    Sprite neutral;

    int correctSymbolCount = 0, incorrectSymbolCount = 0;

    public bool canStart = false;

    public void CorrectSymbolCheckMethod(Sprite clickedSymbol)
    {
        //if(canStart)
        //{
            for (int i = 0; i < 4; i++) //checking to see if the clicked colour is one of the ones in the "spawned" symbols on the map
            {
                if (clickedSymbol == symbols[i].GetComponentInChildren<Image>().sprite && correctSymbolCount < 4)
                {
                    if (clickedSymbol == clickedSymbols[i])
                    {
                        correctSymbolCount = 0;
                        for (int x = 0; x < 4; x++) //refreshing the array after a failed attempt
                        {
                            clickedSymbols[x] = neutral;
                        }
                        break;
                    }

                    Debug.Log("correct");
                    clickedSymbols[i] = clickedSymbol;
                    correctSymbolCount++;

                    if (correctSymbolCount >= 4) //"win" condition
                    {
                        dashboard.GetComponent<UnlockDoor>().UnlockDoorMethod();
                    }
                }
                else
                {
                    incorrectSymbolCount++;
                    if (incorrectSymbolCount >= 4)
                    {
                        correctSymbolCount = 0;
                        for (int x = 0; x < 4; x++) //refreshing the array after a failed attempt
                        {
                            clickedSymbols[x] = neutral;
                        }
                    }
                }
            }
            incorrectSymbolCount = 0;
        //}
    }
}
