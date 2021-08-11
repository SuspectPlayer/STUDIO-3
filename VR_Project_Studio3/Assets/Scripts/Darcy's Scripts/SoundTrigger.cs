using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

//Written by Darcy Glover

public class SoundTrigger : MonoBehaviour
{
    //this script can be used to trigger any sound from an object with the correct tag, walking through a trigger of an object that has an event emitter and this script attached

    public string triggerTag;

    StudioEventEmitter sound;

    bool isVRPlayer;

    void OnTriggerEnter(Collider other)
    {
        isVRPlayer = GameObject.Find("GameSetup").GetComponent<GameSetup>().isVRPlayer;
        if(other.tag == triggerTag && isVRPlayer)
        {
            sound = GetComponent<StudioEventEmitter>();
            sound.Play();
        }
    }
}
