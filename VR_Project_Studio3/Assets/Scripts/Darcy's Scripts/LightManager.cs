using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class LightManager : MonoBehaviour
{
    public int lightCount = 0;

    public GameObject[] feedbackLights, visibleLights;

    public void TurnOffAllLights()
    {
        foreach(var l in visibleLights) //this is for puzzle 3, to turn off all the lights at the same time
        {
            l.GetComponent<Light>().enabled = false;
            l.GetComponent<LightControl>().SpriteOff();
            l.GetComponent<LightControl>().FeedbackLightOff();
        }
    }

    public void CountUp()
    {
        lightCount++;
    }

    public void CountDown()
    {
        lightCount--;
    }
}
