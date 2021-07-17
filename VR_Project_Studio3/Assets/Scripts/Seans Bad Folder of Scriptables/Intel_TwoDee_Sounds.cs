// SEAN CASEY WROTE THIS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intel_TwoDee_Sounds : MonoBehaviour
{


    public void LightButton()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Feedback Sounds/Intel Feedback/Light Clicks");
    }

    public void SymbolButtonSounds()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Feedback Sounds/Intel Feedback/Intel Buttons");
    }
}
