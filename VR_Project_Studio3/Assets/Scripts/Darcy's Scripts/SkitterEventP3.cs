using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class SkitterEventP3 : MonoBehaviour
{
    [SerializeField]
    GameObject dashboard;

    bool canMove = false, wait = false; 
    [HideInInspector]
    public bool playersLose = false;

    void Update()
    {
        if(canMove) //starts the co-routine. skitter can ony be active if the trigger is entered.
        {
            StartCoroutine("SkitterAnimation");
        }
        if(wait)
        {
            StartCoroutine("WaitForDoorUnlock");
        }
    }

    public void SpawnSkitter() //this activates the skitter from the trigger being tripped
    {
        gameObject.SetActive(true);
        canMove = true;
        GetComponent<Animator>().SetBool("canMove", true);
    }

    public void PlayersLose() //event for the end of the animation, once animation is done, players lose
    {
        playersLose = true;
    }

    IEnumerator SkitterAnimation() //checking the skitter variables for win/lose
    {
        Debug.Log("anime");
        if(playersLose) 
        {
            Debug.Log("lose");
            canMove = false;
            StopAllCoroutines();
            GameObject.Find("4 - Lights").GetComponent<LightManager>().TurnOffAllLights(); //turning off lights
            GetComponent<Animator>().SetBool("canMove", false);
            GameObject.Find("6 - Checkpoints").GetComponent<CheckpointControl>().LoadCheckpoint(); //loading the checkpoint
            gameObject.SetActive(false);
        }

        if (dashboard.GetComponent<DoorControl>().doorTwoLocked) //if the players manage to close the door, the script waits for them to open the door again to move on to the final door.
        {
            StopAllCoroutines();
            canMove = false;
            wait = true;
        }
        yield return null;
    }

    IEnumerator WaitForDoorUnlock()
    {
        if (!dashboard.GetComponent<DoorControl>().doorTwoLocked)
        {
            gameObject.SetActive(false); //if the door is locked, the players are safe
            dashboard.GetComponent<DoorControl>().door = GameObject.Find("Door 3"); //only sets to the third door if the skitter event is done
            wait = false;
            StopAllCoroutines();
        }
        yield return null;
    }
}
