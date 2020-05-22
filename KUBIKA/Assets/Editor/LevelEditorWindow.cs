using Kubika.CustomLevelEditor;
using Kubika.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CanEditMultipleObjects]
public class LevelEditorWindow : EditorWindow
{
    UnityEngine.Object[] levelFiles;
    string[] levels;
    int index = 0;
    private string levelName;
    private int miminumMoves;

    [MenuItem("Tools/Level Editor")]
    static void Init()
    {
        var window = GetWindow<LevelEditorWindow>();
        window.position = new Rect(0, 0, 180, 80);
        window.Show();
    }

    void OnGUI()
    {
        LoadLevel();
        PlaceCubes();
        DeleteCubes();
        RotateCubes();
        GUILayout.Space(20);
        SaveLevel();
        SaveCurrentLevel();
    }

    private void PlaceCubes()
    {
        if (GUILayout.Button("Place Cubes"))
            LevelEditor.instance.isPlacing = !LevelEditor.instance.isPlacing;
    }
    private void DeleteCubes()
    {
        if (GUILayout.Button("Delete Cubes"))
            LevelEditor.instance.isDeleting = !LevelEditor.instance.isDeleting;
    }
    private void RotateCubes()
    {
        if (GUILayout.Button("Rotate Cubes"))
            LevelEditor.instance.isRotating = !LevelEditor.instance.isRotating;
    }

    private void LoadLevel()
    {
        levelFiles = Resources.LoadAll("MainLevels", typeof(TextAsset));

        levels = new string[levelFiles.Length];

        for (int i = 0; i < levelFiles.Length; i++)
        {
            levels[i] = levelFiles[i].name;
        }

        GUILayout.Space(26);

        index = EditorGUI.Popup(new Rect(0, 0, position.width, 20), "Load Level : ", index, levels);

        if (GUILayout.Button("Load Level !"))
            SaveAndLoad.instance.DevLoadLevel(levels[index]);
    }
    private void SaveLevel()
    {
        levelName = EditorGUILayout.TextField("Load/Save Level Name", levelName);
        miminumMoves = EditorGUILayout.IntField("Minimum Moves to Beat", miminumMoves);

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Level")) SaveAndLoad.instance.DevSavingLevel(levelName, miminumMoves);
    }

    private void SaveCurrentLevel()
    {
        if (GUILayout.Button("Save Current Level")) SaveAndLoad.instance.DevSavingCurrentLevel();
    }
}
