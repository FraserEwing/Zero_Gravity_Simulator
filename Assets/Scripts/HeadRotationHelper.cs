using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotationHelper : MonoBehaviour
{
    public Transform xrOriginTransform;  // XR Origin Transform
    public Transform vrCameraTransform;  // VR Camera (headset) Transform

    public float boundingBoxAngle = 45f; // Angle from centre to the edge of the bounding box
    public float rotationStep = 30f;     // Degrees to rotate when head exits the box
    public float holdTime = 1f;          // Time to hold head outside box before rotating again
    public float rotationSpeed = 100f;   // Smooth rotation speed (degrees per second)

    private Vector3 centreDirection;     // Direction when the head is "centred"
    private float timeOutsideBox = 0f;   // Time the user's head has stayed outside the bounding box
    private float targetRotationY = 0f;  // Target Y rotation for smooth rotation

    void Start()
    {
        if (!xrOriginTransform || !vrCameraTransform)
        {
            Debug.LogError("Please assign XR Origin and VR Camera in the Inspector!");
            return;
        }

        // Initialize centre direction relative to xrOriginTransform's forward
        centreDirection = new Vector3(xrOriginTransform.forward.x, 0, xrOriginTransform.forward.z).normalized;
        targetRotationY = xrOriginTransform.eulerAngles.y;
    }

    void Update()
    {
        HandleBoundingBoxRotation();
        SmoothRotateOrigin();
    }

    private void HandleBoundingBoxRotation()
    {
        // Get the camera's forward direction relative to the XR Origin
        Vector3 localForward = xrOriginTransform.InverseTransformDirection(vrCameraTransform.forward);
        Vector3 currentDirection = new Vector3(localForward.x, 0, localForward.z).normalized;

        // Compute the angle between the centre direction and the current gaze direction
        float angleFromCentre = Vector3.SignedAngle(centreDirection, currentDirection, Vector3.up);

        // Check if the angle is outside the bounding box
        if (Mathf.Abs(angleFromCentre) > boundingBoxAngle)
        {
            timeOutsideBox += Time.deltaTime;

            // If the user has stayed outside the box for the required hold time, queue a rotation
            if (timeOutsideBox >= holdTime)
            {
                float rotationDirection = angleFromCentre > 0 ? 1f : -1f;
                targetRotationY += rotationStep * rotationDirection;

                // Reset timer
                timeOutsideBox = 0f;
            }
        }
        else
        {
            // Reset timer when the gaze is inside the bounding box
            timeOutsideBox = 0f;
        }
    }

    private void SmoothRotateOrigin()
    {
        // Smoothly rotate towards the target rotation
        float currentY = xrOriginTransform.eulerAngles.y;
        float newY = Mathf.MoveTowardsAngle(currentY, targetRotationY, rotationSpeed * Time.deltaTime);
        xrOriginTransform.eulerAngles = new Vector3(0, newY, 0);
    }
}
