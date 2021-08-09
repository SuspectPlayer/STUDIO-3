//Written by Jack
using System.Collections;
using UnityEngine;

public class WhiskyPuzzle1_Adjustments : MonoBehaviour
{
    public LightControl light1Control;
    public RandomiseSymbols puzzle1SymbolRandomiser;

    public void TurnOnLight1()
    {
        if (light1Control.gameObject.GetComponentInParent<LightManager>().lightCount == 2) //limited to 2 lights on at any time.
        {
            //nothing will be sent back if the light is off as it can only be turned off, not on at this point
            //Check is needed as couroutine needs to stop if the light is turned off
            if (light1Control.assignedButton.image.sprite == light1Control.lightOn) //checks for the sprite first, to see if its on or not.
            {
                //The light will be turned off
                light1Control.LightParameterCheck();
                StopCoroutine("TurnOffAllLights");
                puzzle1SymbolRandomiser.PickUniqueRandomNumbers();
                puzzle1SymbolRandomiser.ApplySymbols();
            }

        }
        else if (light1Control.gameObject.GetComponentInParent<LightManager>().lightCount < 2)
        {
            if (light1Control.assignedButton.image.sprite == light1Control.lightOn)
            {
                //The light will be turned off
                light1Control.LightParameterCheck();
                StopCoroutine("TurnOffAllLights");
                puzzle1SymbolRandomiser.PickUniqueRandomNumbers();
                puzzle1SymbolRandomiser.ApplySymbols();
            }
            else
            {
                //The light will be turned on
                light1Control.LightParameterCheck();
                StartCoroutine("TurnOffAllLights");
            }
        }
    }

    IEnumerator TurnOffAllLights()
    {
        yield return new WaitForSeconds(25);
        FindObjectOfType<LightManager>().TurnOffAllLights();
        puzzle1SymbolRandomiser.PickUniqueRandomNumbers();
        puzzle1SymbolRandomiser.ApplySymbols();
    }

}
