using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRControllerSettings : MonoBehaviour
{
    GameObject vrControllerObject;
    [Header("Eye Position Transforms")]
    //Player Camera Transforms
    public Transform playerNeckAxle;
    public Transform playerCentreEyeCamera;
    public Transform playerLeftEyeCamera;
    public Transform playerRightEyeCamera;

    //Visual Adjustment Variables
    [Header("Eye Position Adjustors")]
    [Space(10)]
    [Range(0, 1)] public float eyeDistInterpolator;

    [Space(7)]
    public float dualEyeXMinFromCentre;
    public float dualEyeXMaxFromCentre;
    [SerializeField] float dualEyeXPresentFromCentre;

    //Player Height Variables
    [Header("Player Height Adjustment (DOES NOTHING PRESENTLY)")]
    [Space(10)]
    public float minHeight; // - Minimum allowed height
    public float maxHeight; // - Maximum allowed height
    public float metreScaleRatio; // - Modifier to ensure resulting height change is accurate to the world scale

    [SerializeField] float currentHeight; // - Current height
    [Range(0,1)] public float heightInterpolator; // - Slider to scale height between minimum and maximum heights

    // Start is called before the first frame update
    void Start()
    {
        //Probably should get relative height from somewhere, given the controller is instantiated

    }

    // Update is called once per frame
    void Update()
    {
        //Height Changes
        currentHeight = Mathf.Lerp(minHeight, maxHeight, heightInterpolator);
    }

    /// <summary>
    /// This is used to appropriately set player height, intended to make
    /// certain that the player does not feel any sickness from the expansion/contraction
    /// </summary>
    public void SetHeight()
    {

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
