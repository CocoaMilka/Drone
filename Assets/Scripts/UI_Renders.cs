using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Renders : MonoBehaviour
{
    public RawImage render;
    public Camera renderCam;
    private RenderTexture renderTexture;

    private bool isSelected = false;

    public List<GameObject> renders = new List<GameObject>();  // Initialize this list in Unity Editor

    void Start()
    {
        renderTexture = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
        renderTexture.Create();

        renderCam.targetTexture = renderTexture;
        render.texture = renderTexture;
    }

    private void FixedUpdate()
    {
        if (isSelected)
        {
            spinRenders(renders);
        }
    }

    private void spinRenders(List<GameObject> spinObjects)
    {
        foreach (GameObject render in spinObjects)
        {
            render.transform.Rotate(new Vector3(0, 1, 0), 1.0f);
        }
    }

    public void onMouseHover()
    {
        isSelected = true;
    }

    public void onMouseExitHover()
    {
        isSelected = false;
    }
}
