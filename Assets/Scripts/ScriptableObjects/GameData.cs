using UnityEngine;

[CreateAssetMenu(menuName ="GameData",fileName ="newGameData")]
public class GameData : ScriptableObject
{
    //Fields
    [SerializeField] int lastScore;
    [SerializeField] int highScore;
    [SerializeField] float bestSurviveTime;
    [SerializeField] float lastSurviveTime;

    //Properties
    public int LastScore { get { return lastScore; } set { lastScore = value; } }
    public float LastSurviveTime { get { return lastSurviveTime; } set { lastSurviveTime = value; } }
    public int Highscore { get { return highScore; } set {if(value > highScore) highScore = value; } }
    public float BestSurviveTime { get { return bestSurviveTime; } set {if(value > bestSurviveTime) bestSurviveTime = value; } }

    public void ClearAllData()
    {
        highScore = 0;
        bestSurviveTime = 0f;

        lastScore = 0;
        lastSurviveTime = 0f;
    }

}
