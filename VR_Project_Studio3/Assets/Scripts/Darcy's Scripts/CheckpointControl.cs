using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

//Written by Darcy Glover

public class CheckpointControl : MonoBehaviour
{
    static CheckpointControl instance;

    [SerializeField]
    GameObject vrPlayer;

    [HideInInspector]
    public Vector3 lastCheckpointPos;

    GameObject[] turnedOnLights = new GameObject[2];

    PhotonView photonView;

    void Awake()
    {
        photonView = GetComponent<PhotonView>();
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(vrPlayer == null) //assigning the vr aplyer once they have been instantiated
        {
            vrPlayer = GameObject.Find("First Person Controller(Clone)");
        }
        if(photonView == null)
        {
            Awake();
        }
    }

    public void LoadCheckpoint()
    {
        SaveLights();
        photonView.RPC("RPC_LoadCheckpoint", RpcTarget.All, lastCheckpointPos, turnedOnLights);
    }

    void SaveLights()
    {
        GameObject[] lights = GameObject.Find("Lights").GetComponent<LightCounter>().lights;
        int y = 0;
        for (int i = 0; i < lights.Length; i++)
        {
            y++;
            if (lights[i].GetComponent<Light>().enabled)
            {
                for(int x = 0; x < 2; x++)
                {
                    if(turnedOnLights[x] == null)
                    {
                        turnedOnLights[x] = lights[i];
                        break;
                    }
                }
            }
            Debug.Log(y.ToString());
        }
    }

    [PunRPC]
    void RPC_LoadCheckpoint(Vector3 lastCheckpointPos, GameObject[] turnedOnLights)
    {
        vrPlayer.transform.position = lastCheckpointPos;
        Debug.Log("loaded");
    }
}
