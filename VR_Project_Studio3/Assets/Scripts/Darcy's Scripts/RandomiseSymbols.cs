using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover and Jack Hobbs

public class RandomiseSymbols : MonoBehaviour
{
    [SerializeField]
    GameObject[] symbols;

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
            photonView.RPC("RPC_ApplySymbols", RpcTarget.All);
        }
    }

    void PickUniqueRandomNumbers()
    {
        for (int i = 0; i < 4; i++) //applying the random numbers into the array
        {
            randomNumber = Random.Range(0, 9);
            storedRandomNumbers[i] = randomNumber;
        }
        CheckNumbers();
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

    [PunRPC]
    void RPC_ApplySymbols() //applying the symbols to the images to 'spawn' them in
    {
        for (int i = 0; i < 4; i++)
        {
            symbols[i].GetComponentInChildren<Image>().sprite = sprites[storedRandomNumbers[i]];
        }
    }
}
