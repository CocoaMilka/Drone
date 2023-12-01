using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrainGaugeInteract : MonoBehaviour
{
    public GameObject strainDisplay;
    public bool isInteractable;

    private Transform collidedObjectTransform;

    private LineRenderer lineRenderer;
    private void Start()
    {
        strainDisplay.SetActive(false);

        // Add a LineRenderer component to this GameObject if it doesn't already have one
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        // Optional: Customize the appearance of the line here
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Use a simple shader
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    private void Update()
    {
        if (isInteractable && Input.GetButtonDown("Interact"))
        {
            Debug.Log("interacting...");
            lineRenderer.endColor = Color.yellow;
        }

        if (isInteractable)
        {
            strainDisplay.transform.rotation = Camera.main.transform.rotation;
            strainDisplay.transform.position = collidedObjectTransform.position - new Vector3(0, 2, 0);

            lineRenderer.SetPosition(0, gameObject.transform.position);
            lineRenderer.SetPosition(1, collidedObjectTransform.transform.position);
            CreateLightningEffect();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        isInteractable = true;
        collidedObjectTransform = other.transform; // Store the transform of the collided object
        strainDisplay.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        isInteractable = false;
        strainDisplay.SetActive(false);
    }

    private void CreateLightningEffect()
    {
        // Set the number of points in the line
        int numberOfPoints = 10;
        lineRenderer.positionCount = numberOfPoints;

        Vector3 startPosition = gameObject.transform.position;
        Vector3 endPosition = collidedObjectTransform.position;
        Vector3 direction = endPosition - startPosition;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float ratio = (float)i / (numberOfPoints - 1);
            Vector3 position = Vector3.Lerp(startPosition, endPosition, ratio);

            if (i != 0 && i != numberOfPoints - 1)
            {
                float randomOffset = 0.1f;
                position += new Vector3(Random.Range(-randomOffset, randomOffset), Random.Range(-randomOffset, randomOffset), Random.Range(-randomOffset, randomOffset));
            }

            lineRenderer.SetPosition(i, position);
        }
    }
}
