using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    
    public int GetCorrectAnswers() {
        return correctAnswers;
    }

    public int GetQuestionsSeen() {
        return questionsSeen;
    }

    public void IncrementCorrectAnswers() {
        correctAnswers++;
    }

    public void IncrementQuestionsSeen() {
        questionsSeen++;
    }

    public int CalculateScore() {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
