using UnityEngine;

public class ClimbingRobotController : MonoBehaviour
{
    public bool isSelected = false;

    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;
    public GameObject sensor;  // The point from which the ray will be cast
    public bool isGrounded = false;

    void Update()
    {
        
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
        RaycastHit hit;
        if (Physics.Raycast(sensor.transform.position, sensor.transform.forward, out hit, 1.5f))
        {
            isGrounded = true;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
            transform.position = Vector3.Slerp(transform.position, hit.point, Time.deltaTime * 5);
        }
        else
        {
            isGrounded = false;
        }

        // If not grounded, add downward force
        if (!isGrounded)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        // Debug Ray
        Debug.DrawRay(sensor.transform.position, sensor.transform.forward * 1.5f, Color.green);
    }
}
