using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseUIActivator : MonoBehaviour
{
    public GameObject wristUIObject;
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
    [SerializeField] bool xAngleInBounds = false;
    [SerializeField] bool yAngleInBounds = false;
    [SerializeField] bool zAngleInBounds = false;

    [SerializeField] bool isPoseTrue = false;

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

        wristUIObject.SetActive(isPoseTrue);
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
}
