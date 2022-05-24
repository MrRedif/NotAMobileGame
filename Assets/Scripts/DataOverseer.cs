using UnityEngine;
using System;

public class DataOverseer : MonoBehaviour
{
    //Fields
    [SerializeField] GameData data;
    [SerializeField] int currentScore;

    float startTime;
    bool isHighscoreAchived;

    //Actions
    public static event Action GameOverAction;
    public static event Action NewHighScoreAction;
    public static event Action<int> ScoreChangeAction;

    private void Awake()
    {
        PlayerStatusManager.PlayerDeathAction += FireGameOver;
        NoteBehaviour.KillNoteAction += AddScore;
        GameOverAction += UpdateData;

        startTime = Time.time;
    }

    private void OnDestroy()
    {
        PlayerStatusManager.PlayerDeathAction -= FireGameOver;
        NoteBehaviour.KillNoteAction -= AddScore;
        GameOverAction -= UpdateData;
    }

    /// <summary>
    /// Saves and updates game data.
    /// </summary>
    void UpdateData()
    {
        data.LastScore = currentScore;
        data.LastSurviveTime = Time.time - startTime;

        //Highscores
        if (isHighscoreAchived)
        {
            data.Highscore = currentScore;
        }

        if (Time.time - startTime > data.BestSurviveTime)
        {
            data.BestSurviveTime = Time.time - startTime;
        }
    }

    void FireGameOver()
    {
        GameOverAction?.Invoke();
    }

    void AddScore(int point)
    {
        currentScore += point;
        ScoreChangeAction?.Invoke(currentScore);
        if (currentScore > data.Highscore && !isHighscoreAchived)
        {
            isHighscoreAchived = true;
            NewHighScoreAction?.Invoke();
        }
    }
}
