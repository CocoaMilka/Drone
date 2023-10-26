using UnityEngine;

public class ClimbingRobotController : MonoBehaviour
{
    public bool isSelected = false;

    public Camera secondCamera;

    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;

    // Moveable on robot, for detecting ground and walls
    public GameObject sensor;
    public GameObject sensorBottom;

    public bool isGrounded = false;

    void Update()
    {
        if (Input.GetButtonDown("Select"))
        {
            secondCamera.enabled = !secondCamera.enabled;
            Debug.Log("Camera Toggled");
        }
    }

    private void FixedUpdate()
    {
        if (isSelected)
        {
            HandleInput();
        }

        ApplyPhysics();
    }

    private void HandleInput()
    {
        float move = Input.GetAxis("Vertical_L") * moveSpeed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal_R") * rotationSpeed * Time.deltaTime;

        transform.Translate(Vector3.forward * move);
        transform.Rotate(0, turn, 0);
    }

    private void ApplyPhysics()
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
