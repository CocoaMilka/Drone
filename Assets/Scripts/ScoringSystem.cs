using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    private int totalScore = 0;
    //private int capturedDefects = 0;
    [Header("Total Scannable Defects in the Scene")]
    [SerializeField] public int totalDefects = 0;
    private const int BaseScorePerDefect = 100;
    private const int MaxAdditionalScorePerDefect = 100;
    private const int MaxScorePerDefect = BaseScorePerDefect + MaxAdditionalScorePerDefect;

    // Contains all defects
    List<Defect> defects;

    private void Awake()
    {
        Debug.Log("Starting up Scoring system...");
        defects = new List<Defect>();
    }

    void OnEnable()
    {
        Defect.OnDefectScanned += HandleDefectScanned;
    }

    void OnDisable()
    {
        Defect.OnDefectScanned -= HandleDefectScanned;
    }

    private void HandleDefectScanned(Defect defect)
    {
        // Add base score and defect-specific score
        //totalScore += BaseScorePerDefect + defect.defectScore;
        //capturedDefects++;

        // Each defect contains information about its score
        defects.Add(defect);

        Debug.Log($"Updated Score: {totalScore}");
    }

    public void CalculateFinalGrade()
    {
        Debug.Log("Calculating Grade...");

        if (defects.Count > 0)
        {
            foreach (Defect defect in defects)
            {
                totalScore += defect.defectScore;
            }

            int maxPossibleScore = defects.Count * MaxScorePerDefect;
            float grade = (float)totalScore / maxPossibleScore;
            Debug.Log($"Final Grade: {grade * 100}%");
        }
        else
        {
            // If no defects are captured, F
            totalScore = 0;
            Debug.Log($"Final Grade: {totalScore * 100}%");
        }
    }
}
