using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPuzzleSkitter : MonoBehaviour
{
    GameObject trigger;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)) //devtool to start for testing
        {
            trigger.SetActive(true); 
        }

        if(gameObject.activeInHierarchy) //if the skitter is active, starts the co-routine. skitter can ony be active if the trigger is entered.
        {
            StartCoroutine("MoveAtPlayer");
        }
    }

    void SpawnSkitter()
    {
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if(!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }
    }

    IEnumerator MoveAtPlayer()
    {

        yield return null;
    }
}
