using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Defect : MonoBehaviour
{
    // Contains defect variations
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

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
        Report.Instance.updateReport();

        Debug.Log("Defect Sent to Report!");
    }

    // Determine whether defect is currently visible to active robot's camera
    public virtual void whileInCameraView(Camera camera)
    {
        /*
        if (camera == null || GetComponent<Collider>() == null || GetComponent<MeshRenderer>() == null)
        {
            Debug.LogError("Missing component: Camera, Collider, or MeshRenderer is null.");
            return;
        }
        */

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        if (GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds))
        {
            print("The object" + gameObject.name + "has appeared");
            GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            print("The object" + gameObject.name + "has disappeared");
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
