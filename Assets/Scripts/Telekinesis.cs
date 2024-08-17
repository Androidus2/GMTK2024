using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    [SerializeField] private float pickupRange = 5f;         // How far the player can pick up objects from
    [SerializeField] private float holdDistance = 3f;        // Distance from the player where the object will be held
    [SerializeField] private float moveSpeed = 10f;          // Speed at which the object moves when held
    [SerializeField] private Transform holdPosition;         // Position where the object will be held
    [SerializeField] private LayerMask pickupMask;           // Layer mask for objects that can be picked up
    [SerializeField] private Camera cam;       


    private GameObject heldObject;         // The object currently being held
    private Rigidbody heldObjectRb;        // Rigidbody of the held object
    private bool isHolding;

    private float originalScale;
    private float originalMass;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for pickup
        {
            if (heldObject == null)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }

        if (heldObject != null)
        {
            MoveObject();
        }
    }

    void TryPickup()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupMask))
        {
            PickupObject(hit.collider.gameObject);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObject = pickObj;
            isHolding = true;
            heldObjectRb = heldObject.GetComponent<Rigidbody>();
            originalMass = heldObjectRb.mass;
            originalScale = heldObject.transform.localScale.x;
            heldObjectRb.mass = 0.0001f;

            // Disable physics while holding
            heldObjectRb.useGravity = false;
            heldObjectRb.drag = 10;


            // make kinematic
            heldObjectRb.freezeRotation = true;
        }
    }

    void MoveObject()
    {
        // Determine where the object should be
        Vector3 targetPosition = holdPosition.position;

        // Move the object towards the target position
        Vector3 direction = targetPosition - heldObject.transform.position;
        heldObjectRb.velocity = direction * moveSpeed;
        
        
    }

    void DropObject()
    {
        // Re-enable physics
        heldObjectRb.useGravity = true;
        heldObjectRb.mass = originalMass *  (heldObject.transform.localScale.x / originalScale);
        originalMass = 0;
        originalScale = 0;

        heldObjectRb.drag = 1;

        heldObjectRb.velocity = Vector3.zero;
        heldObject = null;
        isHolding = false;

        heldObjectRb.freezeRotation = false;
    }

    public bool getIsHolding() 
    { 
        return isHolding;
    }

    public GameObject getHeldObject()
    {
        return heldObject;
    }

}
