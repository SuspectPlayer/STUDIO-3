using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

//Written by Darcy Glover - Any FMOD scripting was done by Sean Casey

public class CorrectSymbolCheck : MonoBehaviour
{
    [SerializeField]
    StudioEventEmitter[] doorCloseSounds; //When wrong answer

    [SerializeField]
    GameObject[] symbols;

    [SerializeField]
    GameObject dashboard, door;

    Sprite[] clickedSymbols = new Sprite[4];

    [SerializeField]
    Sprite neutral;

    [Tooltip("The bool or trigger name for the animation")] public string animParameter;
    [SerializeField]
    public Animator intelPuzzleAnims;

        
    //[HideInInspector]
    public int correctSymbolCount = 0;
    int incorrectSymbolCount = 0;

    void Start() //this is only here so i can turn on and off the script component
    {
        
    }

    public bool CorrectSymbolCheckMethod(Sprite clickedSymbol)
    {
        bool condition;

        for (int i = 0; i < 4; i++) //checking to see if the clicked colour is one of the ones in the "spawned" symbols on the map
        {
            if (clickedSymbol == symbols[i].GetComponent<SpriteRenderer>().sprite && correctSymbolCount < 4)
            {
                for (int z = 0; z < 4; z++)
                {
                    if(clickedSymbol == clickedSymbols[z])
                    {
                        Debug.Log("wrong");
                        condition = false;
                        correctSymbolCount = 0;
                        for (int x = 0; x < 4; x++) //refreshing the array after a failed attempt
                        {
                            clickedSymbols[x] = neutral;
                        }
                        return condition;
                        //break;
                    }
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
                                intelPuzzleAnims.SetBool(animParameter, true);
                                dashboard.GetComponent<ClickOnSymbols>().OrangeButtonColours(); //this turns the buttons back to orange for feedback
                                dashboard.GetComponent<DoorControl>().UnlockDoor();
                                correctSymbolCount = 0;
                                condition = true;
                                return condition;
                                //break;
                            }
                        case "Door 2":
                            {
                                if(gameObject.name == "Inside")
                                {
                                    dashboard.GetComponent<ClickOnSymbols>().OrangeButtonColours(); 
                                    dashboard.GetComponent<DoorControl>().LockDoor();
                                    correctSymbolCount = 0;
                                }
                                else
                                {
                                    if(!intelPuzzleAnims.GetBool("puz2"))
                                    {
                                        intelPuzzleAnims.SetBool("puz2", true);
                                    }
                                    dashboard.GetComponent<ClickOnSymbols>().OrangeButtonColours();
                                    dashboard.GetComponent<DoorControl>().UnlockDoor();
                                    correctSymbolCount = 0;
                                }
                                condition = true;
                                return condition;
                                //break;
                            }
                    }
                }
            }
            else
                {
                incorrectSymbolCount++; //counts when the player hits a wrong button
                Debug.Log(incorrectSymbolCount.ToString());
                if (incorrectSymbolCount >= 4)
                {
                    doorCloseSounds[dashboard.GetComponent<PuzzleManager>().whichPuzzle].Play(); // Plays feedback song that players were wrong
                    Debug.Log("incorrect");
                    correctSymbolCount = 0;
                    incorrectSymbolCount = 0;
                    for (int x = 0; x < 4; x++) //refreshing the array after a failed attempt
                    {
                        clickedSymbols[x] = neutral;
                    }
                }
            }
        }
        incorrectSymbolCount = 0;
        if (correctSymbolCount > 0)
        {
            condition = true;
        }
        else
        {
            condition = false;
        }
        return condition;
    }
}
