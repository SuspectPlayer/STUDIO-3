using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCameraAssign : MonoBehaviour
{
    [Tooltip("the default camera assigned to this screen")]
    public Camera cam;

    [Tooltip("The Virtual Camera assigned to this screen")]
    public CinemachineVirtualCamera vCam;
}
