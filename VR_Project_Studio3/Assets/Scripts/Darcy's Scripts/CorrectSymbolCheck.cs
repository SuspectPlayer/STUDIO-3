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
    GameObject dashboard, door;

    [HideInInspector]
    public Sprite temp;

    Sprite[] clickedSymbols = new Sprite[4];

    [SerializeField]
    Sprite neutral;

    [HideInInspector]
    public int correctSymbolCount = 0;
    int incorrectSymbolCount = 0, rightOrderCount = 0;

    void Start() //this is only here so i can turn on and off the script component
    {
        
    }

    public void CorrectSymbolCheckMethod(Sprite clickedSymbol)
    {
        if(clickedSymbol == null)
        {
            clickedSymbol = temp;
        }

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

                clickedSymbols[correctSymbolCount] = clickedSymbol;
                correctSymbolCount++;
                Debug.Log(correctSymbolCount.ToString() + " " + name);

                if (correctSymbolCount >= 4) //"win" condition
                {
                    switch (door.name) //checking which puzzle the player is currently doing
                    {
                        case "Door 1":
                            {
                                dashboard.GetComponent<DoorControl>().UnlockDoor();
                                correctSymbolCount = 0;
                                break;
                            }
                        case "Door 2":
                            {
                                if(gameObject.name == "Inside")
                                {
                                    dashboard.GetComponent<DoorControl>().LockDoor();
                                    correctSymbolCount = 0;
                                }
                                else
                                {
                                    dashboard.GetComponent<DoorControl>().UnlockDoor();
                                    correctSymbolCount = 0;
                                }
                                break;
                            }
                        case "Door 3": //for door 3, the symbols will need to be in a particular order
                            {
                                for(int x = 0; x < 4; x++)
                                {
                                    if(clickedSymbols[i] == symbols[i].GetComponentInChildren<Image>().sprite)
                                    {
                                        Debug.Log("right order");
                                        rightOrderCount++;                                     
                                    }
                                    else
                                    {
                                        Debug.Log("wrong order");
                                        correctSymbolCount = 0;
                                        break;
                                    }
                                }
                                if(rightOrderCount == 4) //if the 4 symbols were clicked in the right order it opens the door
                                {
                                    dashboard.GetComponent<DoorControl>().UnlockDoor();
                                    correctSymbolCount = 0;
                                }
                                break;
                            }
                    }
                }
            }
            else
            {
                incorrectSymbolCount++; //counts when the player hits a wrong button
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
    }
}
