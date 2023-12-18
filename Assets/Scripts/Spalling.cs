using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spalling : Defect
{
    // Start is called before the first frame update
    void Start()
    {
        classification = "Spalling";
        measurement = Random.Range(1f, 3f);
    }

    private void Update()
    {
        // Check whether current robot can see defect
        whileInCameraView(GameManager.Instance.activeRobot.robotCamera);
    }
}
