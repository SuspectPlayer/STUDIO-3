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

    //Color alphaControl = Color.white;

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
                GetComponentInParent<LightCounter>().CountDown();
                SpriteOff();
                //photonView.RPC("RPC_TurnLightOff", RpcTarget.All);
            }
        }
        else if (GetComponentInParent<LightCounter>().lightCount < 2) 
        {
            if(assignedButton.image.sprite == lightOn)
            {
                GetComponentInParent<LightCounter>().CountDown();
                SpriteOff();
                //photonView.RPC("RPC_TurnLightOff", RpcTarget.All);
            }
            else 
            {
                GetComponentInParent<LightCounter>().CountUp();
                SpriteOn();
                //photonView.RPC("RPC_TurnLightOn", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    void RPC_TurnLightOn() 
    {
        GetComponent<Light>().enabled = true;
        //if (gameObject.tag == "Inside") //if the light is an inside one, it needs to have its mesh changed
        //{
        //    GetComponentInChildren<MeshRenderer>().materials[1] = lightOn;
        //}
        GetComponentInParent<LightCounter>().CountUp();
        SpriteOn();
    }

    [PunRPC]
    void RPC_TurnLightOff()
    {
        GetComponent<Light>().enabled = false; 
        //if (gameObject.tag == "Inside") //if the light is an inside one, it needs to have its mesh changed
        //{
        //    GetComponentInChildren<MeshRenderer>().materials[1] = lightOff;
        //}
        GetComponentInParent<LightCounter>().CountDown();
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
