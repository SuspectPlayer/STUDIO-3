// Script written by Sean Casey

using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoVRAudio : MonoBehaviour
{
    public StudioEventEmitter[] audiofiles;


    public bool audioTog;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GameController"))
        {
            Debug.Log("audio collider");
            audioTog = true;
            foreach(StudioEventEmitter s in audiofiles) 
            {
                s.Play();
            }
            ToggleCheck();
        }
    }

    public void ToggleCheck()
    {
        Debug.Log("made it this far");
        if(audioTog == true)
        {
            Destroy(gameObject);
        }
    }

}
