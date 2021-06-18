using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class LightControl : MonoBehaviour
{
    [SerializeField]
    Button assignedButton;

    Color alphaControl = Color.white;

    public void TurnLightOn()
    {
        if(assignedButton.image.color.a < 1)
        {
            gameObject.SetActive(true);
            AlphaUp();
        }
        else
        {
            TurnLightOff();
        }
    }

    void TurnLightOff()
    {
        gameObject.SetActive(false);
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
