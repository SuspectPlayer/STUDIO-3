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
    GameObject dashboard, door;

    Sprite[] clickedSymbols = new Sprite[4];

    [SerializeField]
    Sprite neutral;

    int correctSymbolCount = 0, incorrectSymbolCount = 0;

    bool devCheat = false;

    public void CorrectSymbolCheckMethod(Sprite clickedSymbol)
    {
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

                if (correctSymbolCount >= 4 || devCheat) //"win" condition
                {
                    switch (door.name) //checking which puzzle the player is currently doing
                    {
                        case "Door 1":
                            {
                                dashboard.GetComponent<DoorControl>().UnlockDoor();
                                break;
                            }
                        case "Door 2":
                            {
                                Debug.Log("complete");
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

    void Update() //dev tool to complete the puzzles for easier testing
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            devCheat = true;
        }
    }
}
