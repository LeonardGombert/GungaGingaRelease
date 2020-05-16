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
            dynamicEnum = DynamicEnums.Base;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}