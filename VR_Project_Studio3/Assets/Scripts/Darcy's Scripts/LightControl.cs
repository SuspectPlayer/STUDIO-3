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

    Color alphaControl = Color.white;

    [SerializeField]
    Material lightOn, lightOff;

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void TurnLightOn()
    {
        Debug.Log("click");
        photonView.RPC("RPC_TurnLightOn", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnLightOn() //checks for the alpha first, if the alpha is half it means the light is off
    {
        if (assignedButton.image.color.a < 1 && gameObject.GetComponentInParent<LightCounter>().lightCount < 2) //limited to 2 lights on at any time.
        {
            GetComponent<Light>().enabled = true;
            if(gameObject.name.Contains("Inside")) //if the light is an inside one, it needs to have its mesh changed
            {
                GetComponentInChildren<MeshRenderer>().materials[1] = lightOn;
            }
            GetComponentInParent<LightCounter>().CountUp();
            AlphaUp();
        }
        else if (assignedButton.image.color.a == 1)
        {
            photonView.RPC("RPC_TurnLightOff", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_TurnLightOff()
    {
        GetComponent<Light>().enabled = false; 
        if (gameObject.name.Contains("Inside")) //if the light is an inside one, it needs to have its mesh changed
        {
            GetComponentInChildren<MeshRenderer>().materials[1] = lightOff;
        }
        GetComponentInParent<LightCounter>().CountDown();
        AlphaDown();
    }

    void AlphaUp() //controls the alpha for feedback to the pc player
    {
        alphaControl.a = 1f;
        assignedButton.image.color = alphaControl;
    }
    void AlphaDown()
    {
        alphaControl.a = 0.5f;
        assignedButton.image.color = alphaControl;
    }
}
