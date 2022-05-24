using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a singleton be careful !
/// </summary>
public class LineManager : MonoBehaviour
{
    //Fields
    List<Transform> lines = new List<Transform>();

    //Properties
    public List<Transform> Lines => lines;

    //Singleton instance
    public static LineManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            lines.Add(transform.GetChild(i).transform);
        }
    }

    /// <summary>
    /// Returns left line of given current line;
    /// </summary>
    public Transform GetLeftLine(Transform currentLine)
    {
        int nextIndex = lines.IndexOf(currentLine) - 1;
        if (nextIndex >= 0) return lines[nextIndex];
        else return null;
    }

    /// <summary>
    /// Returns right line of given current line;
    /// </summary>
    public Transform GetRightLine(Transform currentLine)
    {
        int nextIndex = lines.IndexOf(currentLine) + 1;
        if (nextIndex < lines.Count) return lines[nextIndex];
        else return null;
    }
}
