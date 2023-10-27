using UnityEngine;

public class ClimbingRobotController : RobotController
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;

    // Moveable on robot, for detecting ground and walls
    public GameObject sensor;
    public GameObject sensorBottom;

    public bool isGrounded = false;

    void Start()
    {
        InitializeCamera();

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
        }
    }

    private void FixedUpdate()
    {
        // Robot must be ON and SELECTED to control
        if (isOn && isSelected)
        {
            HandleInput();
        }

        ApplyPhysics();

        DefectDetection();
    }

    public override void HandleInput()
    {
        float move = Input.GetAxis("Vertical_L") * moveSpeed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal_R") * rotationSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * move);
        transform.Rotate(0, turn, 0);
    }

    public override void ApplyPhysics()
    {
        // Wall sticking and Surface Snapping

        // Forward sensor
        RaycastHit hit;
        if (Physics.Raycast(sensor.transform.position, sensor.transform.forward, out hit, 0.3f))
        {
            // Smooth rotation when climbing walls
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2);
            transform.position = Vector3.Slerp(transform.position, hit.point, Time.deltaTime * 2);
        }

        // Bottom sensor
        RaycastHit bottom;
        if (Physics.Raycast(sensorBottom.transform.position, sensorBottom.transform.forward, out bottom, 0.4f))
        {
            isGrounded = true;
            Vector3 oppositeNormal = -bottom.normal;
            float forceMagnitude = 3;  // Should be equal to or greater than mass
            gameObject.GetComponent<Rigidbody>().AddForce(oppositeNormal * forceMagnitude, ForceMode.Impulse);
        }
        else
        {
            isGrounded = false;
        }

        // If not grounded, apply gravity
        if (!isGrounded)
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().useGravity = false;
        }

        // Debug Rays
        Debug.DrawRay(sensor.transform.position, sensor.transform.forward * .3f, Color.green);
        Debug.DrawRay(sensorBottom.transform.position, sensorBottom.transform.forward * 0.2f, Color.green);
    }
}
