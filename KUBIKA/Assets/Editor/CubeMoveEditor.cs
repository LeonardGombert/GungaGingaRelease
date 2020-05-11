using Kubika.Game;
using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CubeMove))]
public class CubeMoveEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CubeMove cubeScript = (CubeMove)target;

        _VictoryCube timerCube = cubeScript.gameObject.GetComponent<_VictoryCube>();

        if(timerCube != null)
        {
            HideAllValues();
        }
    }

    private void HideAllValues()
    {
        throw new NotImplementedException();
    }
}
