using Kubika.CustomLevelEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kubika.Game
{
    [RequireComponent(typeof(Button))]
    public class CubeSelector : MonoBehaviour
    {
        //set by CubePopulator
        public CubeTypes selectedCubeType;

        // called on button press
        public void ChangeCubeSelection()
        {
            UIManager.instance.ButtonCallback("LEVELEDITOR_isPlacing");
            Debug.Log("You've pressed " + selectedCubeType.ToString());
            LevelEditor.instance.currentCube = selectedCubeType;
        }
    }
}