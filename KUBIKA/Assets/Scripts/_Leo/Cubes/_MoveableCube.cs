using Kubika.CustomLevelEditor;
using System.Collections;
using UnityEngine;

namespace Kubika.Game
{
    public class _MoveableCube : CubeMove
    {
        public override void Start()
        {
            myCubeLayer = CubeLayers.cubeMoveable;
            myCubeType = CubeTypes.MoveableCube;

            base.Start();

            isStatic = false;
            //call base.start AFTER assigning the cube's layers
        }

        public override void Update()
        {
            base.Update();
        }
    }
}