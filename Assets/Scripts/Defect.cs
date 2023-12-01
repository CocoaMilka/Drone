using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Defect : MonoBehaviour
{
    // Contains defect variations
    public List<Sprite> sprites = new List<Sprite>();

    public bool isChecked = false;

    // Defect measurements
    public string classification = "unclassified";
    public float measurement = 0.0f;
    public string timeCapture;
    public Sprite defectCapture;

    // Send defect instance to report list for display
    public virtual void sendDefectToReport()
    {
        // Capture time defect was scanned
        isChecked = true;
        timeCapture = GameManager.Instance.timer.ToString();
        Report.Instance.defects.Add(this);
    }
}
