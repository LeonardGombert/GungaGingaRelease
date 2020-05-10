using Kubika.CustomLevelEditor;
using System.Collections;
using UnityEngine;

namespace Kubika.Game
{
    public class _MoveableCube : CubeMove
    {
        new void Start()
        {
            myCubeLayer = CubeLayers.cubeMoveable;
            myCubeType = CubeTypes.MoveableCube;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;
        }

        new void Update()
        {
            base.Update();
        }
    }
}