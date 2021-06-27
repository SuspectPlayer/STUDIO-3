using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Written by Darcy Glover

public class FindEventCamera : MonoBehaviour
{
    void Update()
    {
        GetComponent<Canvas>().worldCamera = GameObject.Find("ViewerCam Variant(Clone)").GetComponentInChildren<Camera>();
    }
}
