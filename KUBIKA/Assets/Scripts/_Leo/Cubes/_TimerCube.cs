using System;
using UnityEngine;

namespace Kubika.Game
{
    public class _TimerCube : CubeScanner
    {
        public int timerValue;
        bool cubePassed;

        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.TimerCube;
            myCubeLayer = CubeLayers.cubeFull;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = true;

            forward = backward = left = right = up = down = true;
            SetScanDirections();
        }

        // Update is called once per frame
        new void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) CheckForCube();
        }

        private void CheckForCube()
        {
            if (timerValue > 0)
            {
                //checks in every "direction"
                foreach (int index in indexesToCheck)
                {
                    cubePassed = ProximityChecker(index, CubeTypes.None, CubeLayers.cubeMoveable);
                    if (cubePassed) break; DecrementTimer();
                }
            }

            else
            {
                Destroy(DestroyCubeProcedure(gameObject));
            }
        }

        private void DecrementTimer()
        {
            cubePassed = false;
            timerValue--;
        }
    }
}