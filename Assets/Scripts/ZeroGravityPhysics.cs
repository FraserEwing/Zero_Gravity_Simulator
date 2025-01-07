using UnityEngine;


public class ZeroGravityPhysics : MonoBehaviour
{
    public Transform xrOrigin; // Reference to the XR Origin
    public float drag = 0.1f; // Reduces momentum over time
    private Vector3 currentVelocity; // Stores the current velocity of the XR Origin
    private Vector3 lastHandPosition; // Tracks the last position of the hand
    private bool isGrabbing = false; // Whether the grab movement is active
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor activeInteractor; // Reference to the active grabbing interactor

    void Update()
    {
        if (isGrabbing && activeInteractor != null)
        {
            // Calculate velocity based on the movement of the hand
            Vector3 handPosition = activeInteractor.transform.position;
            currentVelocity = (handPosition - lastHandPosition) / Time.deltaTime;
            lastHandPosition = handPosition;
        }
        else
        {
            // Apply momentum to the XR Origin
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, drag * Time.deltaTime);
            xrOrigin.position += currentVelocity * Time.deltaTime;
        }
    }

    public void OnGrabStart(UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
    {
        isGrabbing = true;
        activeInteractor = interactor;
        lastHandPosition = interactor.transform.position;
        currentVelocity = Vector3.zero; // Reset momentum
    }

    public void OnGrabEnd()
    {
        isGrabbing = false;
        activeInteractor = null; // Clear the interactor reference
    }
}
