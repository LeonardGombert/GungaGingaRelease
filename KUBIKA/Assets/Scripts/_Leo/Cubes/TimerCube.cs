﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class TimerCube : _CubeScanner
    {
        public int timerValue = 2;
        bool touchedCube;

        public List<int> touchingCubeIndex = new List<int>();
        private bool hasCubes;

        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.TimerCube;
            myCubeLayer = CubeLayers.cubeFull;
            dynamicEnum = DynamicEnums.Counter;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = true;

            SetScanDirections();
        }

        // Update is called once per frame
        public override void Update()
        {
            //call this when all cubes have finished moving
            CubeListener();
        }

        private void CubeListener()
        {
            //if the timer already has cubes it is following
            if (hasCubes)
            {
                Debug.Log("looking out for my cubes");

                // check each registered index to make sure the cube is still there
                foreach (int index in touchingCubeIndex)
                {
                    // if one or more of the cubes have moved, reset the bools
                    if (grid.kuboGrid[index - 1].cubeOnPosition == null)
                    {
                        //reset find cube variables
                        touchedCube = false;
                        hasCubes = false;

                        //decrement the value by 1 for the next pass
                        Debug.Log("Man down !");
                        timerValue--;
                    }
                }
            }

            if (timerValue >= 0)
            {
                // forget the cubes you've already registered (in case only 1 moves)
                touchingCubeIndex.Clear();

                // check in every "direction"
                foreach (int index in indexesToCheck)
                {
                    touchedCube = ProximityChecker(index, CubeTypes.None, CubeLayers.cubeMoveable);

                    // if you touch a cube
                    if (touchedCube)
                    {
                        // save that cube to the list of "registered" cubes
                        touchingCubeIndex.Add(myIndex + index);

                        // set your state to "has registered cubes"
                        hasCubes = true;
                    }
                }

            }

            else DisableCube();
        }

    }
}