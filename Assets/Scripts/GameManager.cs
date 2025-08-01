using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;

    EndScreen endScreen;

    // Start is called before the first frame update
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }

    void Start()
    {
        quiz.GameObject().SetActive(true);
        endScreen.GameObject().SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete)
        {
            endScreen.GameObject().SetActive(true);
            quiz.GameObject().SetActive(false);
            endScreen.ShowFinalScore();
        }
    }

    public void onReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
