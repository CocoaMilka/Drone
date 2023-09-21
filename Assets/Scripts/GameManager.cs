using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float Gravity = 10f;
    void Start()
    {
        Physics.gravity = new Vector3(0, -10f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
