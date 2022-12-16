using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    // Set the speed at which the helicopter will move
    public float movementSpeed = 15.0f;
    
    // Set the amount of force applied to the helicopter to make it hover
    public float hoverForce = 50000.0f;

    // Set the amount of force applied to the helicopter when it moves up or down
    public float liftForce = 10.0f;

    // Set the amount of force applied to the helicopter when it moves left or right
    public float lateralForce = 0.01f;

    // Set the helicopter's rotor blade GameObject
    public GameObject rotorBlade;

    // Set the helicopter's rigidbody component
    private Rigidbody rb;

    void Start()
    {
        // Get the helicopter's rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        // Get the helicopter's current tilt and roll
        float tilt = transform.localRotation.eulerAngles.x;
        float roll = transform.localRotation.eulerAngles.z;

        // Handle input to move the helicopter up and down
        if (Input.GetKey(KeyCode.W))
        {
            // Apply a upward force to the helicopter
            rb.AddForce(Vector3.up * liftForce * 5);
            rb.constraints = RigidbodyConstraints.None;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            // Apply a downward force to the helicopter
             rb.AddForce(Vector3.down * liftForce);
             rb.constraints = RigidbodyConstraints.None;

        }
        else if (Input.GetKey(KeyCode.Space))
        {
            // Apply a forward force to the helicopter
            rb.AddForce(-transform.forward * movementSpeed * 10);
            rb.constraints = RigidbodyConstraints.None;

        }

        // Handle input to move the helicopter left and right
        if (Input.GetKey(KeyCode.D))
        {
            // Apply a force to the left of the helicopter
            rb.AddForce(Vector3.left * lateralForce);
            rb.constraints = RigidbodyConstraints.None;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            // Apply a force to the right of the helicopter
            rb.AddForce(Vector3.right * lateralForce);
            rb.constraints = RigidbodyConstraints.None;
        }

        // Handle input to rotate the helicopter left and right
        if (Input.GetKey(KeyCode.Q))
        {
            // Rotate the helicopter left
            transform.Rotate(Vector3.down * movementSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            // Rotate the helicopter right
            transform.Rotate(Vector3.up * movementSpeed * Time.deltaTime);
        }

        // Check if the helicopter is tilted or rolled too far
        if (tilt > 180.0f) tilt -= 360.0f;
        if (roll > 180.0f) roll -= 360.0f;
        if (tilt > 10.0f || tilt < -10.0f || roll > 10.0f || roll < -10.0f)
        {
            // Apply a counteracting force to level out the helicopter
            rb.AddTorque(-transform.right * tilt * 0.1f);
            rb.AddTorque(-transform.forward * roll * 0.1f);
        }
    }

    void FixedUpdate()
    {
        // Spin the helicopter's rotor blade
        rotorBlade.transform.Rotate(Vector3.forward * movementSpeed * Time.deltaTime * 300);
        
        // Apply a constant upward force to the helicopter to make it hover
        rb.AddForce(Vector3.up * hoverForce);
    }
}