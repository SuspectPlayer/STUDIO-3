using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover
public class DevTeleport : MonoBehaviour
{
    [SerializeField]
    Transform player, room1, room2;

    bool roomOne = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(roomOne)
            {
                player.position = room2.position;
            }
            else
            {
                player.position = room1.position;
            }
        }
    }
}
