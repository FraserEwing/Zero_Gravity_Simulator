using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotationHelper : MonoBehaviour
{
    // Reference to the XR camera (VR headset)
    public Transform vrCameraTransform;

    // Thresholds for rotation (in degrees)
    public float upThreshold = 90f; 
    public float downThreshold = 270f;  
    public float rightThreshold = 90f; 
    public float leftThreshold = 270f; 

    // Adjustment angle (in degrees)
    public float adjustmentAngle = 45f;

    private Vector3 lastEulerAngles;

    // Update is called once per frame
    void Update()
    {
        if (lastEulerAngles == Vector3.zero){
            lastEulerAngles = vrCameraTransform.eulerAngles;
        }
        AdjustHeadsetRotation();
    }

    private void AdjustHeadsetRotation()
    {
        // Get the current rotation of the VR camera (headset)
        Vector3 currentEulerAngles = vrCameraTransform.eulerAngles;

        // Ensure we're dealing with the shortest angle (account for angle wrapping)
        currentEulerAngles.x = NormalizeAngle(currentEulerAngles.x);
        currentEulerAngles.y = NormalizeAngle(currentEulerAngles.y);
        
        // Handle vertical rotation (up/down)
        if (currentEulerAngles.y > upThreshold && currentEulerAngles.y < downThreshold && currentEulerAngles.y - lastEulerAngles.y > 0)
        {
            // If looking up, adjust upward
            vrCameraTransform.Rotate(Vector3.up * adjustmentAngle);
            upThreshold += adjustmentAngle; // Increase the vertical threshold when looking up
            downThreshold -= adjustmentAngle; // Decrease the vertical threshold when looking down
        }

        if (currentEulerAngles.y < upThreshold && currentEulerAngles.y > downThreshold && currentEulerAngles.y - lastEulerAngles.y < 0)
        {
            // If looking down, adjust downward
            vrCameraTransform.Rotate(Vector3.down * adjustmentAngle);
            upThreshold -= adjustmentAngle; // Decrease the vertical threshold when looking up
            downThreshold += adjustmentAngle; // Increase the vertical threshold when looking down
        }

        // Handle horizontal rotation (left/right)
        if (currentEulerAngles.x > rightThreshold && currentEulerAngles.x < leftThreshold && currentEulerAngles.x - lastEulerAngles.x > 0)
        {
            // If looking right, adjust rightward
            vrCameraTransform.Rotate(Vector3.right * adjustmentAngle);
            rightThreshold += adjustmentAngle; // Increase the right threshold when looking right
            leftThreshold -= adjustmentAngle; // Decrease the left threshold when looking right
        }

        if (currentEulerAngles.x < rightThreshold && currentEulerAngles.x > leftThreshold && currentEulerAngles.x - lastEulerAngles.x < 0)
        {
            // If looking left, adjust leftward
            vrCameraTransform.Rotate(Vector3.left * adjustmentAngle);
            rightThreshold -= adjustmentAngle; // Decrease the right threshold when looking left
            leftThreshold += adjustmentAngle; // Increase the left threshold when looking left
        }

        // Update the lastEulerAngles for the next frame
        lastEulerAngles = currentEulerAngles;
    }

    // Normalize the angle to ensure it's between 0 and 360 degrees
    private float NormalizeAngle(float angle)
    {
        if (angle < 0) 
        {
            angle += 360f;
        }
        return angle % 360f;
    }
}

