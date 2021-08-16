using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptControl : MonoBehaviour
{
    [SerializeField]
    GameObject prompt;

    public void TurnOnPrompt()
    {
        if(FindObjectOfType<GameSetup>().isVRPlayer)
        {
            prompt.SetActive(true);
        }
    }

    public void TurnOffPrompt()
    {
        if (FindObjectOfType<GameSetup>().isVRPlayer)
        {
            prompt.SetActive(false);
        }
    }
}
