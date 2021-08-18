using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skitter_Disappearing : MonoBehaviour
{
    public GameObject tankSkitter;
    public GameObject biomass;
    public GameObject airbagColliderBox;
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        SkitterDisappear();
    //    }
    //}

    public void SkitterDisappear()
    {
        tankSkitter.SetActive(false);
        biomass.SetActive(true);
        airbagColliderBox.SetActive(true);
    }
}
