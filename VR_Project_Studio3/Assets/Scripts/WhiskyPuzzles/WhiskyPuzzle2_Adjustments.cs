//Written by Jack
using System.Collections;
using UnityEngine;

public class WhiskyPuzzle2_Adjustments : MonoBehaviour
{
    public LightControl light3Control;
    public LightControl light4Control;
    public RandomiseSymbols puzzle2SymbolRandomiser;
    bool countdownActive;

    public void TurnOnLight3()
    {
        if (light3Control.gameObject.GetComponentInParent<LightManager>().lightCount == 2) //limited to 2 lights on at any time.
        {
            //nothing will be sent back if the light is off as it can only be turned off, not on at this point
            //Check is needed as couroutine needs to stop if the light is turned off
            if (light3Control.assignedButton.image.sprite == light3Control.lightOn) //checks for the sprite first, to see if its on or not.
            {
                //The light will be turned off
                light3Control.LightParameterCheck();
                if (light3Control.assignedButton.image.sprite != light3Control.lightOn)
                {
                    StopCoroutine("TurnOffAllLights");
                    countdownActive = false;
                }
            }

        }
        else if (light3Control.gameObject.GetComponentInParent<LightManager>().lightCount < 2)
        {
            if (light3Control.assignedButton.image.sprite == light3Control.lightOn)
            {
                //The light will be turned off
                light3Control.LightParameterCheck();
                if (light4Control.assignedButton.image.sprite != light3Control.lightOn)
                {
                    StopCoroutine("TurnOffAllLights");
                    countdownActive = false;
                }

            }
            else 
            {
                //The light will be turned on
                puzzle2SymbolRandomiser.PickUniqueRandomNumbers();
                puzzle2SymbolRandomiser.ApplySymbols();
                light3Control.LightParameterCheck();
                if (!countdownActive)
                {
                    StartCoroutine("TurnOffAllLights");
                }
            }
        }
    }

    public void TurnOnLight4()
    {
        if (light4Control.gameObject.GetComponentInParent<LightManager>().lightCount == 2) //limited to 2 lights on at any time.
        {
            //nothing will be sent back if the light is off as it can only be turned off, not on at this point
            //Check is needed as couroutine needs to stop if the light is turned off
            if (light4Control.assignedButton.image.sprite == light4Control.lightOn) //checks for the sprite first, to see if its on or not.
            {
                //The light will be turned off
                light4Control.LightParameterCheck();
                if (light3Control.assignedButton.image.sprite != light3Control.lightOn)
                {
                    StopCoroutine("TurnOffAllLights");
                    countdownActive = false;
                }
            }

        }
        else if (light4Control.gameObject.GetComponentInParent<LightManager>().lightCount < 2)
        {
            if (light4Control.assignedButton.image.sprite == light4Control.lightOn)
            {
                //The light will be turned off
                light4Control.LightParameterCheck();
                if (light3Control.assignedButton.image.sprite != light3Control.lightOn)
                {
                    StopCoroutine("TurnOffAllLights");
                    countdownActive = false;
                }
            }
            else
            {
                //The light will be turned on
                puzzle2SymbolRandomiser.PickUniqueRandomNumbers();
                puzzle2SymbolRandomiser.ApplySymbols();
                light4Control.LightParameterCheck();
                if (!countdownActive)
                {
                    StartCoroutine("TurnOffAllLights");
                }

            }
        }
    }

    IEnumerator TurnOffAllLights()
    {
        countdownActive = true;
        yield return new WaitForSeconds(25);
        FindObjectOfType<LightManager>().TurnOffAllLights();
        puzzle2SymbolRandomiser.PickUniqueRandomNumbers();
        puzzle2SymbolRandomiser.ApplySymbols();
        countdownActive = false;
    }

}
