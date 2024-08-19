using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    [SerializeField]
    private LineRenderer leftHandEffect;

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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, (playerHeight / 2 + 0.1f) * transform.localScale.x, groundMask);

        HandleInput();
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ScalePlayer(new Vector3(0.1f, 0.1f, 0.1f));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ScalePlayer(new Vector3(-0.1f, -0.1f, -0.1f));
        }

        anim.SetFloat("Speed", rb.velocity.sqrMagnitude);
        //Debug.Log(rb.velocity.sqrMagnitude);
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space) && isGrounded)
            jumpInput = jumpForce * Mathf.Pow(transform.localScale.x, 0.3f);
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

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * Mathf.Pow(transform.localScale.x, 0.3f), ForceMode.VelocityChange);

        rb.velocity = new Vector3(rb.velocity.x, jumpInput, rb.velocity.z);
    }

    private void ScalePlayer(Vector3 scaleChange)
    {
        if((transform.localScale.x > 0.2f && scaleChange.x < 0f) || (transform.localScale.x < 3.99 && scaleChange.x > 0f)) {
            transform.localScale += scaleChange;
            rb.mass = transform.localScale.x;
            leftHandEffect.startWidth = transform.localScale.x;
            leftHandEffect.endWidth = transform.localScale.x;
        }
    
    }

}
