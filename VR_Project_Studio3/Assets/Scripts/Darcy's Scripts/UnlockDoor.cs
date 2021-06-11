using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    GameObject doorLight;

    [SerializeField]
    Material red, green;

   public void UnlockDoorMethod()
   {
      doorLight.GetComponent<MeshRenderer>().material = green;
   }
}
