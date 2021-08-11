// Written by Sean Casey

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class echoDoorSounds : MonoBehaviour
{

    public Animator[] doorAnims;
    public StudioEventEmitter[] doorOpenSounds; //When right answer
    private GameObject vrPlayer;


    private void Start()
    {
        vrPlayer = GameObject.Find("VR Player (XR Rig)");
        MoveToPlayer();
    }


    private void Update()
    {
        if(GameObject.Find("GameSetup").GetComponent<GameSetup>().isVRPlayer)
        {
            if (doorAnims[0].GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            {
                doorOpenSounds[0].Play();
            }
            if (doorAnims[1].GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            {
                doorOpenSounds[1].Play();
            }
            if (doorAnims[2].GetCurrentAnimatorStateInfo(0).IsName("DoorOpen"))
            {
                doorOpenSounds[2].Play();
            }
            if (doorAnims[0].GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
            {
                doorOpenSounds[3].Play();
            }
            if (doorAnims[1].GetCurrentAnimatorStateInfo(0).IsName("DoorClose"))
            {
                doorOpenSounds[4].Play();
            }
        }
    }

    public void MoveToPlayer()
    {
        gameObject.transform.parent = vrPlayer.transform;
    }

}