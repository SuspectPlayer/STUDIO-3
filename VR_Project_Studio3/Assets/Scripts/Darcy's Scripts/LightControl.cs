using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover

public class LightControl : MonoBehaviour
{
    [SerializeField]
    Button assignedButton;

    [SerializeField]
    Material lightOnMat, defaultMat;

    [SerializeField]
    Sprite lightOn, lightOff;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void LightParameterCheck() //checks all the parameters to decide which method to use
    {
        if(GetComponentInParent<LightManager>().lightCount == 2) //limited to 2 lights on at any time.
        {
            if (assignedButton.image.sprite == lightOn) //checks for the sprite first, to see if its on or not.
            {
                photonView.RPC("RPC_TurnLightOff", RpcTarget.All);
            }
        }
        else if (GetComponentInParent<LightManager>().lightCount < 2) 
        {
            if(assignedButton.image.sprite == lightOn)
            {
                photonView.RPC("RPC_TurnLightOff", RpcTarget.All);
            }
            else 
            {
                photonView.RPC("RPC_TurnLightOn", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_TurnLightOn() 
    {
        GetComponent<Light>().enabled = true;
        GetComponentInParent<LightManager>().CountUp();
        FeedbackLightOn();
        SpriteOn();
    }

    [PunRPC]
    void RPC_TurnLightOff()
    {
        GetComponent<Light>().enabled = false; 
        GetComponentInParent<LightManager>().CountDown();
        FeedbackLightOff();
        SpriteOff();
    }

    void FeedbackLightOn()
    {
        for (int i = 0; i < 2; i++) //controlling the lights that appear on the top right of the map for feedback to the player
        {
            if (GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<SpriteRenderer>().sprite == lightOn)
            {
                continue;
            }
            else
            {
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<SpriteRenderer>().sprite = lightOn;
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<SpriteRenderer>().material = lightOnMat;
                break;
            }
        }
    }
    public void FeedbackLightOff()
    {
        for (int i = 1; i > -1; i--) //controlling the lights that appear on the top right of the map for feedback to the player
        {
            if (GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<SpriteRenderer>().sprite == lightOn)
            {
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<SpriteRenderer>().sprite = lightOff;
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<SpriteRenderer>().material = defaultMat;
                break;
            }
        }
    }

    void SpriteOn() //controls the sprites for feedback to the pc player
    {
        assignedButton.image.sprite = lightOn;
        assignedButton.image.material = lightOnMat;
    }
    public void SpriteOff()
    {
        assignedButton.image.sprite = lightOff;
        assignedButton.image.material = defaultMat;
    }
}
