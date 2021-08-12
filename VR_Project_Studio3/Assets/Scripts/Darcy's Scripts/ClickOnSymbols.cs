using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover

public class ClickOnSymbols : MonoBehaviour
{
    Sprite clickedSymbol;

    PhotonView photonView;

    GameObject currentPuzzle, currentDoor;

    [SerializeField]
    GameObject[] puzzles, visualButtons;

    [SerializeField]
    Material[] greens, oranges;

    bool firstCondition = false, secondCondition = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void ClickOnSymbolsMethod(Button clickedButton)                    
    {
        clickedSymbol = clickedButton.GetComponentInChildren<Image>().sprite; //finding the symbol of the clicked button      

        currentDoor = GetComponent<DoorControl>().door;

        switch (currentDoor.name)
        {
            case "Door 1":
                {
                    currentPuzzle = puzzles[0];
                    firstCondition = currentPuzzle.GetComponent<PuzzleCompletionManager>().CorrectSymbolCheck(clickedSymbol);
                    if(!firstCondition)
                    {
                        OrangeButtonColours();
                    }
                    break;
                }
            case "Door 2":
                {
                    firstCondition = false;

                    DoorControl doorControl = FindObjectOfType<DoorControl>();

                    Debug.Log("Before first condition");

                    firstCondition = GameObject.Find("Inside").GetComponent<PuzzleCompletionManager>().CorrectSymbolCheck(clickedSymbol);

                    Debug.Log("Before second condition");

                    if (!firstCondition)
                    {
                       Debug.Log("Attempting second condition");
                       secondCondition = GameObject.Find("Outside").GetComponent<PuzzleCompletionManager>().CorrectSymbolCheck(clickedSymbol);
                       Debug.Log("After second condition");
                    }
                
                    if(!secondCondition && !firstCondition)
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
        photonView.RPC("RPC_OrangeButtonColours", RpcTarget.All);
    }

    [PunRPC]
    void RPC_OrangeButtonColours()
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
