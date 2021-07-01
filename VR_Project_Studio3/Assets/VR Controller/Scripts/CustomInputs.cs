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

            if (device.isValid && customInputs.Count < 0)
            {
                Debug.Log("Step1");
                foreach (GeneralButtonAction action in customInputs)
                {
                    if (action.actionButton != InputHelpers.Button.None)
                    {
                        Debug.Log(action.actionButton);
                        if (device.IsPressed(action.actionButton, out input, 0.1f) && input)
                        {
                            action.actionBehavior.Invoke();
                            Debug.Log(action.actionName + " has been invoked via " + device.name + "'s " + action.actionButton);
                            return;
                        }
                    }
                    else
                    {
                        Debug.LogWarning(action.actionName + " has no assigned action button");
                        return;
                    }
                }
            }
            else if (customInputs.Count == 0)
            {
                Debug.LogWarning("No custom button behaviours found on: " + device.name);
            }
            else if (!device.isValid)
            {
                Debug.LogWarning(device.name + " is not valid, brothuuur");
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
