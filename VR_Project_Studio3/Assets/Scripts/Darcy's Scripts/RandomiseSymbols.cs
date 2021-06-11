using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class RandomiseSymbols : MonoBehaviour
{
    [SerializeField]
    GameObject[] symbols;

    [SerializeField]
    Color[] colours;

    int randomNumber, numberChecker;
    int[] storedRandomNumbers = new int[4];

    void Start()
    {
        PickUniqueRandomNumbers();
        ApplySymbols();
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

    void ApplySymbols() //applying the symbols to the images to 'spawn' them in
    {
        for(int i = 0; i < 4; i++)
        {
            symbols[i].GetComponent<Image>().color = colours[storedRandomNumbers[i]];
        }
    }
}
