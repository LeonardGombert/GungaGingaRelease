using Kubika.LevelEditor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(_Grid))]
public class GridEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _Grid grid = (_Grid)target;

        if (GUILayout.Button("Clear Level"))
        {
            grid.ResetGrid();
        }
    }
}
