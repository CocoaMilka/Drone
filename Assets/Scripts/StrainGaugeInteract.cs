using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrainGaugeInteract : MonoBehaviour
{
    public GameObject strainDisplay;

    private void Start()
    {
        strainDisplay.SetActive(false);
    }

    private void Update()
    {
        strainDisplay.transform.rotation = Camera.main.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        strainDisplay.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        strainDisplay.SetActive(false);
    }
}
