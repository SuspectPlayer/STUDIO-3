using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseUIActivator : MonoBehaviour
{
    public GameObject wristUIObject;
    public UIBehaviours uiBehave;

    private GameSetup gameSetup;

    [SerializeField]private Animator openAnimator;
    [Space(10)]
    Vector3 minHandAngles;
    Vector3 maxHandAngles;
    Vector3 handRelative;
    public Vector3 desiredRelative;
    public Vector3 offset;

    [Space(10)]
    public Transform handController;
    public Transform waist;
    [Space(10)]
    bool xAngleInBounds = false;
    bool yAngleInBounds = false;
    bool zAngleInBounds = false;

    bool isPoseTrue = false;
    bool queueWaiting = false;

    private void Awake()
    {
        gameSetup = GameObject.Find("GameSetup").GetComponent<GameSetup>();

        openAnimator.SetBool("IsVRPlayer", !gameSetup.isFlatScreen);
        openAnimator.SetTrigger("Branch");
    }

    // Update is called once per frame
    void Update()
    {
        handRelative = waist.rotation.eulerAngles - handController.rotation.eulerAngles;

        minHandAngles = desiredRelative - offset;
        maxHandAngles = desiredRelative + offset;

        xAngleInBounds = WithinRange(handRelative.x, minHandAngles.x, maxHandAngles.x) || WithinRange(handRelative.x, minHandAngles.x - 360, maxHandAngles.x - 360);
        yAngleInBounds = WithinRange(handRelative.y, minHandAngles.y, maxHandAngles.y) || WithinRange(handRelative.y, minHandAngles.y - 360, maxHandAngles.y - 360);
        zAngleInBounds = WithinRange(handRelative.z, minHandAngles.z, maxHandAngles.z) || WithinRange(handRelative.z, minHandAngles.z - 360, maxHandAngles.z - 360);

        isPoseTrue = (xAngleInBounds && yAngleInBounds && zAngleInBounds);

        //wristUIObject.SetActive(isPoseTrue);
        if(!queueWaiting)
        {
            StartCoroutine(QueueAnimator());
        }
        
    }

    bool WithinRange(float toCompare, float minLimit, float maxLimit)
    {
        if (toCompare >= minLimit && toCompare <= maxLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EmoteUIActive()
    {
        if (isPoseTrue) openAnimator.SetTrigger("PlayerOpen");
        else openAnimator.SetTrigger("PlayerClose");
    }

    public void WristyBoi() // To Call From Animator; Checks if UI should be open
    {
        if (!isPoseTrue)
        {
            uiBehave.Close();
        }
        else
        {
            uiBehave.OpenToCorrect();
        }
    }

    bool IsAnimatorMoveState()
    {
        bool isMoveState;
        AnimatorStateInfo stateInfo = openAnimator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("VRUIOpening") || stateInfo.IsName("VRUIClosing") || stateInfo.IsName("FPSUIOpening") || stateInfo.IsName("FPSUIClosing"))
            isMoveState = true;
        else isMoveState = false;


        if (openAnimator.IsInTransition(0) || isMoveState)
            return true;
        else
            return false;
    }

    IEnumerator QueueAnimator() // Waits for animations to get to a rest state
    {
        Debug.Log("Queued");
        queueWaiting = true;
        while (IsAnimatorMoveState())
        {
            yield return null;
        }

        EmoteUIActive();
        queueWaiting = false;
    }
}
