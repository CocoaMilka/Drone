using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    // Force will be applied from propellers
    public List<GameObject> props;

    public float internalHoverHeight = 3f;
    public float hoverPower = 3f;

    public float distance = 3f;
    public float force = 40f;
    public float turnSpeed = 100f;

    public float horizontalSensitivity = 0.1f;
    public float verticalSensitivity = 0.1f;
    public float upwardForceMultiplier = 10.0f;

    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Read joystick input
        float horizontal = Input.GetAxis("Horizontal") * horizontalSensitivity;
        float vertical = Input.GetAxis("Vertical") * verticalSensitivity;

        // For turning
        body.AddTorque(transform.TransformDirection(Vector3.up) * horizontal * turnSpeed);

        // Apply upwards force from each propeller
        foreach (GameObject prop in props)
        {
            RaycastHit hit;
            if (Physics.Raycast(prop.transform.position, transform.TransformDirection(Vector3.down), out hit, distance))
            {
                //body.AddForceAtPosition(transform.TransformDirection(Vector3.up) * Mathf.Pow(distance - hit.distance, 2) / distance * force, prop.transform.position);

                if (distance < internalHoverHeight)
                {
                    body.AddForce(transform.TransformDirection(Vector3.up) * hoverPower * (1 - (distance / internalHoverHeight)), ForceMode.Acceleration);
                }

                // Debug raycast
                Debug.DrawRay(prop.transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
            }
        }
    }
}
