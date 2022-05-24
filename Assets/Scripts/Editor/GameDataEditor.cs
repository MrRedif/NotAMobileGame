using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameData))]
public class GameDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameData responseEvent = (GameData)target;

        if (GUILayout.Button("ClearAllData"))
        {
            responseEvent.ClearAllData();
        }
    }
}
