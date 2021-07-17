using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.OpenXR;
using UnityEngine.UI;

public class InitialiseIfVR : MonoBehaviour
{
    public GameObject firstPersonCampbellsSoup;
    public GameObject vrCampbellsSoup, keyboardHolder, keyboardManager;
    [Space(10)]
    public Canvas canvas;
    public Camera fpsCamera;
    public Camera vrCamera;

    void Awake()
    {
        bool gotHeadset = HMDExists.XRPresent();

        canvas.worldCamera = EventCamera(gotHeadset);

        Activation(gotHeadset);
    }

    public Camera EventCamera(bool headset)
    {
        if(headset)
        {
            return vrCamera;
        }
        else
        {
            return fpsCamera;
        }
    }

    public void Activation(bool headset)
    {
        firstPersonCampbellsSoup.SetActive(!headset);
        vrCampbellsSoup.SetActive(headset);
        keyboardHolder.SetActive(headset);
        keyboardManager.SetActive(headset);
    }
}

static class HMDExists
{
    public static bool XRPresent()
    {
        List<XRDisplaySubsystem> subsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetSubsystems(subsystems);


        foreach (XRDisplaySubsystem instance in subsystems)
        {
            if (instance.running)
            {
                Debug.Log("HMD found");
                return true;
            }
        }
        Debug.Log("HMD not found");
        return false;
    }
}
