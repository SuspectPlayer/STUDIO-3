using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class UnlockDoor : MonoBehaviour
{
    [SerializeField]
    GameObject door;

   public void UnlockDoorMethod()
   {
        door.SetActive(true);
   }
}
