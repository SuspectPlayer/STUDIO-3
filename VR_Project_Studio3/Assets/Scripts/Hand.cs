using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Animation
    //Animator animator;
    //public SkinnedMeshRenderer mesh;
    //private float gripTarget;
    //private float triggerTarget;
    //private float gripCurrent;
    //private float triggerCurrent;
    //public float animationSpeed;
    //private string animatorGripParam = "Grip"; 
    //private string animatorTriggerParam = "Trigger";

    // Physics Movement
    [SerializeField] GameObject followObject;
    [SerializeField] float followSpeed = 30f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] Vector3 rotationOffset;
    [SerializeField] Vector3 positionOffset;
    Transform _followTarget;
    Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();

        // Physics Movement
        _followTarget = followObject.transform;
        _body = GetComponent<Rigidbody>();

        _body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _body.interpolation = RigidbodyInterpolation.Interpolate;
        _body.mass = 20f;

        // Teleport Hands
        _body.position = _followTarget.position;
        _body.rotation = _followTarget.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //AnimateHand();

        PhysicsMove();
    }

    private void PhysicsMove()
    {
        // Position
        var positionWithOffset = _followTarget.TransformPoint(positionOffset);
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        // Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        _body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

    //internal void SetTrigger(float v)
    //{
    //    triggerTarget = v;
    //}

    //internal void SetGrip(float v)
    //{
    //    gripTarget = v;
    //}
    //void AnimateHand()
    //{
    //    if (gripCurrent != gripTarget)
    //    {
    //        gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
    //        animator.SetFloat(animatorGripParam, gripCurrent);
    //    } 
        
    //    if (triggerCurrent != triggerTarget)
    //    {
    //        triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
    //        animator.SetFloat(animatorTriggerParam, triggerCurrent);
    //    }
    //}
    //public void ToggleVisibility()
    //{
    //    mesh.enabled = !mesh.enabled;
    //}
}
