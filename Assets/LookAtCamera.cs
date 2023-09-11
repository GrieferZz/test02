using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform mainCameraTransform;

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        // Calculate the direction from the object to the main camera
        Vector3 directionToCamera = mainCameraTransform.position - transform.position;
        directionToCamera.y = 0f; // Keep the object facing horizontally

        // Rotate the object to face the camera
        if (directionToCamera != Vector3.zero)
        {
            transform.forward = directionToCamera.normalized;
        }
    }
}