using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    bool isOn = false;

    // Force will be applied from propellers
    public List<GameObject> props;

    public float distance = 5f;
    public float force = 0.1f;
    public float turnSpeed = 10f;

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
            force = 10;
            float forceHorizontal = 0;

            // Read joystick input
            float horizontal = Input.GetAxis("Horizontal_R") * horizontalSensitivity;
            float vertical = Input.GetAxis("Vertical_L") * verticalSensitivity;
            float forward = Input.GetAxis("Forward_R") * verticalSensitivity;

            force += vertical;
            Debug.Log(force);

            forceHorizontal += forward;
            body.AddForce(transform.TransformDirection(Vector3.forward) * forceHorizontal);

            // For turning
            body.AddTorque(transform.TransformDirection(Vector3.up) * horizontal * turnSpeed);

            // Apply upwards force from each propeller
            foreach (GameObject prop in props)
            {
                body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * force / 4, prop.transform.position);

                /*
                RaycastHit hit;
                if (Physics.Raycast(prop.transform.position, transform.TransformDirection(Vector3.down), out hit, distance))
                {
                    //body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * Mathf.Pow(distance - hit.distance, 2) / distance * force, prop.transform.position);
                    body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * force / 4, prop.transform.position);

                    // Debug raycast
                    Debug.DrawRay(prop.transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
                }
                */
            }
        }
    }

    // Turn on drone rotors and activates physics
    void startDrone()
    {
        isOn = true;

        // Spin rotors
    }

    void stopDrone()
    {
        isOn = false;

        // Turn off rotors
    }
}
