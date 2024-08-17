using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;  // The player or object the camera will follow

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
