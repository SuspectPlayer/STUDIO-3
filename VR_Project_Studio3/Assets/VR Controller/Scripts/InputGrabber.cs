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
    //[Space(10)]
    //public UnityEvent 

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
            if (device.TryGetFeatureValue(CommonUsages.) )
            {

            }
        }
    }
}
