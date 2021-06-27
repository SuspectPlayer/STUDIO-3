using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class LightCounter : MonoBehaviour
{
    public int lightCount = 0;

    public void CountUp()
    {
        lightCount++;
    }

    public void CountDown()
    {
        lightCount--;
    }
}
