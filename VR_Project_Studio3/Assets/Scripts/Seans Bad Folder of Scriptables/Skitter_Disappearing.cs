using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skitter_Disappearing : MonoBehaviour
{
    public GameObject tankSkitter;
    public GameObject biomass;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameController"))
        {
            tankSkitter.SetActive(false);
            biomass.SetActive(true);
        }
    }
}
