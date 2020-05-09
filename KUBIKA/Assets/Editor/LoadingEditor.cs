using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kubika.Saving;

[CustomEditor(typeof(SaveAndLoad))]
public class LoadingEditor : Editor
{
    public static string levelName;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveAndLoad saveAndLoader = (SaveAndLoad)target;

        levelName = EditorGUILayout.TextField("Load/Save Level Name", levelName);

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Level"))
        {
            saveAndLoader.SaveLevel(levelName);
        }

        if (GUILayout.Button("Load Level"))
        {
            saveAndLoader.LoadLevel(levelName);
        }
    }
}
