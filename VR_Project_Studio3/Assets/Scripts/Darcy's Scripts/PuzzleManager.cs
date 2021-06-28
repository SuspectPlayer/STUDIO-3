using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class PuzzleManager : MonoBehaviour
{ 
    [SerializeField]
    GameObject[] puzzles;

    public int whichPuzzle = 0; //checking which puzzle the player is currently solving

    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        ActivatePuzzle();
    }

    public void ActivatePuzzle() //method that activates puzzles
    {
        switch (whichPuzzle)
        {
            case 0: 
                {
                    photonView.RPC("RPC_ActivateFirstPuzzle", RpcTarget.All, whichPuzzle);
                    break;
                }
            case 1:
                {
                    photonView.RPC("RPC_ActivateSecondPuzzle", RpcTarget.All, whichPuzzle);
                    break;
                }
        }
    }

    public void DeactivatePuzzle() //method that deactivates puzzles
    {
        switch (whichPuzzle)
        {
            case 0:
                {
                    photonView.RPC("RPC_DeactivateFirstPuzzle", RpcTarget.All, whichPuzzle);
                    break;
                }
            case 1:
                {
                    photonView.RPC("RPC_DeactivateSecondPuzzle", RpcTarget.All, whichPuzzle);
                    break;
                }
        }
    }

    [PunRPC]
    void RPC_ActivateFirstPuzzle(int whichPuzzle)
    {
        puzzles[whichPuzzle].GetComponent<RandomiseSymbols>().enabled = true;
    }

    [PunRPC]
    void RPC_DeactivateFirstPuzzle(int whichPuzzle)
    {
        puzzles[whichPuzzle].GetComponent<CorrectSymbolCheck>().enabled = false;
    }

    [PunRPC]
    void RPC_ActivateSecondPuzzle(int whichPuzzle)
    {
        foreach (var p in puzzles[whichPuzzle].GetComponentsInChildren<RandomiseSymbols>()) //turning on all the randomisers in the second puzzle
        {
            p.enabled = true;
        }
        GetComponent<DoorControl>().door = GameObject.Find("Door 2"); //switching to the second door for unlocking purposes
    }

    [PunRPC]
    void RPC_DeactivateSecondPuzzle(int whichPuzzle)
    {
        foreach (var p in puzzles[whichPuzzle].GetComponentsInChildren<CorrectSymbolCheck>()) //turning on all the randomisers in the second puzzle
        {
            p.enabled = false;
        }
    }
}
