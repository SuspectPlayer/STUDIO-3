using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }

    public void DynamicTrackingOffset(Vector3 trackingDifference, bool x, bool y, bool z)
    {
        if(x) trackingPositionOffset.x = (Mathf.Round(trackingDifference.x * 100.0f) * 0.01f);
        if(y) trackingPositionOffset.y = (Mathf.Round(trackingDifference.y * 100.0f) * 0.01f);
        if(z) trackingPositionOffset.z = (Mathf.Round(trackingDifference.z * 100.0f) * 0.01f);



    }
}
public class VRRig : MonoBehaviour
{
    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    [Space(10)]
    public Vector3 headBodyOffset;
    public Transform headConstraint;
    public Vector3 currentHeadBodyOffset;
    public float turnSmoothness = 1f;

    void Start()
    {
        //currentHeadBodyOffset = transform.position - headConstraint.position;
        currentHeadBodyOffset = headBodyOffset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = headConstraint.position + currentHeadBodyOffset;
        //transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        transform.forward = Vector3.Lerp(transform.forward, Vector3.ProjectOnPlane(headConstraint.forward, Vector3.up).normalized, Time.deltaTime * turnSmoothness);


        head.Map();
        leftHand.Map();
        rightHand.Map();
    }

    
}
