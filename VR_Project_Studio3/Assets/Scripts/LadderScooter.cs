using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LadderScooter : MonoBehaviour
{
    [SerializeField] Transform player;

    Color yee = Color.black;

    [SerializeField] LayerMask laymak;

    [SerializeField] Transform ladderTopPortLoc;
    [SerializeField] Transform ladderBottomPortLoc;

    Transform fpsCam;

    //Blackout
    [SerializeField] Image blackoutScreen;

    float time = 0f;

    float timeywimey = 2f;

    float alfMin = 0;
    float alfMax = 255;

    float alfAlfa;

    bool transferring = false;
    void Start()
    {
        if(FindObjectOfType<GameSetup>().isFlatScreen)
        {
            player = GameObject.Find("First Person Controller(Clone)").GetComponent<Transform>();
            fpsCam = GameObject.Find("FPSCam").GetComponent<Transform>();
        }
        else
        {
            player = GameObject.Find("VR Controller (XR Rig)(Clone)").GetComponent<Transform>();
        }
    }
    private void Update()
    {
        if(FindObjectOfType<GameSetup>().isFlatScreen)
        {
            RaycastHit hit;

            if(Physics.Raycast(fpsCam.position, fpsCam.forward, out hit, 4f, laymak, QueryTriggerInteraction.Collide))
            {
                if(Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Top"))
                {
                    if (!transferring)
                    StartCoroutine(Blackout(ladderBottomPortLoc));
                }
                else if (Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Bottom"))
                {
                    if (!transferring)
                    StartCoroutine(Blackout(ladderTopPortLoc));
                }
            }
        }
    }

    public void VRBopperoo(Transform potato)
    {
        if(!transferring)
        StartCoroutine(Blackout(potato));
        transferring = true;
    }
    IEnumerator Blackout(Transform potato)
    {
        transferring = true;
        while (time < timeywimey)
        {
            alfAlfa = Mathf.Lerp(alfMin, alfMax, time/timeywimey);
            time += Time.deltaTime;
            yee.a = alfAlfa;
            blackoutScreen.color = yee;

            yield return null;
        }

        player.position = potato.position;

        while (time > 0)
        {
            alfAlfa = Mathf.Lerp(alfMin, alfMax, time / timeywimey);
            time -= Time.deltaTime;
            yee.a = alfAlfa;
            blackoutScreen.color = yee;

            yield return null;
        }
        transferring = false;
    }

}
