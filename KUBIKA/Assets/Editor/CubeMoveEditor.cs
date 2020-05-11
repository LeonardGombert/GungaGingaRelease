using Kubika.Game;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CubeMove))]
public class CubeMoveEditor : OdinEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CubeMove cubeScript = (CubeMove)target;

        _MoveableCube moveableCube = cubeScript.gameObject.GetComponent<_MoveableCube>();

        if(moveableCube != null)
        {

        }
    }
}
