using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrainGaugeInteract : MonoBehaviour
{
    public GameObject strainDisplay;
    public bool isInteractable;

    private Transform collidedObjectTransform;
    private void Start()
    {
        strainDisplay.SetActive(false);
    }

    private void Update()
    {
        strainDisplay.transform.rotation = Camera.main.transform.rotation;

        // Display slightly below robot
        strainDisplay.transform.position = collidedObjectTransform.position - new Vector3(0, 2, 0);


        if (isInteractable && Input.GetButtonDown("Interact"))
        {
            Debug.Log("interacting...");
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
}
