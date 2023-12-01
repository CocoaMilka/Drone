using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public abstract class RobotController : MonoBehaviour
{
    public bool isSelected;
    public bool isOn;

    // Robot Vision
    public Camera robotCamera;
    public RawImage display;
    public RenderTexture renderTexture;
    private Transform cameraTransform;

    // Defect Detection
    RaycastHit defect;

    // Call in Start
    public void InitializeCamera()
    {
        cameraTransform = robotCamera.transform;
        renderTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
        robotCamera.targetTexture = renderTexture;
        display.texture = renderTexture;
    }

    // Call in Update
    public void DefectDetection()
    {
        // Someone clean this up
        if (robotCamera.enabled)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out defect, 4.0f))
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);

            if (defect.collider != null)
            {
                Defect hitDefect = defect.collider.gameObject.GetComponent<Defect>();
                if (hitDefect != null)
                {
                    // hitDefect.Check(); UPDATE THIS
                }
            }
        }
    }

    public void toggleRobotSelection()
    {
        isSelected = !isSelected;
    }

    public void toggleRobotPowerState()
    {
        isOn = !isOn;
    }

    // Custom implementations in each derived class
    public abstract void HandleInput();
    public abstract void ApplyPhysics();
}
