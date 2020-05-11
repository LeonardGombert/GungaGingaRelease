using Kubika.CustomLevelEditor;
using System.Collections;
using UnityEngine;

namespace Kubika.Game
{
    public class _MoveableCube : CubeMove
    {
        public override void Start()
        {
            base.Start();
            myCubeLayer = CubeLayers.cubeMoveable;
            myCubeType = CubeTypes.MoveableCube;

            isStatic = false;
            //call base.start AFTER assigning the cube's layers
        }

        public override void Update()
        {
            base.Update();
        }
    }
}