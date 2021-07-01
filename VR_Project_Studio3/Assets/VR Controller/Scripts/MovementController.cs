//Creation info

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementController : MonoBehaviour
{
    public float moveSpeed = 1f;

    public List<XRController> controllers;

    public GameObject head = null;

    [SerializeField] TeleportationProvider teleportationProvider;
    public GameObject playerMain;
    public GameObject playerXRRig;

    private void OnEnable()
    {
        teleportationProvider.endLocomotion += OnEndLocomotion;
    }

    private void OnDisable()
    {
        teleportationProvider.endLocomotion -= OnEndLocomotion;
    }

    void OnEndLocomotion(LocomotionSystem locomotion)
    {
        Debug.Log("Teleport Mode Off");
        playerMain.transform.position = playerMain.transform.TransformPoint(playerXRRig.transform.localPosition);
        playerXRRig.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (XRController xRController in controllers)
        {
            Debug.Log(xRController.name);

            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 positionVector))
            {
                if (positionVector.magnitude > 0.15f)
                {
                    Debug.Log(positionVector);
                    PlayerMove(positionVector);
                }

            }
        }
    }

    private void PlayerMove(Vector2 positionVec)
    {
        Vector3 direction = new Vector3(positionVec.x, 0, positionVec.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);

        direction = Quaternion.Euler(headRotation) * direction;

        Vector3 movement = direction * moveSpeed;

        transform.position += (Vector3.ProjectOnPlane(Time.deltaTime * movement, Vector3.up));
    }
}
