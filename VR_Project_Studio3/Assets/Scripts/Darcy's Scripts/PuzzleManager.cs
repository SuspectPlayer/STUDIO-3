using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class PuzzleManager : MonoBehaviour
{ 
    [SerializeField]
    GameObject[] puzzles;

    bool isOn;

    void Start()
    {
        //puzzles[0].GetComponent<CorrectSymbolCheck>().canStart = true;
    }

    public void FirstPuzzle()
    {
        
    }
}
