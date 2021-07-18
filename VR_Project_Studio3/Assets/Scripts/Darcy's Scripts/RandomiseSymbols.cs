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

    //[HideInInspector]
    public GameObject outside;

    [SerializeField]
    Sprite[] sprites;

    int randomNumber, numberChecker;
    int[] fourStoredRandomNumbers = new int[4], eightStoredRandomNumbers = new int[8];

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
        if (name == "Inside") //checking for the second puzzle, make sure that the symbols are unique
        {
            outside = GameObject.Find("Outside");
            for (int i = 0; i < 8; i++)
            {
                randomNumber = Random.Range(0, 20);
                eightStoredRandomNumbers[i] = randomNumber;
            }
            CheckNumbers(eightStoredRandomNumbers);
        }
        else
        {
            for (int i = 0; i < 4; i++) //applying the random numbers into the array
            {
                randomNumber = Random.Range(0, 20);
                fourStoredRandomNumbers[i] = randomNumber;
            }
            CheckNumbers(fourStoredRandomNumbers);
        }
    }

    void CheckNumbers(int[] storedNumbers)
    {
        for (int i = 0; i < 4; i++) //cross referencing the array to make sure all the numbers are unique.
        {
            numberChecker = storedNumbers[i];
            for (int x = 0; x < 4; x++)
            {
                if (x == i)
                {
                    continue;
                }
                else if (numberChecker == storedNumbers[x])
                {
                    PickUniqueRandomNumbers();
                }
            }
        }
    }

    void ApplySymbols()
    {
        photonView.RPC("RPC_ApplySymbols", RpcTarget.All, eightStoredRandomNumbers, fourStoredRandomNumbers);
    }

    [PunRPC]
    void RPC_ApplySymbols(int[] eightStored, int[] fourStored) //applying the symbols to the images to 'spawn' them in
    {
        Debug.Log(name);
        if(name == "Inside")
        {
            for (int i = 0; i < 4; i++)
            {
                symbols[i].GetComponent<SpriteRenderer>().sprite = sprites[eightStored[i]];
            }
            for (int i = 4; i < 8; i++)
            {
                outside.GetComponent<RandomiseSymbols>().symbols[i].GetComponent<SpriteRenderer>().sprite = sprites[eightStored[i]];
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                symbols[i].GetComponent<SpriteRenderer>().sprite = sprites[fourStored[i]];
            }

        }

        if (name == "Puzzle 3") //for the third puzzle, also needs to apply the symbols to the intelligence's map
        {
            for (int x = 0; x < 4; x++)
            {
                symbolsOnMap[x].GetComponent<Image>().sprite = sprites[fourStored[x]];
            }
        }
    }
}
