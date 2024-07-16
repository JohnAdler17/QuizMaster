using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz Question")] //creates a menu option for initializing scriptable object in assets folder/scene
public class QuestionSO : ScriptableObject {
    [TextArea(2,6)] //allows you to adjust and control the size of the textbox in the inspector (min#lines, max#lines)
    [SerializeField] string question = "Enter new question text here."; //using SerializeField here lets you change the question from the inspector but not from another class (like public would)

    [SerializeField] string[] answers = new string[4]; //initializes an empty array with the size 4 (for the 4 question answers)

    [SerializeField] int answerIndex;

    [SerializeField] public Sprite questionImage;

    [SerializeField] public Sprite answerImage;
    //this GetQuestion() is a getter method
    public string GetQuestion() {
        return question;
    }

    public int GetCorrectAnswerIndex() {
        return answerIndex;
    }

    public string GetAnswer(int index) {
        return answers[index];
    }
}
