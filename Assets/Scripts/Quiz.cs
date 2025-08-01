using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField]
    QuestionSO currentQuestion;

    [SerializeField]
    List<QuestionSO> availableQuestions = new List<QuestionSO>();

    [SerializeField]
    TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField]
    GameObject[] answerButtons = new GameObject[4];
    bool hasAnsweredEarly;

    [SerializeField]
    Sprite defaultAnswerSprite;

    [SerializeField]
    Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField]
    Image timerImage;
    Timer timer;

    [Header("Scoring")]
    ScoreKeeper scoreKeeper;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [Header("Slider")]
    [SerializeField]
    Slider progressSlider;

    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressSlider.maxValue = availableQuestions.Count();
    }

    void Update()
    {
        if (timer.loadNextQuestion)
        {
            if (progressSlider.maxValue == progressSlider.value)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void GetRandomQuestion()
    {
        if (!availableQuestions.Any())
        {
            return;
        }
        var index = Random.Range(0, availableQuestions.Count());
        var possibleQuestion = availableQuestions.ElementAtOrDefault(index);

        if (possibleQuestion != null)
        {
            currentQuestion = possibleQuestion;
            availableQuestions.Remove(possibleQuestion);
        }
    }

    void GetNextQuestion()
    {
        GetRandomQuestion();
        SetButtonState(true);
        SetDefaultButtonSprites();
        DisplayQuestion();
        scoreKeeper.IncrementQuestionsSeen();
        progressSlider.value = progressSlider.maxValue - availableQuestions.Count();
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Count(); i++)
        {
            var button = answerButtons.ElementAt(i);
            var buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    public void DisplayAnswer(int index)
    {
        hasAnsweredEarly = true;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "You're correct";
            var buttonImage = answerButtons.ElementAt(index).GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = $"The correct answer was: {currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex())}";
            var buttonImage = answerButtons.ElementAt(currentQuestion.GetCorrectAnswerIndex()).GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}%";
    }

    void SetButtonState(bool state)
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Button>().interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        foreach (var button in answerButtons)
        {
            button.GetComponentInChildren<Image>().sprite = defaultAnswerSprite;
        }
    }
}