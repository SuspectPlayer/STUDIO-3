using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover
public class DevTeleport : MonoBehaviour
{
    [SerializeField]
    GameObject player1, player2;

    bool roomOne = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(roomOne)
            {
                player1.SetActive(false);
                player2.SetActive(true);
                roomOne = false;
            }
            else
            {
                player1.SetActive(true);
                player2.SetActive(false);
                roomOne = true;
            }
        }
    }
}
