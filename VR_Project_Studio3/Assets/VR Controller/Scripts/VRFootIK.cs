//One Day, this will be commented

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFootIK : MonoBehaviour
{
    private Animator animator;

    public LayerMask walkableFloor;

    public Vector3 footOffset;

    [Space(10)]
    [Range(0,1)]
    public float leftFootPosWeight = 1f;
    [Range(0,1)]
    public float leftFootRotWeight = 1f;
    [Range(0, 1)]
    public float rightFootPosWeight = 1f;
    [Range(0, 1)]
    public float rightFootRotWeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK(int layerIndex)
    {
        Vector3 leftFootPos = animator.GetIKPosition(AvatarIKGoal.LeftFoot);
        Vector3 rightFootPos = animator.GetIKPosition(AvatarIKGoal.RightFoot);

        RaycastHit hitLeft, hitRight;

        bool hitFloorLeft = Physics.Raycast(leftFootPos + Vector3.up, Vector3.down, out hitLeft, Mathf.Infinity, walkableFloor.value, QueryTriggerInteraction.Ignore);
        bool hitFloorRight = Physics.Raycast(rightFootPos + Vector3.up, Vector3.down, out hitRight, Mathf.Infinity, walkableFloor.value, QueryTriggerInteraction.Ignore);

        if (hitFloorLeft)
        {
            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitLeft.normal), hitLeft.normal);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, leftFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.LeftFoot, hitLeft.point + footOffset);

            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, leftFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.LeftFoot, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        }

        if (hitFloorRight)
        {
            Quaternion footRotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, hitRight.normal), hitRight.normal);

            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, rightFootPosWeight);
            animator.SetIKPosition(AvatarIKGoal.RightFoot, hitRight.point + footOffset);

            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, rightFootRotWeight);
            animator.SetIKRotation(AvatarIKGoal.RightFoot, footRotation);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        }
    }
}
