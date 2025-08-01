using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float timeToCompleteQuestion = 30f;

    [SerializeField]
    float timeToShowCorrectAnswer = 10f;


    public bool isAnsweringQuestion = false;
    public bool loadNextQuestion = false;

    float timerValue;

    int timerSeconds;

    // Update is called once per frame
    void Start()
    {
        isAnsweringQuestion = false;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    { 
        timerValue = 0f;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if (timerSeconds != (int)timerValue)
        {
            GetComponent<Image>().fillAmount = timerValue / (isAnsweringQuestion ? timeToCompleteQuestion : timeToShowCorrectAnswer);
        }
        timerSeconds = (int)timerValue;

        if (timerValue <= 0)
        {
            isAnsweringQuestion = !isAnsweringQuestion;
            timerValue = isAnsweringQuestion ? timeToCompleteQuestion : timeToShowCorrectAnswer;

            loadNextQuestion = isAnsweringQuestion;
        }
    }
}
