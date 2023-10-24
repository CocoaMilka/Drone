using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    bool isOn = false;
    public bool isSelected = true;

    public Camera secondCamera;

    // Physics is applied from each propellor, using faux prop locations so model can balance, spinny props are just visual
    public List<GameObject> props;
    public List<GameObject> spinnyProps;

    public float maxPropellerTorque = 1000f; // Maximum torque to apply to propellers
    public float torqueIncreaseRate = 500f;  // Rate at which torque increases to start the propellers
    public float torqueDecreaseRate = 200f;  // Rate at which torque decreases to slow down the propellers

    // Speed of gravity is set to 10, this is to allow hover
    public float baseForce = 10f;
    public float forwardForceMultiplier = 5f;
    public float turnTorqueMultiplier = 1.5f;
    public float verticalForceMultiplier = 10f;

    public float drag = 1.5f;

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

        if (Input.GetButtonDown("Select"))
        {
            secondCamera.enabled = !secondCamera.enabled;
            Debug.Log("Camera Toggled");
        }
    }

    private void FixedUpdate()
    {
        if (isOn)
        {
            // If drone is selected, then user can control (handled in GameManager)
            if (isSelected) 
            {
                HandleInput();
            }
            SpinPropellers();
            ApplyPhysics();
            ApplyDrag();
        }
    }

    private void HandleInput()
    {
        // Read joystick input
        float horizontalInput = Input.GetAxis("Horizontal_R") * horizontalSensitivity;
        float verticalInput = Input.GetAxis("Vertical_L") * verticalSensitivity;
        float forwardInput = Input.GetAxis("Forward_R") * verticalSensitivity;
        float leftRightInput = Input.GetAxis("LeftRight_R") * horizontalSensitivity;

        // Update force and torque based on input
        baseForce = 10 + verticalInput * verticalForceMultiplier;
        Debug.Log(baseForce);

        body.AddForce(transform.forward * forwardInput * forwardForceMultiplier);
        body.AddForce(transform.right * leftRightInput * forwardForceMultiplier);
        body.AddTorque(transform.up * horizontalInput * turnTorqueMultiplier);
    }

    private void ApplyPhysics()
    {
        // Apply upwards force from each propeller
        foreach (GameObject prop in props)
        {
            body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * baseForce / props.Count, prop.transform.position);
        }
    }
    private void ApplyDrag()
    {
        // Apply drag to slow down the drone when no input is present
        Vector3 dragForce = -body.velocity * drag;
        body.AddForce(dragForce);
        body.AddTorque(-body.angularVelocity * drag);
    }

    void startDrone()
    {
        isOn = true;
    }

    void stopDrone()
    {
        isOn = false;
    }

    // Purely visual, doesn't work properly tho smh my head
    void SpinPropellers()
    {
        foreach (GameObject prop in spinnyProps)
        {
            Rigidbody propRigidbody = prop.GetComponent<Rigidbody>();
            if (propRigidbody != null)
            {
                // Gradually increase torque to start the propellers
                float currentTorque = Mathf.Min(maxPropellerTorque, propRigidbody.angularDrag + torqueIncreaseRate * Time.deltaTime);
                propRigidbody.AddTorque(prop.transform.up * currentTorque, ForceMode.Acceleration);

                // Gradually decrease torque to slow down the propellers
                if (!isOn)
                {
                    currentTorque = Mathf.Max(0, propRigidbody.angularDrag - torqueDecreaseRate * Time.deltaTime);
                    propRigidbody.angularDrag = currentTorque;
                }
            }
        }
    }
}
