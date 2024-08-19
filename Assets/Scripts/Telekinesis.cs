using System;
using UnityEngine;
using UnityEngine.Animations;

public class Telekinesis : MonoBehaviour
{
    [SerializeField]
    private float pickupRange = 5f;         // How far the player can pick up objects from
    [SerializeField]
    private float holdDistance = 70f;        // Distance from the player where the object will be held (max)
    [SerializeField]
    private float moveSpeed = 10f;          // Speed at which the object moves when held
    [SerializeField]
    private Transform holdPosition;         // Position where the object will be held
    [SerializeField]
    private Transform playerPosition;       // Position of the player
    [SerializeField]
    private LayerMask pickupMask;           // Layer mask for objects that can be picked up
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private LineRenderer moveBeam;
    [SerializeField]
    private Transform beamPosition;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float rotationSpeed = 100f; 


    private GameObject heldObject;         // The object currently being held
    private ScaleObject scaleObject;
    private Rigidbody heldObjectRb;        // Rigidbody of the held object
    private bool isHolding;

    private float originalScale;
    private float originalMass;
    private int rotationAxis;


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

            if(Input.GetMouseButtonDown(1)) 
            {
                rotationAxis++;
                if(rotationAxis > 2)
                {
                    rotationAxis = 0;
                }
            }

            if (Input.GetKey(KeyCode.F) && rotationAxis == 0) {
                RotateObject(Vector3.up);
            }
            else if (Input.GetKey(KeyCode.F) && rotationAxis == 1) {
                RotateObject(Vector3.right);
            }
            else if (Input.GetKey(KeyCode.F) && rotationAxis == 2) {
                RotateObject(Vector3.forward);
            }
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
            scaleObject = heldObject.GetComponent<ScaleObject>();
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

            anim.SetBool("Hold", true);


            // for animation
            beamPosition.position = pickObj.transform.position;
            moveBeam.SetPosition(1, beamPosition.localPosition);
            moveBeam.transform.parent.gameObject.SetActive(true);


        }
    }

    void MoveObject()
    {
        // Determine where the object should be
        Vector3 targetPosition = holdPosition.position;

        // Move the object towards the target position
        Vector3 direction = targetPosition - heldObject.transform.position;
        heldObjectRb.velocity = direction * moveSpeed;

        beamPosition.position = heldObject.transform.position;
        moveBeam.SetPosition(1, beamPosition.localPosition);


        // If we get past a certain range we drop the item after a certain delay
        if (Vector3.Distance(heldObject.transform.position, playerPosition.position) > holdDistance)
        {
            DropObject();
        }

        
    }

    void DropObject()
    {
        // Re-enable physics
        heldObjectRb.useGravity = true;
        heldObjectRb.mass = (float)(originalMass *  Math.Pow(heldObject.transform.localScale.x / originalScale, 3));
        originalMass = 0;
        originalScale = 0;

        heldObjectRb.drag = 1;

        heldObjectRb.velocity = Vector3.zero;
        heldObject = null;
        isHolding = false;

        heldObjectRb.freezeRotation = false;

        anim.SetBool("Hold", false);

        moveBeam.transform.parent.gameObject.SetActive(false);
    }

    void RotateObject(Vector3 axis)
    {
        if(heldObject != null) {
            heldObject.transform.Rotate(axis, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    public bool GetIsHolding() 
    { 
        return isHolding;
    }

    public GameObject GetHeldObject()
    {
        return heldObject;
    }

    public ScaleObject GetScaleObject()
    {
        return scaleObject;
    }


}
