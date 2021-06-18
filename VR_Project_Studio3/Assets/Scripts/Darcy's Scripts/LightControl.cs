using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class LightControl : MonoBehaviour
{
    [SerializeField]
    Button assignedButton;

    int lightsOn = 0;

    Color alphaControl = Color.white;

    public void TurnLightOn()
    {
        if(assignedButton.image.color.a < 1 && lightsOn <= 2)
        {
            gameObject.SetActive(true);
            lightsOn++;
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
        lightsOn--;
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
