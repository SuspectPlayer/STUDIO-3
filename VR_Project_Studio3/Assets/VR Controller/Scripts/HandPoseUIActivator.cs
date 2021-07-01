using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseUIActivator : MonoBehaviour
{
    public GameObject wristUIObject;

    public Vector3 minHandAngles;
    public Vector3 maxHandAngles;
    [SerializeField] Vector3 localHand;

    [Space(10)]
    public Transform handController;
    [Space(10)]
    [SerializeField] bool xAngleInBounds = false;
    [SerializeField] bool yAngleInBounds = false;
    [SerializeField] bool zAngleInBounds = false;

    [SerializeField] bool isPoseTrue = false;

    // Update is called once per frame
    void Update()
    {
        localHand = handController.localRotation.eulerAngles;

        if ((handController.localRotation.x >= minHandAngles.x) && (handController.localRotation.x <= maxHandAngles.x)) xAngleInBounds = true;
        if ((handController.localRotation.y >= minHandAngles.y) && (handController.localRotation.y <= maxHandAngles.y)) yAngleInBounds = true;
        if ((handController.localRotation.z >= minHandAngles.z) && (handController.localRotation.z <= maxHandAngles.z)) zAngleInBounds = true;

        if (xAngleInBounds && yAngleInBounds && zAngleInBounds) isPoseTrue = true;

        //xAngleInBounds = (handController.localRotation.x >= minHandAngles.x) && (handController.localRotation.x <= maxHandAngles.x);
        //yAngleInBounds = (handController.localRotation.y >= minHandAngles.y) && (handController.localRotation.y <= maxHandAngles.y);
        //zAngleInBounds = (handController.localRotation.z >= minHandAngles.z) && (handController.localRotation.z <= maxHandAngles.z);

        //isPoseTrue = (xAngleInBounds && yAngleInBounds && zAngleInBounds);

        wristUIObject.SetActive(isPoseTrue);
    }
}
