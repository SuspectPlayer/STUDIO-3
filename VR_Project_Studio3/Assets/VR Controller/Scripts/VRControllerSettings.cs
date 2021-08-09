//Written by Jasper von Riegen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRControllerSettings : MonoBehaviour
{
    //References/General variables
    public bool heightScaleInUpdate = false;

    VRRig vRRig;
    VRFootIK vRFoot;
    GameObject vrControllerObject;
    ActionBasedContinuousMoveProvider moveProvider;
    ActionBasedSnapTurnProvider snapProvider;

    public Transform bodySize;
    public Transform bodyCollider;

    [Space(10)]
    [Header("Eye Position Transforms")]
    //Player Head Transform
    public Transform playerNeckAxle;

    //Visual Adjustment Variables
    [Space(10)]
    [Header("Eye Position Adjustors")]
    [Range(0, 1)] public float eyeDistInterpolator;

    [Space(7)]
    public float dualEyeXMinFromCentre;
    public float dualEyeXMaxFromCentre;
    [SerializeField] float dualEyeXPresentFromCentre;

    //Player Height Variables
    [Space(10)]
    [Header("Player Height Adjustment")]
    public float defaultHeight;
    public float minBodyHeight; // - Minimum allowed height of actual body
    public float maxBodyHeight; // - Maximum allowed height of actual body
    public float intendedBodyHeight;

    [SerializeField] float bodyScale;
    public float scaleMultiplier = 1;
    Vector3 baseFootOffset;
    [Range(0,1)] public float heightInterpolator; // - Slider to scale height between minimum and maximum heights

    //Motion Variables
    [Range(0.5f, 4f)]
    public float playerSpeed;
    [Space(10)]
    public int snapAmtOne;
    public int snapAmtTwo;
    public int snapAmtThree;

    bool snapSetOne = true;
    bool snapSetTwo = false;
    bool snapSetThree = false;
    bool snapSetOff = false; // Will confirm if inclusion is desired

    // Start is called before the first frame update
    void Start()
    {
        //Probably should get relative height from somewhere, given the controller is instantiated
        vrControllerObject = gameObject;
        vRRig = GetComponentInChildren<VRRig>();
        vRFoot = GetComponentInChildren<VRFootIK>();
        moveProvider = GetComponentInChildren<ActionBasedContinuousMoveProvider>();
        snapProvider = GetComponentInChildren<ActionBasedSnapTurnProvider>();
        baseFootOffset = vRFoot.footOffset;
    }

    
    void Update()
    {
        //Height Changes
        if (heightScaleInUpdate)
        {
            SetHeight();
        }
    }


    //Methods for settings
    /// <summary>
    /// This is used to appropriately set player height, intended to make
    /// certain that the player does not feel any sickness from the expansion/contraction
    /// </summary>
    public void SetHeight()
    {
        intendedBodyHeight = Mathf.Lerp(minBodyHeight, maxBodyHeight, heightInterpolator);
        
        float headHeight = playerNeckAxle.localPosition.y;
        float scale = headHeight/ defaultHeight;
        bodyScale = scale * scaleMultiplier;
        Vector3 scoil = Vector3.one * bodyScale;
        if (headHeight > 0.2f)
        {
            bodySize.localScale = scoil;
            bodyCollider.localScale = scoil;
            vRRig.currentHeadBodyOffset = vRRig.headBodyOffset * bodyScale;
            vRFoot.footOffset = baseFootOffset * bodyScale;
        }
    }

    public void SetMoveSpeed()
    {
        moveProvider.moveSpeed = playerSpeed;
    }

    public void SetTurnSnap()
    {
        if (snapSetOne) snapProvider.turnAmount = snapAmtOne;
        else if (snapSetTwo) snapProvider.turnAmount = snapAmtTwo;
        else if (snapSetThree) snapProvider.turnAmount = snapAmtThree;
    }

    /// <summary>
    /// Called via button. Used in conjunction with SetTurnSnap() method.
    /// </summary>
    public void SnapOne()
    {
        snapSetOne = true;
        snapSetTwo = false;
        snapSetThree = false;
        snapSetOff = false;
    }

    /// <summary>
    /// Called via button. Used in conjunction with SetTurnSnap() method.
    /// </summary>
    public void SnapTwo()
    {
        snapSetOne = false;
        snapSetTwo = true;
        snapSetThree = false;
        snapSetOff = false;
    }

    /// <summary>
    /// Called via button. Used in conjunction with SetTurnSnap() method.
    /// </summary>
    public void SnapThree()
    {
        snapSetOne = false;
        snapSetTwo = false;
        snapSetThree = true;
        snapSetOff = false;
    }

    /// <summary>
    /// Called via button. Used in conjunction with SetTurnSnap() method. Currently not implemented.
    /// </summary>
    public void SnapOff()
    {
        snapSetOne = false;
        snapSetTwo = false;
        snapSetThree = false;
        snapSetOff = true;
    }

    //Methods for updating settings
    /// <summary>
    /// Method to be called via button. Starts sequence to update VRController settings.
    /// </summary>
    public void SetSettings()
    {
        StartCoroutine(SetSettingsSequence());
    }

    IEnumerator SetSettingsSequence()
    {
        //Activate the blinder
        yield return new WaitForSeconds(1f);
        //Do the Setting Methods
        yield return new WaitForSeconds(1f);
        //Deactivate the blinder
    }
}
