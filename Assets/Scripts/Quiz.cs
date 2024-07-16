using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //adds the textmeshpro namespace
using UnityEngine.UI; //need for Image type

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true; //checks whether the timer has run out and should be displaying the answer OR we've clicked the button to show the answer

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;

    [Header("Question Image")]
    [SerializeField] Image questionImage;

    [Header("Answer Image")]
    [SerializeField] Image answerImage;

    [Header("SFX Player")]
    [SerializeField] SFXPlayer sfxPlayer;

    public bool isComplete; //whether quiz is complete or not

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
        questionImage.gameObject.SetActive(false);
        answerImage.gameObject.SetActive(false);
    }

    bool hasBeenCalled = false;
    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion) {
            if (progressBar.value == progressBar.maxValue) {
            isComplete = true;
            return;
            }

            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
            hasBeenCalled = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            if (!hasBeenCalled) {
              DisplayAnswer(-1);
              SetButtonState(false);
              hasBeenCalled = true;
            }
        }
    }

    void DisplayAnswer(int index) {
        questionImage.gameObject.SetActive(false);

        if (currentQuestion.answerImage != null) {
          answerImage.gameObject.SetActive(true);
          answerImage.sprite = currentQuestion.answerImage;
        }

        Image buttonImage;
        if(index == currentQuestion.GetCorrectAnswerIndex()) {
            questionText.text = "Correct!";
            sfxPlayer.PlayCorrectAudio();
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            sfxPlayer.PlayIncorrectAudio();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was:\n" + correctAnswer;
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreText.text = "% Correct: " + scoreKeeper.CalculateScore() + "%";
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "% Correct: " + scoreKeeper.CalculateScore() + "%";
    }

    void GetNextQuestion() {
        if(questions.Count > 0) {
            SetButtonState(true);
            SetDefaultButtonSprites();
            answerImage.gameObject.SetActive(false);
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion() {
        int index = Random.Range(0, questions.Count); //returns a random element between 0 and the question count in the list
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion() {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }

        if (currentQuestion.questionImage != null) {
          questionImage.gameObject.SetActive(true);
          questionImage.sprite = currentQuestion.questionImage;
        }
    }

    void SetButtonState(bool state) {
        for (int i = 0; i < answerButtons.Length; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites() {
        for (int i = 0; i < answerButtons.Length; i++) {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
