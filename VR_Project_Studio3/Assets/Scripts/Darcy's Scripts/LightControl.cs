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
    Material lightOnMat;

    [SerializeField]
    Sprite lightOn, lightOff;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void LightParameterCheck() //checks all the parameters to decide which method to use
    {
        if(GetComponentInParent<LightCounter>().lightCount == 2) //limited to 2 lights on at any time.
        {
            if (assignedButton.image.sprite == lightOn) //checks for the sprite first, to see if its on or not.
            {
                photonView.RPC("RPC_TurnLightOff", RpcTarget.All, lightOn, lightOff);
            }
        }
        else if (GetComponentInParent<LightCounter>().lightCount < 2) 
        {
            if(assignedButton.image.sprite == lightOn)
            {
                photonView.RPC("RPC_TurnLightOff", RpcTarget.All, lightOn, lightOff);
            }
            else 
            {
                photonView.RPC("RPC_TurnLightOn", RpcTarget.All, lightOn, lightOnMat);
            }
        }
    }

    [PunRPC]
    void RPC_TurnLightOn(Sprite lightOn, Material lightOnMat) 
    {
        GetComponent<Light>().enabled = true;
        GetComponentInParent<LightCounter>().CountUp();
        for(int i = 0; i < 2; i++) //controlling the lights that appear on the top right of the map for feedback to the player
        {
            if (GetComponentInParent<LightCounter>().lights[i].GetComponent<Image>().sprite == lightOn)
            {
                continue;
            }
            else
            {
                GetComponentInParent<LightCounter>().lights[i].GetComponent<Image>().sprite = lightOn;
                GetComponentInParent<LightCounter>().lights[i].GetComponent<Image>().material = lightOnMat;
            }
        }
        SpriteOn();
    }

    [PunRPC]
    void RPC_TurnLightOff(Sprite lightOn, Sprite lightOff)
    {
        GetComponent<Light>().enabled = false; 
        GetComponentInParent<LightCounter>().CountDown();
        for(int i = 0; i < 2; i++) //controlling the lights that appear on the top right of the map for feedback to the player
        {
            if (GetComponentInParent<LightCounter>().lights[i].GetComponent<Image>().sprite == lightOn)
            {
                GetComponentInParent<LightCounter>().lights[i].GetComponent<Image>().sprite = lightOff;
                GetComponentInParent<LightCounter>().lights[i].GetComponent<Image>().material = null;
                break;
            }
        }
        SpriteOff();
    }

    void SpriteOn() //controls the sprites for feedback to the pc player
    {
        assignedButton.image.sprite = lightOn;
        assignedButton.image.material = lightOnMat;
    }
    void SpriteOff()
    {
        assignedButton.image.sprite = lightOff;
        assignedButton.image.material = null;
    }
}
