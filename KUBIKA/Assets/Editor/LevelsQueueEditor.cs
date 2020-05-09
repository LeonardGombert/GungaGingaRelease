using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Kubika.Game;
using Sirenix.OdinInspector.Editor;

[CustomEditor(typeof(LevelsQueue))]
public class LevelsQueueEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelsQueue levelsQueue = (LevelsQueue)target;

        if (GUILayout.Button("Load Next Level"))
        {
            levelsQueue.LoadLevel();
        }
    }
}
