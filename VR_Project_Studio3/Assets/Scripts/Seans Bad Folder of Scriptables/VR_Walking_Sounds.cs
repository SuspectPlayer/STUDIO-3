using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class VR_Walking_Sounds : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MetalFloor"))
        {
            gameObject.GetComponent<StudioEventEmitter>().Play();
        }
    }
}
