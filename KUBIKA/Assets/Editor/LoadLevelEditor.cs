using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoadLevelEditor : EditorWindow
{
    string[] levels;
    int index = 0;

    [MenuItem("Examples/Editor GUI Popup usage")]
    static void Init()
    {
        var window = GetWindow<EditorGUIPopup>();
        window.position = new Rect(0, 0, 180, 80);
        window.Show();
    }

    void OnGUI()
    {
        index = EditorGUI.Popup(new Rect(0, 0, position.width, 20), "Load Level : ", index, levels);
    }
}
