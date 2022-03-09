using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ScoreUpdated : UnityEvent<int>
{
}

[Serializable]
public class LivesUpdated : UnityEvent<int>
{
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int lives = 3;
    int score;

    float speed = 5.0f;
    float gameSpeedIncrement = 1.0f;

    [HideInInspector] public ScoreUpdated scoreUpdated;
    [HideInInspector] public LivesUpdated livesUpdated;

    [HideInInspector] public UnityEvent gameIsEnded;
    [HideInInspector] public UnityEvent gameRestarted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        scoreUpdated = new ScoreUpdated();
        livesUpdated = new LivesUpdated();

        gameIsEnded = new UnityEvent();
        gameRestarted = new UnityEvent();

        Time.timeScale = 0.0f;
    }

    private void Start()
    {
        SectionsFactory.Instance.BuildSectionsOnStart();
    }

    public void StartGame()
    {
        Time.timeScale = 1.0f;
    }

    public void RestartGame()
    {
        score = 0;
        lives = 3;
        gameSpeedIncrement = 1.0f;

        scoreUpdated.Invoke(score);
        livesUpdated.Invoke(lives);


        gameRestarted.Invoke();
        Time.timeScale = 1.0f;
        SectionsFactory.Instance.BuildSectionsOnStart();
    }

    public void UpdateScore()
    {
        score++;
        scoreUpdated.Invoke(score);
        CheckIncrementIncrease();
    }

    public void CheckGameOver()
    {
        lives--;
        livesUpdated.Invoke(lives);
        if (lives == 0)
        {
            Time.timeScale = 0.0f;
            gameIsEnded.Invoke();
        }
    }

    public float GetSpeed()
    {
        return speed * gameSpeedIncrement;
    }

    public int GetScore()
    {
        return score;
    }

    private void CheckIncrementIncrease()
    {
        float newIncrement = 1.0f;
        if (score == 10)
        {
            newIncrement = 1.5f;
            StartCoroutine(IncreaseSpeed(1.5f));
        }
        else if (score == 25)
        {
            newIncrement = 2.0f;
            StartCoroutine(IncreaseSpeed(2.0f));
        }
        else if (score == 50)
        {
            newIncrement = 3.0f;
            StartCoroutine(IncreaseSpeed(3.0f));
        }
        else if (score == 100)
        {
            newIncrement = 4.0f;
            StartCoroutine(IncreaseSpeed(4.0f));
        }
    }

    IEnumerator IncreaseSpeed(float newIncrement)
    {
        float initialIncrement = gameSpeedIncrement;

        float timer = 0.0f;

        while (timer < 2.0f)
        {
            timer += Time.deltaTime;
            gameSpeedIncrement = Mathf.Lerp(initialIncrement, newIncrement, timer / 2.0f);
            yield return null;
        }
        gameSpeedIncrement = newIncrement;
    }
}
