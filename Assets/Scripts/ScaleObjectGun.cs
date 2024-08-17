using UnityEngine;

public class ScaleObjectGun : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;  // The camera used for aiming
    [SerializeField] private float maxDistance = 10f;  // Maximum distance for interacting with objects
    [SerializeField] private float scaleAmount = 0.01f;  // Amount to scale the object per key press
    [SerializeField] private LayerMask interactableLayer; // Layer mask to define which objects can be scaled

    // Reference to the Telekinesis script
    private Telekinesis telekinesis;

    void Start()
    {
        // Assuming the Telekinesis script is on the same GameObject
        telekinesis = GetComponent<Telekinesis>();

        // If the Telekinesis script is on a different GameObject, you can find it like this:
        // telekinesis = GameObject.Find("TelekinesisObject").GetComponent<Telekinesis>();
    }

    void Update()
    {
        HandleScaling();
    }

    private void HandleScaling()
    {
        // If the player is holding an object
        if (telekinesis.GetIsHolding())
        {

            // Check if the held object has a ScaleObject component
            ScaleObject scaleObject = telekinesis.GetScaleObject();

            if (scaleObject != null)
            {
                // Increase/Decrease size
                if (Input.GetKey(KeyCode.Q))
                {
                    scaleObject.Scale(Vector3.one * scaleAmount);
                }
                else if (Input.GetKey(KeyCode.E) && scaleObject.transform.localScale.x > 0.1f)
                {
                    scaleObject.Scale(Vector3.one * -scaleAmount);
                }

                // Reset size with R
                if (Input.GetKeyDown(KeyCode.R))
                {
                    scaleObject.ResetScale();
                }
            }
        }
        else
        {
            // If the player is not holding an object, perform the normal raycast operation

            // Cast a ray from the center of the screen (camera) forward
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer))
            {
                // Check if the object hit by the ray has the ScaleObject script
                ScaleObject scaleObject = hit.transform.GetComponent<ScaleObject>();

                if (scaleObject != null)
                {
                    // Increase/Decrease size
                    if (Input.GetKey(KeyCode.Q))
                    {
                        scaleObject.Scale(Vector3.one * scaleAmount);
                    }
                    else if (Input.GetKey(KeyCode.E))
                    {
                        scaleObject.Scale(Vector3.one * -scaleAmount);
                    }

                    // Reset size with R
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        scaleObject.ResetScale();
                    }
                }
            }
        }
    }
}
