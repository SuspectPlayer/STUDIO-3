using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class SpawnMapSymbols : MonoBehaviour
{
    [SerializeField]
    GameObject[] symbols; //this script spawns in the symbols on the map for puzzle 3

    Color alpha;

    void Start()
    {
        alpha.a = 1f; //the symbols are always there, but the alpha gets changed to make them visible
    }

    public void SpawnSymbols()
    {
        for(int i = 0; i < symbols.Length; i++)
        {
            symbols[i].GetComponent<Image>().color = alpha;
        }
    }
}
