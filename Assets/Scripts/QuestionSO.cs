using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName = "New question")]
public class QuestionSO : ScriptableObject
{
    [SerializeField]
    [TextArea(2, 6)]
    string question = "Enter new question text here";

    [SerializeField]
    string[] answers = new string[4];

    [SerializeField]
    int correctAnswerIndex = 0;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
}
