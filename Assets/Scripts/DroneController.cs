using System.Collections.Generic;
using UnityEngine;

public class DroneController : RobotController
{
    // Physics is applied from each propellor, using faux prop locations so model can balance, spinny props are just visual
    public List<GameObject> props;
    public List<GameObject> spinnyProps;

    public ParticleSystem dustParticles;

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
        InitializeCamera();
        body = GetComponent<Rigidbody>();

        // Initialize Robot State
        isOn = false;
        isSelected = false;
    }

    void Update()
    {
        if (isSelected)
        {
            // Toggle drone
            if (Input.GetButtonDown("Start"))
            {
                toggleRobotPowerState();
                Debug.Log(isOn);
            }

            // Toggle Camera (enable/enable defect detection too)
            if (Input.GetButtonDown("Select"))
            {
                robotCamera.enabled = !robotCamera.enabled;
            }

            // Take pictures of Defects
            if (Mathf.Round(Input.GetAxisRaw("Capture")) > 0)   // only way to get triggers to work?? Negative is LT, Positive is RT
            {
                Debug.Log("Say Cheese! Taking Picture!");
                DefectDetection();
            }
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

    public override void HandleInput()
    {
        // Read joystick input
        float horizontalInput = Input.GetAxis("Horizontal_R") * horizontalSensitivity;
        float verticalInput = Input.GetAxis("Vertical_L") * verticalSensitivity;
        float forwardInput = Input.GetAxis("Forward_R") * verticalSensitivity;
        float leftRightInput = Input.GetAxis("LeftRight_R") * horizontalSensitivity;

        // Update force and torque based on input
        baseForce = 10 + verticalInput * verticalForceMultiplier;
        //Debug.Log(baseForce);

        body.AddForce(transform.forward * forwardInput * forwardForceMultiplier);
        body.AddForce(transform.right * leftRightInput * forwardForceMultiplier);
        body.AddTorque(transform.up * horizontalInput * turnTorqueMultiplier);
    }

    public override void ApplyPhysics()
    {
        // Apply upwards force from each propeller
        foreach (GameObject prop in props)
        {
            body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * baseForce / props.Count, prop.transform.position);
        }

        // Bottom sensor for spawning dust particles
        RaycastHit bottom;
        if (Physics.Raycast(transform.position, -transform.up, out bottom, 1.5f))
        {
            //Debug.DrawRay(transform.position, -transform.up * 1.5f, Color.green);
            //Debug.Log("Near Ground");

            // Move the dustParticles to the collision point
            dustParticles.transform.position = bottom.point;

            // Start the particle system if it's not already playing
            if (!dustParticles.isPlaying)
            {
                dustParticles.Play();
            }
        }
        else
        {
            // Stop the particle system if it's playing
            if (dustParticles.isPlaying)
            {
                dustParticles.Stop();
            }
        }
    }
    private void ApplyDrag()
    {
        // Apply drag to slow down the drone when no input is present
        Vector3 dragForce = -body.velocity * drag;
        body.AddForce(dragForce);
        body.AddTorque(-body.angularVelocity * drag);
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
                float currentTorque = Mathf.Min(maxPropellerTorque, propRigidbody.angularDrag + torqueIncreaseRate * Time.fixedDeltaTime);
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
