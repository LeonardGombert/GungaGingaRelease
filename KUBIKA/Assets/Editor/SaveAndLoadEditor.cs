using UnityEngine;
using UnityEditor;
using Kubika.Saving;
using Sirenix.OdinInspector.Editor;

[CustomEditor(typeof(SaveAndLoad))]
public class SaveAndLoadEditor : OdinEditor
{
    public static string levelName;
    public static int miminumMoves;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SaveAndLoad saveAndLoader = (SaveAndLoad)target;

        levelName = EditorGUILayout.TextField("Load/Save Level Name", levelName);
        miminumMoves = EditorGUILayout.IntField("Minimum Moves to Beat", miminumMoves);

        EditorGUILayout.Space();

        if (GUILayout.Button("Save Level"))
        {
            saveAndLoader.SaveLevelFull(levelName, false, miminumMoves);
        }

        if (GUILayout.Button("Load Level"))
        {
            saveAndLoader.LoadLevel(levelName);
        }
    }
}
