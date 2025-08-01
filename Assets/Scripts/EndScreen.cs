using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = $"Congratulations!\nYour score was {scoreKeeper.CalculateScore()}%";
    }
}
