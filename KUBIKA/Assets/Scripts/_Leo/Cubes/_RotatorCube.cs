using System;
using System.Collections.Generic;

namespace Kubika.Game
{
    public class _RotatorCube : CubeMove
    {
        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.RotatorCube;
            myCubeLayer = CubeLayers.cubeMoveable;
            dynamicEnum = DynamicEnums.Rotators;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;
        }
        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }

        //rotate in a certain direction
        private void RotateInDirection(int direction)
        {

        }

        //allow the player to access the UI buttons
        private void UnlockRotation()
        {
            UIManager.instance.TurnOnRotate();
        }
    }
}