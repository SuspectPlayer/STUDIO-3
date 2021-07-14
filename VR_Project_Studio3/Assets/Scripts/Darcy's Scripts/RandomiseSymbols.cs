using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover and Jack Hobbs

public class RandomiseSymbols : MonoBehaviour
{
    public GameObject[] symbols;

    [SerializeField]
    GameObject[] symbolsOnMap;

    GameObject outside;

    [SerializeField]
    Sprite[] sprites;

    int randomNumber, numberChecker;
    int[] storedRandomNumbers = new int[4];

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PickUniqueRandomNumbers();
            ApplySymbols();
        }
    }

    void PickUniqueRandomNumbers()
    {
        for (int i = 0; i < 4; i++) //applying the random numbers into the array
        {
            randomNumber = Random.Range(0, 20);
            storedRandomNumbers[i] = randomNumber;
        }
        CheckNumbers();
        if (name == "Inside") //checking for the second puzzle, make sure that the symbols are unique
        {
            outside = GameObject.Find("Outside");
            for (int i = 0; i < 4; i++)
            {
                for(int x = 0; x < 4; x++)
                {
                    if (storedRandomNumbers[i] == outside.GetComponent<RandomiseSymbols>().storedRandomNumbers[x])
                    {
                        PickUniqueRandomNumbers();
                    }
                }
            }
        }
    }

    void CheckNumbers()
    {
        for (int i = 0; i < 4; i++) //cross referencing the array to make sure all the numbers are unique.
        {
            numberChecker = storedRandomNumbers[i];
            for (int x = 0; x < 4; x++)
            {
                if (x == i)
                {
                    continue;
                }
                else if (numberChecker == storedRandomNumbers[x])
                {
                    PickUniqueRandomNumbers();
                }
            }
        }
    }

    void ApplySymbols()
    {
        photonView.RPC("RPC_ApplySymbols", RpcTarget.All, storedRandomNumbers);
    }

    [PunRPC]
    void RPC_ApplySymbols(int[] storedNumbers) //applying the symbols to the images to 'spawn' them in
    {
        Debug.Log(name);
        for (int i = 0; i < 4; i++)
        {
            symbols[i].GetComponentInChildren<Image>().sprite = sprites[storedNumbers[i]];
        }

        if (name == "Puzzle 3") //for the third puzzle, also needs to apply the symbols to the intelligence's map
        {
            for (int x = 0; x < 4; x++)
            {
                symbolsOnMap[x].GetComponentInChildren<Image>().sprite = sprites[storedNumbers[x]];
            }
        }
    }
}
