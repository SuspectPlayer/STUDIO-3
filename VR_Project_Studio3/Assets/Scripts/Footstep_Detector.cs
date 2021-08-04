//Written by Jack and edited by Sean
using System.Collections;
using UnityEngine;
using FMODUnity;


public class Footstep_Detector : MonoBehaviour
{

    [Range(0f, 2f)]
    public float delay;


    bool isVRPlayer;

    public bool audioTog;

    public StudioEventEmitter metalFootsteps;

    public Transform cam;


    void Awake()
    {
        isVRPlayer = GameObject.Find("GameSetup").GetComponent<GameSetup>().isVRPlayer;

        //Plays the footstep sound when the game starts depending on the surface the player is standing on 
        RaycastHit detect;
        string floortag;
        if (isVRPlayer)
        {
            if (Physics.Raycast(cam.position, Vector3.down, out detect, 3f))
            {

                floortag = detect.collider.tag;

                if (detect.collider.tag == "MetalFloor")
                {
                    metalFootsteps.Play();
                }
                else if (floortag == "badwalking")
                {
                }
            }
        }
    }

    // Use this for initialization
    IEnumerator Start()
    {
        while (true)
        {
            RaycastHit hit = new RaycastHit();
            string floortag;
            if (isVRPlayer)
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    if (Physics.Raycast(cam.position, Vector3.down, out hit, 3f))
                    {
                        floortag = hit.collider.tag;
                        //Plays footsetp sounds depending on the tag of the surface the player is on
                        if (hit.collider.tag == "MetalFloor")
                        {
                            metalFootsteps.Play();
                        }
                        else if (floortag == "badwalking")
                        {
                        }
                    }
                    yield return new WaitForSeconds(1f);
                }
                else
                {
                    yield return 0;
                }
            }

        }


    }
}
