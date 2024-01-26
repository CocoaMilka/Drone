using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    private int totalScore = 0;
    private int totalDefects = 0;
    private const int BaseScorePerDefect = 100;
    private const int MaxAdditionalScorePerDefect = 100;
    private const int MaxScorePerDefect = BaseScorePerDefect + MaxAdditionalScorePerDefect;

    void OnEnable()
    {
        // Subscribe to the OnDefectScanned event
        Defect.OnDefectScanned += HandleDefectScanned;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        Defect.OnDefectScanned -= HandleDefectScanned;
    }

    private void HandleDefectScanned(Defect defect)
    {
        // Add base score and defect-specific score
        totalScore += BaseScorePerDefect + defect.defectScore;
        totalDefects++;

        // You can add additional logic here to handle different scoring criteria based on the defect's properties

        // Optionally, log the updated score
        Debug.Log($"Updated Score: {totalScore}");
    }

    public void CalculateFinalGrade()
    {
        int maxPossibleScore = totalDefects * MaxScorePerDefect;
        float grade = (float)totalScore / maxPossibleScore;
        Debug.Log($"Final Grade: {grade * 100}%");
    }
}
