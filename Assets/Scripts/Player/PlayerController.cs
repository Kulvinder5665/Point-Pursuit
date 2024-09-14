using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float dampingForce = 0.1f;
    [SerializeField] private float jumpPower = 15.0f; // Increased jump power for a more exciting jump
    [SerializeField] private float fallMultiplier = 3.0f; // Increased fall multiplier for a faster fall
    private float rotationSpeed = 100.0f;
    //private Vector3 movement;
    private Quaternion targetRotation;

    private bool isGrounded = true;
    [SerializeField] private LayerMask groundMask;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
     
        ApplyFallingGravity();
    }

    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        // Move player
        Vector3 moveDirection = ((Vector3.left * forwardInput * -1) + Vector3.forward * horizontalInput).normalized;
        playerRb.AddForce(moveDirection * speed, ForceMode.Acceleration); // Use Acceleration for more control

        // Rotate player
        if (horizontalInput != 0)
        {
            targetRotation *= Quaternion.Euler(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, dampingForce);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // Reset y velocity
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void ApplyFallingGravity()
    {
        if (!isGrounded && playerRb.velocity.y < 0)
        {
            playerRb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsGround(collision))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (IsGround(collision))
        {
            isGrounded = false;
        }
    }

    private bool IsGround(Collision collision)
    {
        // Check if the collision is with the ground layer
        return (groundMask & (1 << collision.gameObject.layer)) != 0;
    }
}
