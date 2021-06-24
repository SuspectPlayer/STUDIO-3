using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class PuzzleManager : MonoBehaviour
{ 
    [SerializeField]
    GameObject[] puzzles;

    int puzzleCount = 0;

    PhotonView photonView;

    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        ActivatePuzzle();
    }

    public void ActivatePuzzle()
    {
        photonView.RPC("RPC_ActivatePuzzle", RpcTarget.All);
    }

    [PunRPC]
    void RPC_ActivatePuzzle()
    {
        switch (puzzleCount)
        {
            case 0: //turning on the randomiser in the first puzzle
                {
                    puzzles[puzzleCount].GetComponent<RandomiseSymbols>().enabled = true;
                    puzzleCount++;
                    break;
                }
            case 1:
                {
                    puzzles[puzzleCount - 1].GetComponent<CorrectSymbolCheck>().enabled = false;
                    foreach (var p in puzzles[puzzleCount].GetComponentsInChildren<RandomiseSymbols>()) //turning on all the randomisers in the second puzzle
                    {
                        p.enabled = true;
                    }
                    foreach (var c in puzzles[puzzleCount].GetComponentsInChildren<CorrectSymbolCheck>()) //turning on all the correct symbol checkers in the second puzzle
                    {
                        c.enabled = true;
                    }
                    GetComponent<DoorControl>().door = GameObject.Find("Door 2"); //switching to the second door for unlocking purposes
                    puzzleCount++;
                    break;
                }
        }
    }
}
