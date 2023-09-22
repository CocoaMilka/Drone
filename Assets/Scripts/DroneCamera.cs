using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class DroneCamera : MonoBehaviour
{
    public Camera droneCamera;
    Transform cameraTransform;

    RaycastHit hit;
    void Start()
    {
        cameraTransform = droneCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 4.0f))
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);

        Defect hitDefect = hit.collider.gameObject.GetComponent<Defect>();
        hitDefect.Check();
    }
}
