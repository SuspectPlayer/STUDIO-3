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

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void TurnLightOn()
    {
        photonView.RPC("RPC_TurnLightOn", RpcTarget.All);
    }

    [PunRPC]
    void RPC_TurnLightOn()
    {
        if (assignedButton.image.color.a < 1 && gameObject.GetComponentInParent<LightCounter>().lightCount < 2)
        {
            gameObject.GetComponent<Light>().enabled = true;
            gameObject.GetComponentInParent<LightCounter>().CountUp();
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
        gameObject.GetComponent<Light>().enabled = false;
        gameObject.GetComponentInParent<LightCounter>().CountDown();
        AlphaDown();
    }

    void AlphaUp()
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
