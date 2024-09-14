using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementX : MonoBehaviour
{
    Rigidbody playerRb;
    [SerializeField]private float speed;
    private Quaternion targetRotation;
    private float rotationSpeed = 100f;
    private float dampingForce = 100f;

    public float dampingCoefficient ;
    public float jumpForce;
    bool isGrounded = true;


    public float fallMultiplier;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
        Vector3 moveDirection = (Vector3.right * forwardInput + Vector3.forward * horizontalInput).normalized;
        playerRb.AddForce(moveDirection * speed, ForceMode.Acceleration);

        if(horizontalInput != 0)
        {
               targetRotation *= Quaternion.Euler(0, horizontalInput * rotationSpeed * Time.deltaTime, 0); 
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, dampingForce);

        if(horizontalInput == 0 && forwardInput == 0)
        {
            ApplyDamping();
        }
    }

    void ApplyDamping()
    {
        // Reduce the velocity by the damping coefficient
        Vector3 velocity = playerRb.velocity;
        velocity *= (1 - dampingCoefficient * Time.fixedDeltaTime);

        // Apply the updated velocity to the Rigidbody
        playerRb.velocity = velocity;
    }

    void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void ApplyFallingGravity()
    {
            if (!isGrounded)
            {
                Vector3 gravityX = Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;
                playerRb.velocity += gravityX;
            }
        

    }

    private void OnCollisionExit(Collision collision)
    {
      if( collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}