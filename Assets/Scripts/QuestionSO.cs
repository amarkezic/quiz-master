using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName = "New question")]
public class QuestionSO : ScriptableObject
{
    [SerializeField]
    [TextArea(2, 6)]
    string question = "Enter new question text here";


}
