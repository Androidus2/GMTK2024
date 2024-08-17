using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpForce = 5f;

    [SerializeField]
    private float playerHeight = 2f;
    [SerializeField]
    private LayerMask groundMask;
    private bool isGrounded;

    [SerializeField]
    private Animator anim;


    [SerializeField]
    private Transform orientation;

    private float horizontalInput;
    private float verticalInput;
    private float jumpInput;

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

        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
        //Debug.Log(rb.velocity.sqrMagnitude);
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            jumpInput = jumpForce;
        else
            jumpInput = rb.velocity.y;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        //Reset the player's velocity to zero
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.VelocityChange);

        rb.velocity = new Vector3(rb.velocity.x, jumpInput, rb.velocity.z);
    }

}
