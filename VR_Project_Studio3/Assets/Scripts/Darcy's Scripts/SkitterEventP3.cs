using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Written by Darcy Glover

public class SkitterEventP3 : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField]
    GameObject dashboard, mesh;

    bool canMove = false, wait = false;
    [HideInInspector]
    public bool playersLose = false;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (canMove) //starts the co-routine. skitter can ony be active if the trigger is entered.
        {
            StartCoroutine("SkitterAnimation");
        }
        if (wait)
        {
            StartCoroutine("WaitForDoorUnlock");
        }

        if (photonView == null)
        {
            Start();
        }
    }

    public void SpawnSkitter() //this activates the skitter from the trigger being tripped
    {
        photonView.RPC("RPC_SpawnSkitter", RpcTarget.All);
    }

    public void PlayersLose() //event for the end of the animation, once animation is done, players lose
    {
        playersLose = true;
    }

    IEnumerator SkitterAnimation() //checking the skitter variables for win/lose
    {
        Debug.Log("anime");
        if(playersLose) 
        {
            photonView.RPC("RPC_PlayersLose", RpcTarget.All);
        }

        if (dashboard.GetComponent<DoorControl>().doorTwoLocked) //if the players manage to close the door, the script waits for them to open the door again to move on to the final door.
        {
            photonView.RPC("RPC_WaitStart", RpcTarget.All);
        }
        yield return null;
    }

    IEnumerator WaitForDoorUnlock()
    {
        if (!dashboard.GetComponent<DoorControl>().doorTwoLocked)
        {
            photonView.RPC("RPC_PlayersWin", RpcTarget.All);
        }
        yield return null;
    }


    [PunRPC]
    void RPC_SpawnSkitter()
    {
        mesh.GetComponent<SkinnedMeshRenderer>().enabled = true;
        canMove = true;
        GetComponent<Animator>().SetBool("canMove", true);
    }

    [PunRPC]
    void RPC_PlayersLose()
    {
        Debug.Log("lose");
        canMove = false;
        StopAllCoroutines();
        GetComponent<Animator>().SetBool("canMove", false);
        GameObject.Find("6 - Checkpoints").GetComponent<CheckpointControl>().LoadCheckpoint(); //loading the checkpoint
        mesh.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }

    [PunRPC]
    void RPC_WaitStart()
    {
        StopAllCoroutines();
        canMove = false;
        wait = true;
    }

    [PunRPC]
    void RPC_PlayersWin()
    {
        mesh.GetComponent<SkinnedMeshRenderer>().enabled = false; //if the door is locked, the players are safe
        dashboard.GetComponent<DoorControl>().door = GameObject.Find("Door 3"); //only sets to the third door if the skitter event is done
        wait = false;
        StopAllCoroutines();
    }
}
