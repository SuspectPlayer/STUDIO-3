//Written by Jasper von Riegen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BeaconFlavouredHandScanner : MonoBehaviour
{
    [Tooltip("The tag of the direct interaction controllers")]public string triggerTag;

    public float noSpamSpamThyme;
    [HideInInspector] public bool spicedHam = false;
        
    public UnityEvent OnTouch;
    public UnityEvent OnTouchRelease;


    public void OnTriggerEnter(Collider other)
    {
        DoTheEnterThingVR(other);
    }
    public void OnTriggerExit(Collider other)
    {
        DoTheExitThingVR(other);
    }

    void Update()
    {
        
    }

    public void DoTheEnterThingVR(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            if (OnTouch != null && !spicedHam) OnTouch.Invoke();
        }
    }

    public void DoTheExitThingVR(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            if (OnTouchRelease != null) OnTouchRelease.Invoke();

        }

    }
    public void DoTheEnterThingFPS()
    {
        if (OnTouch != null && !spicedHam) OnTouch.Invoke();
    }

    public void DoTheExitThingFPS()
    {
        if (OnTouchRelease != null) OnTouchRelease.Invoke();
    }
    public void StartTimer()
    {
        StartCoroutine(BeaconTime());
    }
    IEnumerator BeaconTime()
    {
        spicedHam = true;
        yield return new WaitForSeconds(noSpamSpamThyme);
        spicedHam = false;
    }
}
