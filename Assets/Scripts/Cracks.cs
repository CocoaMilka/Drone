using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cracks : Defect
{
    // Start is called before the first frame update
    void Start()
    {
        classification = "Cracks";
        measurement = Random.Range(2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
