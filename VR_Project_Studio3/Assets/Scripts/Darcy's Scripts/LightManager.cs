using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class LightManager : MonoBehaviour
{
    public int lightCount = 0;

    public GameObject[] feedbackLights, visibleLights;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void TurnOffAllLights()
    {
        photonView.RPC("RPC_TurnOffAllLights", RpcTarget.All);
    }

    public void CountUp()
    {
        lightCount++;
    }

    public void CountDown()
    {
        lightCount--;
    }

    [PunRPC]
    void RPC_TurnOffAllLights()
    {
        Debug.Log("turning off");
        for (int i = 0; i < visibleLights.Length; i++) //this is for puzzle 3, to turn off all the lights at the same time
        {
            if (!visibleLights[i].GetComponent<Light>())
            {
                visibleLights[i].GetComponentInChildren<Light>().enabled = false;
            }
            else
            {
                visibleLights[i].GetComponent<Light>().enabled = false;
            }
            visibleLights[i].GetComponent<LightControl>().SpriteOff();
            visibleLights[i].GetComponent<LightControl>().FeedbackLightOff();
            lightCount = 0;
        }
    }
}
