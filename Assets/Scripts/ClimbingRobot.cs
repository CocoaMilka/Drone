using UnityEngine;

public class ClimbingRobot : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 1000;

    public GameObject sensor;

    void Update()
    {
        // Movement
        float move = Input.GetAxis("Vertical_L") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        // Rotation
        float turn = Input.GetAxis("Horizontal_R") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, turn, 0);
        Debug.Log("Turn Input: " + Input.GetAxis("Horizontal_R"));

        // Surface Snapping
        RaycastHit hit;
        float smoothTime = 0.1f; // Adjust this value for smoother/faster transition
        if (Physics.Raycast(sensor.transform.position, sensor.transform.forward, out hit, 1.5f))
        {
            Debug.Log("Hit: " + hit.collider.name);
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime / smoothTime);
        }

        Debug.DrawRay(sensor.transform.position, sensor.transform.forward * 1.5f, Color.green);




    }
}
