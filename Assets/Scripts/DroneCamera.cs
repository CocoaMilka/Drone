using UnityEngine;
using UnityEngine.UI;

public class DroneCamera : MonoBehaviour
{
    public Camera droneCamera;
    public RawImage display;
    public RenderTexture renderTexture;

    Transform cameraTransform;
    RaycastHit hit;

    void Start()
    {
        cameraTransform = droneCamera.transform;

        renderTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
        droneCamera.targetTexture = renderTexture;
        display.texture = renderTexture;
    }

    void Update()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 4.0f))
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);

        if (hit.collider != null)
        {
            Defect hitDefect = hit.collider.gameObject.GetComponent<Defect>();
            if (hitDefect != null)
            {
                hitDefect.Check();
            }
        }
    }
}
