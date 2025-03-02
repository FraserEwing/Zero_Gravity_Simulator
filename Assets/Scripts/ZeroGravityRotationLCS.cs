using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ZeroGravityRotationLCS : MonoBehaviour
{
    public Transform userTransform; // Reference to the user (XR Origin)
    public float rotationSpeed = 10000f; // Adjusts rotation sensitivity
    public float displayTime = 0.5f; // Time between each rotation update
    public CharacterController characterController; // Reference to the character controller
    private XRBaseInteractor activeInteractor; // Tracks the grabbing hand
    private Vector3 lastHandPosition; // Stores last hand position
    private bool isRotating = false; // Whether rotation is active
    private Coroutine rotationCoroutine;

    void Start()
    {
        rotationCoroutine = null;
    }

    public void OnGrabStart(XRBaseInteractor interactor)
    {
        isRotating = true;
        activeInteractor = interactor;
        lastHandPosition = interactor.transform.position;

        if (rotationCoroutine == null)
        {
            rotationCoroutine = StartCoroutine(RotateWithDelay());
        }
    }

    public void OnGrabEnd()
    {
        isRotating = false;
        activeInteractor = null;

        if (rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    private IEnumerator RotateWithDelay()
    {
        while (isRotating)
        {
            if (activeInteractor != null)
            {
                Vector3 handPosition = activeInteractor.transform.position;
                Vector3 handMovement = handPosition - lastHandPosition; // Movement delta

                if (handMovement.magnitude > 0.01f) // Ignore tiny movements
                {
                    // Determine axis of rotation (perpendicular to hand movement)
                    Vector3 rotationAxis = Vector3.Cross(handMovement, userTransform.up).normalized;

                    // Get the centre of the character controller in world space
                    Vector3 characterCentre = characterController.transform.position + characterController.center;

                    // Rotate the ISS model around the character controller centre
                     transform.RotateAround(characterCentre, rotationAxis, handMovement.magnitude * rotationSpeed * Time.deltaTime);
                }

                lastHandPosition = handPosition; // Update last position
            }

            yield return new WaitForSeconds(displayTime); // Delay before next rotation update
        }
    }
}


