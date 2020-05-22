using Kubika.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CanEditMultipleObjects]
public class LoadLevelEditor : EditorWindow
{
    UnityEngine.Object[] levelFiles;
    string[] levels;
    int index = 0;

    [MenuItem("Tools/Load Levels")]
    static void Init()
    {
        var window = GetWindow<LoadLevelEditor>();
        window.position = new Rect(0, 0, 180, 80);
        window.Show();
    }

    void OnGUI()
    {
        levelFiles = Resources.LoadAll("MainLevels", typeof(TextAsset));

        levels = new string[levelFiles.Length];

        for (int i = 0; i < levelFiles.Length; i++)
        {
            levels[i] = levelFiles[i].name;
        }

        index = EditorGUI.Popup(new Rect(0, 0, position.width, 20), "Load Level : ", index, levels);

        if (GUI.Button(new Rect(0, 25, position.width, position.height - 26), "Load Level !"))
            SaveAndLoad.instance.DevLoadLevel(levels[index]);
    }
}
