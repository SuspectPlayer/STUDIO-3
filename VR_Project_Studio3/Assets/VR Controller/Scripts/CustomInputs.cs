using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;
public class CustomInputs : MonoBehaviour
{
    List<InputDevice> inputDevices;

    public XRNode node;
    [Space(10)]
    public List<GeneralButtonAction> customInputs = new List<GeneralButtonAction>();

    private void Awake()
    {
        inputDevices = new List<InputDevice>();
    }

    // Update is called once per frame
    void Update()
    {
        //Listens for inputs on devices specified by Node

        InputDevices.GetDevicesAtXRNode(node, inputDevices);

        foreach (InputDevice device in inputDevices)
        {
            Debug.Log("Device found with name: " + device.name);
            bool input;

            if (customInputs.Count != 0)
            {
                foreach (GeneralButtonAction action in customInputs)
                {
                    if (action.actionButton != InputHelpers.Button.None)
                    {
                        if (device.IsPressed(action.actionButton, out input) && input)
                        {
                            action.actionBehavior.Invoke();
                        }
                    }
                    else
                    {
                        Debug.LogWarning(action.actionName + " has no assigned action button");
                    }
                }
            }
            else
            {
                Debug.LogWarning("No custom button behaviours found");
            }
        }
    }
}

[System.Serializable]
public class GeneralButtonAction //Stores Varables for the creation of custom button actions
{
    public string actionName;
    public InputHelpers.Button actionButton;
    public UnityEvent actionBehavior;
}
