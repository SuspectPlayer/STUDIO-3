using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class FindEventCamera : MonoBehaviour
{
    //this script just finds the cloned event camera for the canvases
    void Update()
    {
        GetComponent<Canvas>().worldCamera = GameObject.Find("ViewerCam Variant(Clone)").GetComponentInChildren<Camera>();
    }
}
