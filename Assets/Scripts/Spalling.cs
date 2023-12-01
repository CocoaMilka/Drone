using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spalling : Defect
{
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        classification = "Spalling";
        measurement = Random.Range(1, 3);
    }
}
