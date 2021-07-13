using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerSettings : MonoBehaviour
{
    public bool heightScaleInUpdate = false;
    VRRig vRRig;
    VRFootIK vRFoot;
    GameObject vrControllerObject;
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
    [Header("Player Height Adjustment")]
    [Space(10)]
    public float defaultHeight;
    public float minBodyHeight; // - Minimum allowed height of actual body
    public float maxBodyHeight; // - Maximum allowed height of actual body
    public float intendedBodyHeight;
    [SerializeField] float bodyScale;
    public float scaleMultiplier = 1;
    Vector3 baseFootOffset;
    [Range(0,1)] public float heightInterpolator; // - Slider to scale height between minimum and maximum heights


    // Start is called before the first frame update
    void Start()
    {
        //Probably should get relative height from somewhere, given the controller is instantiated
        vrControllerObject = gameObject;
        vRRig = GetComponentInChildren<VRRig>();
        vRFoot = GetComponentInChildren<VRFootIK>();
        baseFootOffset = vRFoot.footOffset;
    }

    // Update is called once per frame
    void Update()
    {
        //Height Changes
        if (heightScaleInUpdate)
        {
            SetHeight();
        }
    }

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

    /// <summary>
    /// Allows you to edit a single axis on a Vector3, leaving the other two alone.
    /// </summary>
    /// <param name="vectorToChange">The Vector3 to be edited.</param>
    /// <param name="pos">The position of the axis to edit. (0,1,2) = (x,y,z)</param>
    /// <param name="newValue">New value for chosen axis.</param>
    /// <returns>Vector3 with chosen axis changed.</returns>
    Vector3 VectorThreeAxisSwap(Vector3 vectorToChange, int pos, float newValue)
    {
        Vector3 vectorResult = vectorToChange;

        vectorResult[pos] = newValue;

        return vectorResult;
    }
}
