using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class ThirdPuzzleSkitter : MonoBehaviour
{
    [SerializeField]
    Transform target, dashboard;

    float moveSpeed = 1f, step;

    void Update()
    {
        step = moveSpeed * Time.deltaTime;

        if(gameObject.activeInHierarchy) //if the skitter is active, starts the co-routine. skitter can ony be active if the trigger is entered.
        {
            StartCoroutine("MoveAtPlayer");
        }
    }

    public void SpawnSkitter() //this activates the skitter from the trigger being tripped
    {
        gameObject.SetActive(true);
    }

    IEnumerator MoveAtPlayer() //moving the skitter
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if(transform.position == target.position) 
        {
            Debug.Log("dead");
            gameObject.SetActive(false); //if it reaches the target, stops the script and the players "die"
            StopAllCoroutines();
        }

        if (dashboard.GetComponent<DoorControl>().doorTwoLocked)
        {
            Debug.Log("alive");
            gameObject.SetActive(false); //if the door is locked, the players are safe
            dashboard.GetComponent<DoorControl>().door = GameObject.Find("Door 3"); //only sets to the third door if the skitter event is done
            StopAllCoroutines();
        }
        yield return null;
    }
}
