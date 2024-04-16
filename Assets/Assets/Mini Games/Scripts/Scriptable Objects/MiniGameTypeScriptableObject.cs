using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "MiniGameType", menuName = "Mini Game Data/New Mini Game Type", order = 1)]
public class MiniGameTypeScriptableObject : ScriptableObject
{
    [Header("Info")]
    public string minigameScene;
    public string minigameName;
    
    [Header("Stats")]
    public float gameDuration;
}

#if UNITY_EDITOR

[CustomEditor(typeof(MiniGameTypeScriptableObject))]
public class MiniGameTypeScriptableObjectEditor : Editor
{
    private string[] tabs = { "Info", "Stats" };
    private int currentTab = 0;
    
    public override void OnInspectorGUI()
    {
        MiniGameTypeScriptableObject miniGameTypeScriptableObject = (MiniGameTypeScriptableObject)target;

        // Tab selection buttons
        GUILayout.BeginHorizontal();
        for (int i = 0; i < tabs.Length; i++)
        { if (GUILayout.Button(tabs[i], GUILayout.Height(30)))
            {
                currentTab = i;
            }
        }
        GUILayout.EndHorizontal();

        // Content based on the selected tab
        switch (currentTab)
        {
            case 0: // Info tab
                GUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Mini Game Settings", EditorStyles.boldLabel);
                EditorGUILayout.Space();

                miniGameTypeScriptableObject.minigameName = EditorGUILayout.TextField("Mini Game Name", miniGameTypeScriptableObject.minigameName);
                miniGameTypeScriptableObject.minigameScene = EditorGUILayout.TextField("Mini Game Scene", miniGameTypeScriptableObject.minigameScene);

                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
                break;
            case 1: // Stats tab
                GUILayout.BeginVertical("box");
                EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);
                
                miniGameTypeScriptableObject.gameDuration = EditorGUILayout.FloatField("Game Duration (Seconds)", miniGameTypeScriptableObject.gameDuration);
                
                EditorGUILayout.EndVertical();
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

#endif