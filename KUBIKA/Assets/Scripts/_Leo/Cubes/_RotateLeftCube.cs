using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _RotateLeftCube : CubeScanner
    {
        private bool pressedDown;
        private bool locked;

        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.RotatorLeftTurner;
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

            CheckIfTouched();
        }

        void CheckIfTouched()
        {
            pressedDown = ProximityChecker(_DirectionCustom.up, CubeTypes.None, CubeLayers.cubeMoveable);
            Debug.DrawRay(transform.position, Vector3.up, Color.green);

            //locked == false ensures that the function doesn't loop
            if (pressedDown && locked == false)
            {
                locked = true;
                _KUBRotation.instance.LeftTurn();
            }

            // flip the bools when the delivery cube loses track of the victory cube
            if (pressedDown == false && locked == true)
            {
                locked = false;
            }
        }
    }
}