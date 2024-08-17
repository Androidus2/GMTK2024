using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpForce = 5f;

    private bool isGrounded = true;

    private void Start()
    {
        rb.interpolation = RigidbodyInterpolation.Interpolate;  // Enable interpolation to smooth movement
    }

    private void Update()
    {
        // Handle jump in Update, but apply force in FixedUpdate for better timing
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.T))
        {
            ApplyExplosionForce();
        }

    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = moveHorizontal * transform.right + moveVertical * transform.forward;

        // Move player using MovePosition for physics-based movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void ApplyExplosionForce()
    {
        rb.AddExplosionForce(50f, transform.position - transform.forward * 1f, 5f, 3f, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
