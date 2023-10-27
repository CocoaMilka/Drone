using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class UI_RobotRender : MonoBehaviour
{
    public RawImage render;
    public Camera renderCam;
    private RenderTexture renderTexture;

    public GameObject robot;

    private bool isSelected = false;

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
            spinRender();
        }
    }

    private void spinRender()
    {
        robot.transform.Rotate(new Vector3(0, 1, 0), 1.0f);
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
