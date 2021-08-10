using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Photon.Pun;

//Written by Darcy Glover - Any FMOD scripting was done by Sean Casey

public class PuzzleCompletionManager : MonoBehaviour
{
    [SerializeField]
    StudioEventEmitter[] doorCloseSounds; //When wrong answer

    [SerializeField]
    TimeController timeController;

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

    PhotonView photonView;

    bool isVRPlayer;
        
    //[HideInInspector]
    public int correctSymbolCount = 0;
    int incorrectSymbolCount = 0;

    string masterPlayerName, otherPlayerName;

    void Start()
    {
        timeController.StartTimer();
    }

    public bool CorrectSymbolCheck(Sprite clickedSymbol)
    {
        bool condition;
        isVRPlayer = GameObject.Find("GameSetup").GetComponent<GameSetup>().isVRPlayer;

        if(photonView == null)
        {
            photonView = GetComponent<PhotonView>();
        }

        incorrectSymbolCount = 0;

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
                                photonView.RPC("RPC_PuzzleOneComplete", RpcTarget.All);
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
                                    bool completion = true;
                                    //TimeController.instance.EndTimer(completion);
                                    correctSymbolCount = 0;
                                    incorrectSymbolCount = 0;
                                }
                                else
                                {
                                    if(!intelPuzzleAnims.GetBool("puz2"))
                                    {
                                        intelPuzzleAnims.SetBool("puz2", true);
                                    }
                                    dashboard.GetComponent<ClickOnSymbols>().OrangeButtonColours();
                                    dashboard.GetComponent<DoorControl>().UnlockDoor();
                                    bool completion = true;
                                    //TimeController.instance.EndTimer(completion);
                                    correctSymbolCount = 0;
                                    incorrectSymbolCount = 0;
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
                    if(dashboard.GetComponent<PuzzleManager>().whichPuzzle < 2 && isVRPlayer)
                    {
                        doorCloseSounds[dashboard.GetComponent<PuzzleManager>().whichPuzzle].Play(); // Plays feedback song that players were wrong
                    }

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

    [PunRPC]
    void RPC_PuzzleOneComplete()
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            DarcyAnalyticMethods analyticMethods = FindObjectOfType<DarcyAnalyticMethods>();
            Debug.Log("Analytic Object: " + analyticMethods.gameObject.name);

            intelPuzzleAnims.SetBool(animParameter, true);
            dashboard.GetComponent<ClickOnSymbols>().OrangeButtonColours(); //this turns the buttons back to orange for feedback
            dashboard.GetComponent<DoorControl>().UnlockDoor();

            Debug.Log("Ending Timer");
            timeController.EndTimer();

            Debug.Log("Attemping to set names");
            masterPlayerName = PhotonNetwork.MasterClient.NickName;
            otherPlayerName = PhotonNetwork.LocalPlayer.NickName;
            Debug.Log("Names: " + masterPlayerName + " " + otherPlayerName);

            float elaspedTime = timeController.elaspedTime;

            Debug.Log("Time: " + elaspedTime);

            analyticMethods.PuzzleOneCompletion(elaspedTime);

            string message = "Puzzle 1 was completed by: " + masterPlayerName + " and " + otherPlayerName + " in " + elaspedTime + " seconds";
            Debug.Log(message);

            analyticMethods.PuzzleOneCompletionTest(message);

            correctSymbolCount = 0;
            incorrectSymbolCount = 0;
        }
    }
}
