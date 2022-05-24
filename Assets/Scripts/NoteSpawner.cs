using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    //Fields
    [Header("Prefabs")]
    [SerializeField] GameObject notePrefab;
    [SerializeField] GameObject wallPrefab;

    [Header("Spawn Values")]
    [SerializeField] float spawnTime;
    [SerializeField] float noteSpeed;

    float spawnTimer;
    int spawnedWaves;

    //Line Values
    List<int> usedLines = new List<int>();
    enum LaneStatus { empty, spawned, blocked , resting}
    List<LaneStatus> lineStatuses = new List<LaneStatus>();

    //Dificulty
    [Header("Difficulty")]
    [SerializeField] int mediumDifficultyLevel;
    [SerializeField] int hardDifficultyLevel;
    enum Difficulty { easy, medium, hard}
    [SerializeField] Difficulty difficulty = Difficulty.easy;

    private void Awake()
    {
        spawnTimer = spawnTime;
    }

    private void Start()
    {
        for (int i = 0; i < LineManager.instance.Lines.Count; i++)
        {
            lineStatuses.Add(new LaneStatus());
        }
    }

    private void Update()
    {
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            SpawnLane();
            spawnTimer = spawnTime;
        }
    }

    /// <summary>
    /// Generates notes according to difficulty.
    /// </summary>
    private void SpawnLane()
    {
        DifficultyCheck();

        usedLines.Clear();
        int rand = Random.Range(0, 100);
        switch (difficulty)
        {
            case Difficulty.easy:
                if (rand >= 70) // %70
                {
                    GenerateWave(noteCount: Random.Range(1, 3), wallCount: 0, colorCount: 2);
                }
                else // %30
                {
                    GenerateWave(noteCount: 1, wallCount: Random.Range(0, 2), colorCount: 2);
                }
                break;
            case Difficulty.medium:
                if (rand >= 50) // %50
                {
                    GenerateWave(noteCount: 1, wallCount: 2, colorCount: 3);
                }
                else // %50
                {
                    GenerateWave(noteCount: 2, wallCount: 1, colorCount: 3);
                }
                break;
            case Difficulty.hard:
                if (rand >= 70) // %70
                {
                    GenerateWave(noteCount: 3, wallCount: 0, colorCount: 4);
                }
                else // %30
                {
                    GenerateWave(noteCount: 2, wallCount: 2, colorCount: 4);
                }
                break;
            default:
                break;
        }

    }

    private void DifficultyCheck()
    {
        if (spawnedWaves > hardDifficultyLevel)
        {
            difficulty = Difficulty.hard;
        }
        else if (spawnedWaves > mediumDifficultyLevel)
        {
            difficulty = Difficulty.medium;
        }
    }

    private void GenerateWave(int noteCount,int wallCount,int colorCount)
    {
        for (int i = 0; i < lineStatuses.Count; i++)
        {
            if (lineStatuses[i] == LaneStatus.blocked && difficulty != Difficulty.hard)
            {
                usedLines.Add(i);
                lineStatuses[i] = LaneStatus.resting;
            }
            else if (lineStatuses[i] == LaneStatus.resting)
            {
                lineStatuses[i] = LaneStatus.empty;
            }
        }

        SpawnObstacle(notePrefab, noteCount,LaneStatus.spawned,colorCount);
        SpawnObstacle(wallPrefab, wallCount, LaneStatus.blocked,colorCount);

        for (int i = 0; i < lineStatuses.Count; i++)
        {
            if (!usedLines.Contains(i))
            {
                lineStatuses[i] = LaneStatus.empty;
            }
        }
        spawnedWaves++;
    }

    private void SpawnObstacle(GameObject obj,int count,LaneStatus status,int allowedColorCount)
    {
        for (int i = 0; i < count; i++)
        {
            int rnd = GenerateRandomLine();
            if (rnd != -1)
            {
                Vector3 pos = new Vector3(LineManager.instance.Lines[rnd].position.x, transform.position.y + obj.transform.localScale.y / 2f, transform.position.z);
                Instantiate(obj, pos, Quaternion.identity).GetComponent<NoteBehaviour>().InitNote(noteSpeed, Random.Range(0, allowedColorCount));
                usedLines.Add(rnd);
                lineStatuses[rnd] = status;
            }
        }
    }

    /// <summary>
    /// Finds an avaible line
    /// </summary>
    /// <returns>
    /// Returns an avaible line index otherwise will return -1
    /// </returns>
    private int GenerateRandomLine()
    {
        int lineCount = LineManager.instance.Lines.Count;

        if (usedLines.Count == lineCount) return -1; //All lines are being used !

        int rnd = Random.Range(0, lineCount);
        if (!usedLines.Contains(rnd)) return rnd;
        else
        {
            rnd = (rnd + Random.Range(1, lineCount)) % lineCount;
            int infCheck = 0;
            while (usedLines.Contains(rnd) && infCheck < lineCount)
            {
                rnd = (rnd + 1) % lineCount;
                infCheck++;
            }
            if (infCheck >= lineCount)
            {
                Debug.LogWarning("Couldn't find an unused line! This should have not happened !");
            }
            return rnd;
        }
    }

}
