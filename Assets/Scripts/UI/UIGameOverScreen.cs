using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOverScreen : MonoBehaviour
{
    //Game Data
    [SerializeField] GameData data;

    //Fields
    [SerializeField] TMP_Text currentScoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text currentSurviveTimeText;
    [SerializeField] TMP_Text bestSurviveTimeText;

    private void Start()
    {
        currentScoreText.text = data.LastScore.ToString();
        highScoreText.text = data.Highscore.ToString();
        currentSurviveTimeText.text = data.LastSurviveTime.ToString("F2");
        bestSurviveTimeText.text = data.BestSurviveTime.ToString("F2");
    }
}
