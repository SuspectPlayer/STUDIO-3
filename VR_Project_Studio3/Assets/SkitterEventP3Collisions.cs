using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class SkitterEventP3Collisions : MonoBehaviour
{
    [SerializeField]
    GameObject skitter;

    [HideInInspector]
    public bool canTrigger = false;

    //this script is the collision detection to spawn the skitter and to end the event for puzzle 3
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) //devtool to start for testing
        {
            canTrigger = true;
            Debug.Log("trigger active");
        }
    }


    void OnTriggerEnter(Collider other) //this collider is positioned across the entrance to the second room, which means the player will have to step through it.
    {
        if(canTrigger)
        {
            gameObject.SetActive(false); //turn off the collider to prevent from happening more than once
            skitter.GetComponent<SkitterEventP3>().SpawnSkitter();
        }
    }
}
