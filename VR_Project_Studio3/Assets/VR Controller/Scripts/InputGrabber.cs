using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class InputGrabber : MonoBehaviour
{
    List<InputDevice> inputDevices;

    public XRNode node;
    [Space(10)]

    public UnityEvent primaryButtonAction;
    public UnityEvent gripButtonAction;
    public UnityEvent triggerButtonAction;
    public UnityEvent menuButtonAction;
    public UnityEvent secondaryButtonAction;


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
            Debug.Log(device.name);
            bool input;
            if (primaryButtonAction != null)
            {
                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out input) && input)
                {
                    primaryButtonAction.Invoke();
                }
            }
            if (gripButtonAction != null)
            {
                if (device.TryGetFeatureValue(CommonUsages.gripButton, out input) && input)
                {
                    gripButtonAction.Invoke();
                }
            }
            if (triggerButtonAction != null)
            {
                if (device.TryGetFeatureValue(CommonUsages.triggerButton, out input) && input)
                {
                    triggerButtonAction.Invoke();
                }
            }
            if (menuButtonAction != null)
            {
                if (device.TryGetFeatureValue(CommonUsages.menuButton, out input) && input)
                {
                    menuButtonAction.Invoke();
                }
            }
            if (secondaryButtonAction != null)
            {
                if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out input) && input)
                {
                    secondaryButtonAction.Invoke();
                }
            }
        }
    }
}
