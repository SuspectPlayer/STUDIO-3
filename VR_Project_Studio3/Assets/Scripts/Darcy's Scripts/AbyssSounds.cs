using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AbyssSounds : MonoBehaviour
{
    [SerializeField]
    StudioEventEmitter underwater, breathing;

    void Update()
    {
        if(FindObjectOfType<GameSetup>().isVRPlayer && !underwater.IsPlaying())
        {
            underwater.Play();
            breathing.Play();
        }
    }
}
