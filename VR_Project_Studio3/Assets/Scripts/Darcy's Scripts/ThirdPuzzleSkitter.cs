using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class ThirdPuzzleSkitter : MonoBehaviour
{
    [SerializeField]
    Transform target, dashboard;

    float moveSpeed = 1f, step;

    bool canMove = false, wait = false;

    void Update()
    {
        step = moveSpeed * Time.deltaTime;

        if(canMove) //starts the co-routine. skitter can ony be active if the trigger is entered.
        {
            StartCoroutine("MoveAtPlayer");
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
    }

    IEnumerator MoveAtPlayer() //moving the skitter
    {
        if(!dashboard.GetComponent<DoorControl>().doorTwoLocked)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

        if(transform.position == target.position) 
        {
            gameObject.SetActive(false); //if it reaches the target, stops the script and the players "die"
            canMove = false;
            StopAllCoroutines();
            GameObject.Find("Checkpoints").GetComponent<CheckpointControl>().LoadCheckpoint(); //loading the checkpoint
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
