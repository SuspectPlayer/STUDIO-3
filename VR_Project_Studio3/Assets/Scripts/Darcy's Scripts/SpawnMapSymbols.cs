using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class SpawnMapSymbols : MonoBehaviour
{
    [SerializeField]
    GameObject[] symbols, scanners; //this script spawns in the symbols on the map for puzzle 3

    Color black, white;

    void Start()
    {
        black.a = 1f; //the symbols are always there, but the alpha gets changed to make them visible
        white.a = 1f;

        black = Color.black;
        white = Color.white;
    }

    public void SpawnSymbols()
    {
        for(int i = 0; i < symbols.Length; i++)
        {
            symbols[i].GetComponent<Image>().color = black;
            scanners[i].GetComponent<Image>().color = white;
        }
    }
}
