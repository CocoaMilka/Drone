using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;

    private LineRenderer lineRenderer;

    void Start()
    {
        // Add a LineRenderer component to this GameObject if it doesn't already have one
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Optional: Customize the appearance of the line here
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use a simple shader
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    void Update()
    {
        if (obj1 != null && obj2 != null)
        {
            // Set the positions for the line renderer
            lineRenderer.SetPosition(0, obj1.transform.position);
            lineRenderer.SetPosition(1, obj2.transform.position);
        }
    }
}
