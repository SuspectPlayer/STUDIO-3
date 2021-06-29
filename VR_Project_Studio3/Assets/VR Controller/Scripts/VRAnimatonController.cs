using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAnimatonController : MonoBehaviour
{
    public float speedThreshold = 0.1f;
    [Range(0,1)]public float smoothing = 1f;

    [SerializeField]private bool isMoving;

    [SerializeField] private Animator vRAnimator;
    [SerializeField] private Vector3 previousPos;
    [SerializeField] private VRRig vRRig;
    [SerializeField] Vector3 headSpeed;
    [SerializeField] float magni;

    [SerializeField] float currDirX;
    [SerializeField] float currDirY;

    [SerializeField] float prevDirX;
    [SerializeField] float prevDirY;

    // Start is called before the first frame update
    void Start()
    {
        vRAnimator = GetComponent<Animator>();
        vRRig = GetComponent<VRRig>();
        previousPos = vRRig.head.vrTarget.position;
    }

    // Update is called once per frame
    void Update()
    {

        headSpeed = (vRRig.head.vrTarget.position - previousPos) / Time.deltaTime;
        headSpeed.y = 0;
        
        //Convert Global Speed from Local Speed
        Vector3 headLocalSpeed = transform.InverseTransformDirection(headSpeed);
        previousPos = vRRig.head.vrTarget.position;

        //Set Anim Values
        isMoving = headLocalSpeed.magnitude > speedThreshold;
        magni = headLocalSpeed.magnitude;

        prevDirX = vRAnimator.GetFloat("Dir X");
        prevDirY = vRAnimator.GetFloat("Dir Y");

        currDirX = Mathf.Clamp(headLocalSpeed.x, -1, 1);
        currDirY = Mathf.Clamp(headLocalSpeed.z, -1, 1);

        vRAnimator.SetBool("IsMoving", isMoving);

        vRAnimator.SetFloat("Dir X", Mathf.Lerp(prevDirX, currDirX, smoothing));
        vRAnimator.SetFloat("Dir Y", Mathf.Lerp(prevDirY, currDirY, smoothing));
    }
}
