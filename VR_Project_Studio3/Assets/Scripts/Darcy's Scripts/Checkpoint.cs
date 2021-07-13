using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Written by Darcy Glover

public class Checkpoint : MonoBehaviour
{
    void Update() //dev tool
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("saved");
            SaveCheckpointPosition();
        }
    }

    public void SaveCheckpointPosition() //making the last checkpoint position equal to this checkpoint's position
    {
       GetComponentInParent<CheckpointControl>().lastCheckpointPos = transform.position;
    }
}
