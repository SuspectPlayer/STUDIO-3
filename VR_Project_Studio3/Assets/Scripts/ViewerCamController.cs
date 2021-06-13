using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerCamController : MonoBehaviour
{
    public float yAxisRotationMin = 0;
    public float yAxisRotationMax = 0;
    public float xAxisRotationMin = 0;
    public float xAxisRotationMax = 0;
    [Space]
    public float lookSpeed = 2f;
    [Space]
    public Vector2 cameraRotation = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        cameraRotation.x -= Input.GetAxis("Mouse Y") * lookSpeed;
        cameraRotation.y += Input.GetAxis("Mouse X") * lookSpeed;

        cameraRotation.y = Mathf.Clamp(cameraRotation.y, yAxisRotationMin, yAxisRotationMax);
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, xAxisRotationMin, xAxisRotationMax);

        gameObject.transform.localEulerAngles = cameraRotation;
    }
}
