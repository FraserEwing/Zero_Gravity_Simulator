using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZeroGravityRotation : MonoBehaviour
{
    public Transform userTransform; // Reference to the user (XR Origin)
    public float rotationSpeed = 10000f; // Adjusts rotation sensitivity
    public CharacterController characterController; // Reference to the character controller
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor activeInteractor; // Tracks the grabbing hand (left hand)
    private Vector3 lastHandPosition; // Stores last hand position
    private bool isRotating = false; // Whether rotation is active

    void Update()
    {
        if (isRotating && activeInteractor != null)
        {
            Vector3 handPosition = activeInteractor.transform.position;
            Vector3 handMovement = handPosition - lastHandPosition; // Movement delta

            // Determine axis of rotation (perpendicular to hand movement)
            Vector3 rotationAxis = Vector3.Cross(handMovement, userTransform.up).normalized;

            // Get the centre of the character controller in world space
            Vector3 characterCentre = characterController.transform.position + characterController.center;

            // Rotate the ISS model around the character controller centre
            transform.RotateAround(characterCentre, rotationAxis, handMovement.magnitude * rotationSpeed * Time.deltaTime);

            lastHandPosition = handPosition; // Update last position
        }
    }

    public void OnGrabStart(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        isRotating = true;
        activeInteractor = interactor;
        lastHandPosition = interactor.transform.position;
    }

    public void OnGrabEnd()
    {
        isRotating = false;
        activeInteractor = null;
    }
}
