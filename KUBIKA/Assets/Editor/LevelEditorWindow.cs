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
    int levelIndex = 0;
    int cubeTypeIndex = 0;
    private string levelName;
    private int miminumMoves;

    string[] cubeTypes;
    CubeTypes cubeTypes2;

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

        SelectCubeType();
    }

    private void PlaceCubes()
    {
        if (GUILayout.Button("Place Cubes"))
            LevelEditor.instance.SwitchAction("isPlacing");
    }

    private void DeleteCubes()
    {
        if (GUILayout.Button("Delete Cubes"))
            LevelEditor.instance.SwitchAction("isDeleting");
    }

    private void RotateCubes()
    {
        if (GUILayout.Button("Rotate Cubes"))
            LevelEditor.instance.SwitchAction("isRotating");
    }

    private void LoadLevel()
    {
        levelFiles = Resources.LoadAll("MainLevels", typeof(TextAsset));

        levels = new string[levelFiles.Length];

        for (int i = 0; i < levelFiles.Length; i++)
        {
            levels[i] = levelFiles[i].name;
        }

        levelIndex = EditorGUI.Popup(new Rect(0, 20, position.width, 20), "Load Level : ", levelIndex, levels);

        GUILayout.Space(70);

        if (GUILayout.Button("Load Level !"))
            SaveAndLoad.instance.DevLoadLevel(levels[levelIndex]);
    }

    private void SelectCubeType()
    {
        cubeTypes = new string[(int)CubeTypes.RotatorCube];
        for (int i = 1; i < (int)CubeTypes.RotatorCube; i++)
        {
            cubeTypes2 = (CubeTypes)i;
            cubeTypes[i] = cubeTypes2.ToString();
        }

        cubeTypeIndex = EditorGUI.Popup(new Rect(0, 40, position.width, 20), "Cube Type : ", cubeTypeIndex, cubeTypes);

        LevelEditor.instance.currentCube = (CubeTypes)cubeTypeIndex;
    }

    private void SaveLevel()
    {
        levelName = EditorGUILayout.TextField("Save Level Name", levelName);
        miminumMoves = EditorGUILayout.IntField("Minimum Moves", miminumMoves);

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Level")) SaveAndLoad.instance.DevSavingLevel(levelName, miminumMoves);
    }

    private void SaveCurrentLevel()
    {
        if (GUILayout.Button("Save Current Level")) SaveAndLoad.instance.DevSavingCurrentLevel();
    }
}
