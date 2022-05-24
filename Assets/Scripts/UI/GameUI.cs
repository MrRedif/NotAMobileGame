using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    //Fields
    [SerializeField] TMP_Text playerHealthText;
    [SerializeField] TMP_Text playerScoreText;
    [SerializeField] GameObject newHighscoreText;
    [SerializeField] GameObject gameOverScreenPrefab;

    private void Awake()
    {
        PlayerStatusManager.PlayerHealthChangeAction += ChangePlayerHealthText;
        DataOverseer.ScoreChangeAction += ChangePlayerScoreText;
        DataOverseer.NewHighScoreAction += EnableNewHighscore;
        DataOverseer.GameOverAction += GameOverScreen;
    }

    private void OnDestroy()
    {
        PlayerStatusManager.PlayerHealthChangeAction -= ChangePlayerHealthText;
        DataOverseer.ScoreChangeAction -= ChangePlayerScoreText;
        DataOverseer.NewHighScoreAction -= EnableNewHighscore;
        DataOverseer.GameOverAction -= GameOverScreen;
    }

    void ChangePlayerHealthText(int health)
    {
        playerHealthText.text = health.ToString();
    }
    void ChangePlayerScoreText(int score)
    {
        playerScoreText.text = score.ToString();
    }
    void EnableNewHighscore()
    {
        newHighscoreText.SetActive(true);
    }

    void GameOverScreen()
    {
        Instantiate(gameOverScreenPrefab).transform.SetParent(transform,false);
    }
}
