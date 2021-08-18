using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SubmarineAmbience : MonoBehaviour
{
    [SerializeField]
    StudioEventEmitter amibent;

    void Update()
    {
        if(!amibent.IsPlaying() && !FindObjectOfType<GameSetup>().isVRPlayer)
        {
            amibent.Play();
        }
    }
}
