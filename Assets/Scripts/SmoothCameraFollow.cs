using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;  // The player or object the camera will follow
    [SerializeField]
    private float smoothSpeed = 0.125f;  // Speed at which the camera follows
    [SerializeField]
    private Vector3 offset;  // Offset from the player

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        /// transform.LookAt(target);  // Make the camera look at the player
    }
}
