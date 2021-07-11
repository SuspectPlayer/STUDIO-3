using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class LightCounter : MonoBehaviour
{
    public int lightCount = 0;

    [SerializeField]
    GameObject[] lightsOn, lightsOff;

    void Update()
    {
        switch (lightCount) //controls the lights on the map to be turned on and off depending on how many lights are on
        {
            case 0:
                {
                    lightsOn[0].SetActive(false);
                    lightsOff[0].SetActive(true);
                    lightsOn[1].SetActive(false);
                    lightsOff[1].SetActive(true);
                    break;
                }
            case 1:
                {
                    lightsOn[0].SetActive(true);
                    lightsOff[0].SetActive(false);
                    lightsOn[1].SetActive(false);
                    lightsOff[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    lightsOn[0].SetActive(true);
                    lightsOff[0].SetActive(false);
                    lightsOn[1].SetActive(true);
                    lightsOff[1].SetActive(false);
                    break;
                }
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
