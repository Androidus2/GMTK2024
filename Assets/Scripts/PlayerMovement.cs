using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float groundDrag = 0.8f;


    [SerializeField]
    private float playerHeight = 2f;
    [SerializeField]
    private LayerMask groundMask;
    private bool isGrounded;


    [SerializeField]
    private Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f, groundMask);

        HandleInput();

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Reset the player's velocity to zero
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.VelocityChange);
    }

}
