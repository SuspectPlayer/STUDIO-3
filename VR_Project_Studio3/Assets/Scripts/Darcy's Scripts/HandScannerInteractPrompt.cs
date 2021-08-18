using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScannerInteractPrompt : MonoBehaviour
{
    PromptControl prompt;

    void OnTriggerEnter(Collider other)
    {
        prompt = FindObjectOfType<PromptControl>();
        if (other.tag == "Player")
        {
            prompt.TurnOnPrompt();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            prompt.TurnOffPrompt();
        }
    }
}
