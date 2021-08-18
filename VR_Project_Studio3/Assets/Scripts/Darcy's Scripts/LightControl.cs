using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//Written by Darcy Glover

public class LightControl : MonoBehaviour
{ 
    public Button assignedButton;

    [SerializeField]
    Material lightOnMat, defaultMat;

    [SerializeField]
    Material[] emissionOn, emissionOff, beaconEmissionOn, beaconEmissionOff;

    public Sprite lightOn;

    [SerializeField]
    Sprite lightOff;

    [SerializeField]
    GameObject[] assignedSymbols;

    [SerializeField]
    GameObject glowstick;

    PhotonView photonView;

    public int lightUsed = 0;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        StartCoroutine(CheckForLightOn());
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
        lightUsed++;

        if (!GetComponent<Light>()) //checking to see if the light component is on the object or its children
        {
            GetComponentInChildren<Light>().enabled = true;
        }
        else
        {
            GetComponent<Light>().enabled = true;
        }

        if(assignedSymbols.Length > 0)
        {
            for (int i = 0; i < assignedSymbols.Length; i++) //this loop enables the symbols to only been seen when the light is on
            {
                assignedSymbols[i].GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (gameObject.tag == "Beacon")
        {
            glowstick.GetComponent<MeshRenderer>().materials = beaconEmissionOn;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().materials = emissionOn;
        }

        GetComponentInParent<LightManager>().CountUp();
        FeedbackLightOn();
        SpriteOn();
    }

    [PunRPC]
    void RPC_TurnLightOff()
    {
        if (!GetComponent<Light>()) //checking to see if the light component is on the object or its children
        {
            GetComponentInChildren<Light>().enabled = false;
        }
        else
        {
            GetComponent<Light>().enabled = false;
        }

        if(assignedSymbols.Length > 0)
        {
            for (int i = 0; i < assignedSymbols.Length; i++) //this loop enables the symbols to only been seen when the light is on
            {
                assignedSymbols[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        if(gameObject.tag == "Beacon")
        {
            glowstick.GetComponent<MeshRenderer>().materials = beaconEmissionOff;
        }
        else
        {
            GetComponentInChildren<MeshRenderer>().materials = emissionOff;
        }

        GetComponentInParent<LightManager>().CountDown();
        FeedbackLightOff();
        SpriteOff();
    }

    void FeedbackLightOn()
    {
        for (int i = 0; i < 2; i++) //controlling the lights that appear on the top right of the map for feedback to the player
        {
            if (GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<Image>().sprite == lightOn)
            {
                continue;
            }
            else
            {
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<Image>().sprite = lightOn;
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<Image>().material = lightOnMat;
                break;
            }
        }
    }
    public void FeedbackLightOff()
    {
        for (int i = 1; i > -1; i--) //controlling the lights that appear on the top right of the map for feedback to the player
        {
            if (GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<Image>().sprite == lightOn)
            {
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<Image>().sprite = lightOff;
                GetComponentInParent<LightManager>().feedbackLights[i].GetComponent<Image>().material = defaultMat;
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

    IEnumerator CheckForLightOn()
    {
        if(GetComponentInChildren<Light>().enabled)
        {
            assignedButton.image.sprite = lightOn;
        }

        yield return null;
    }
}
