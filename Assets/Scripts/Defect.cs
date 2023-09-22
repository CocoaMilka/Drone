using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defect : MonoBehaviour
{
    public bool isChecked = false;
    GameObject check;
    void Start()
    {
        check = transform.GetChild(0).gameObject;
    }
    
    public void Check()
    {
        isChecked = true;
        check.SetActive(isChecked);
    }
}
