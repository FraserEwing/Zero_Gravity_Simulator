using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravityPropulsion : MonoBehaviour
{
    public CharacterController characterController; // Assign the Character Controller
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor leftHandInteractor;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor rightHandInteractor;

    public float propulsionForceMultiplier = 5f; // Adjust to control propulsion strength
    public float drag = 0.1f; // Simulates resistance to slow down over time

    private Vector3 currentVelocity;

    private Vector3 leftHandPreviousPosition;
    private Vector3 rightHandPreviousPosition;

    private void Start()
    {
        // Initialize previous positions
        leftHandPreviousPosition = leftHandInteractor.transform.position;
        rightHandPreviousPosition = rightHandInteractor.transform.position;
    }

    private void Update()
    {
        // Simulate drag to slow down the propulsion over time
        currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, drag * Time.deltaTime);

        // Apply the velocity to move the CharacterController
        characterController.Move(currentVelocity * Time.deltaTime);

        // Update previous positions for the next frame
        leftHandPreviousPosition = leftHandInteractor.transform.position;
        rightHandPreviousPosition = rightHandInteractor.transform.position;
    }

    public void OnGrabMoveEnded(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        // Determine which hand ended the grab move and calculate velocity
        Vector3 velocity = Vector3.zero;

        // Calculate velocity for the left hand
        if (interactor == leftHandInteractor)
        {
            velocity = (leftHandInteractor.transform.position - leftHandPreviousPosition) / Time.deltaTime;
        }
        // Calculate velocity for the right hand
        else if (interactor == rightHandInteractor)
        {
            velocity = (rightHandInteractor.transform.position - rightHandPreviousPosition) / Time.deltaTime;
        }
        else
        {
            return; // Unknown interactor
        }

        // Apply the calculated velocity in the inverse direction
        Vector3 inverseVelocityDirection = -velocity.normalized; // Get the inverse of the velocity direction
        currentVelocity += inverseVelocityDirection * velocity.magnitude * propulsionForceMultiplier;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Stop movement when a collision is detected
        currentVelocity = Vector3.zero;
    }
}

