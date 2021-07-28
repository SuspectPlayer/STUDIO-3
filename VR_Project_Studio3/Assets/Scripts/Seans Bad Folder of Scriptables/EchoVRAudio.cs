// Script written by Sean Casey

using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoVRAudio : MonoBehaviour
{
    public StudioEventEmitter[] audiofiles;
    [Tooltip("The tag of the direct interaction controllers")] public string triggerTag;
    
    public bool audioTog;

    private void Start()
    {

        StartCoroutine("AmbiStart");
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag(triggerTag))
    //    {
    //        PlayingAmbience();
    //    }
    //}

    //public void ToggleCheck()
    //{
    //    Debug.Log("made it this far");
    //    if(audioTog == true)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void PlayingAmbience()
    //{
    //    Debug.Log("audio collider");
    //    audioTog = true;
    //    foreach (StudioEventEmitter s in audiofiles)
    //    {
    //        s.Play();
    //    }
    //    ToggleCheck();
    //}

    IEnumerator AmbiStart()
    {
        yield return new WaitForSeconds(1f);
        foreach (StudioEventEmitter s in audiofiles)
        {
            s.Play();
        }
    }
}
