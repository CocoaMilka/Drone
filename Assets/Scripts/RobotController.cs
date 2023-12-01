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

    public Texture2D defectCapture;
    public Sprite defectCaptureSprite;

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

    public void CaptureFrame()
    {
        // Ensure the renderTexture is already assigned and initialized
        if (renderTexture == null)
        {
            Debug.LogError("RenderTexture is not initialized.");
            return;
        }

        // Create a new Texture2D with the same dimensions as the RenderTexture
        defectCapture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        // Save the current active RenderTexture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the active RenderTexture to our RenderTexture, so we can read from it
        RenderTexture.active = renderTexture;

        // Read pixels from the RenderTexture and apply them to the Texture2D
        defectCapture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        defectCapture.Apply();

        // Restore the active RenderTexture
        RenderTexture.active = currentActiveRT;

        // Create a Sprite from the Texture2D
        defectCaptureSprite = Sprite.Create(defectCapture, new Rect(0.0f, 0.0f, defectCapture.width, defectCapture.height), new Vector2(0.5f, 0.5f));
    }

    // Call in Update
    public void DefectDetection()
    {
        if (robotCamera.enabled)
        {
            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 4.0f))
            {
                Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 4.0f, Color.yellow);

                // Check if the hit object has the "Defect" tag
                if (hit.collider.gameObject.tag == "Defect")
                {
                    var defectObject = hit.collider.gameObject.GetComponent<Defect>();

                    if (defectObject != null && !defectObject.isChecked)
                    {
                        CaptureFrame();
                        defectObject.defectCapture = defectCaptureSprite;
                        defectObject.sendDefectToReport();
                    }
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
