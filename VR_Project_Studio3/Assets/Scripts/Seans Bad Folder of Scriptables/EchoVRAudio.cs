// Script written by Sean Casey

using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EchoVRAudio : MonoBehaviour
{
    public StudioEventEmitter[] audiofiles;
    [Tooltip("The tag of the direct interaction controllers")] public string triggerTag;

    PhotonView photonView;

    bool isVRPlayer;
    
    public bool audioTog;

    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        isVRPlayer = GameObject.Find("GameSetup").GetComponent<GameSetup>().isVRPlayer;

        if (!isVRPlayer) //only plays sounds for the vr player
        {
            photonView.RPC("RPC_AmbiStart", RpcTarget.Others);
        }
    }

    private void Update()
    {
        if(photonView == null)
        {
            Start();
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag(triggerTag))
    //    {
    //        PlayingAmbience();
    //    }
    //}

    //public void ToggleCheck()
    //{
    //    Debug.Log("made it this far");
    //    if(audioTog == true)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //public void PlayingAmbience()
    //{
    //    Debug.Log("audio collider");
    //    audioTog = true;
    //    foreach (StudioEventEmitter s in audiofiles)
    //    {
    //        s.Play();
    //    }
    //    ToggleCheck();
    //}

    [PunRPC]
    void RPC_AmbiStart()
    {
        StartCoroutine("AmbiStart");
    }

    IEnumerator AmbiStart()
    {
        yield return new WaitForSeconds(1f);
        foreach (StudioEventEmitter s in audiofiles)
        {
            s.Play();
        }
    }
}
