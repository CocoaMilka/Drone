using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    bool isOn = false;
    public List<GameObject> props;

    // Speed of gravity is set to 10, this is to allow hover
    public float baseForce = 10f;
    public float forwardForceMultiplier = 5f;
    public float turnTorqueMultiplier = 5f;

    // Controller Input
    public float horizontalSensitivity = 0.1f;
    public float verticalSensitivity = 0.1f;

    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Toggle drone
        if (Input.GetButtonDown("Start"))
        {
            if (isOn)
            {
                stopDrone();
                Debug.Log("Shutting down drone...");
            }
            else
            {
                startDrone();
                Debug.Log("Starting up drone...");
            }
        }
    }

    private void FixedUpdate()
    {
        if (isOn)
        {
            HandleInput();
            ApplyPhysics();
        }
    }

    private void HandleInput()
    {
        // Read joystick input
        float horizontalInput = Input.GetAxis("Horizontal_R") * horizontalSensitivity;
        float verticalInput = Input.GetAxis("Vertical_L") * verticalSensitivity;
        float forwardInput = Input.GetAxis("Forward_R") * verticalSensitivity;

        // Update force and torque based on input
        baseForce = 10 + verticalInput;
        Debug.Log(baseForce);

        body.AddForce(transform.forward * forwardInput * forwardForceMultiplier);
        body.AddTorque(transform.up * horizontalInput * turnTorqueMultiplier);
    }

    private void ApplyPhysics()
    {
        // Apply upwards force from each propeller
        foreach (GameObject prop in props)
        {
            body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * baseForce / 4, prop.transform.position);
        }
    }

    void startDrone()
    {
        isOn = true;
        // Add logic to spin rotors
    }

    void stopDrone()
    {
        isOn = false;
        // Add logic to turn off rotors
    }
}
