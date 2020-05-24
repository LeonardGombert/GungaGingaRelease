﻿using Kubika.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _CubeScanner : _CubeBase
    {
        public int[] indexesToCheck = new int[6];

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
        }

        protected void ScannerSet()
        {
            //facingDirection = CubeFacingDirection.CubeFacingFromRotation(transform.localEulerAngles);
        }


        public void SetScanDirections()
        {
            Debug.Log("Set");
            indexesToCheck[0] = _DirectionCustom.up; //+ 1
            indexesToCheck[1] = _DirectionCustom.down; //- 1
            indexesToCheck[2] = _DirectionCustom.right; //+ the grid size
            indexesToCheck[3] = _DirectionCustom.left; //- the grid size
            indexesToCheck[4] = _DirectionCustom.forward; //+ the grid size squared
            indexesToCheck[5] = _DirectionCustom.backward;//- the grid size squared
        }

        public bool VictoryChecker(int targetIndex)
        {
            if (grid.kuboGrid[myIndex - 1 + targetIndex] != null)
            {
                Debug.Log("Cheking in ");
                if (grid.kuboGrid[myIndex - 1 + targetIndex].cubeOnPosition != null)
                {
                    if (grid.kuboGrid[myIndex - 1 + targetIndex].cubeType >= CubeTypes.BaseVictoryCube
                        && grid.kuboGrid[myIndex - 1 + targetIndex].cubeType <= CubeTypes.SwitchVictoryCube) return true;
                    else return false;
                }
                else return false;

            }
            else return false;
        }

        public bool AnyMoveableChecker(int targetIndex)
        {
            if (grid.kuboGrid[myIndex - 1 + targetIndex] != null)
            {
                if (grid.kuboGrid[myIndex - 1 + targetIndex].cubeOnPosition != null)
                {
                    if (grid.kuboGrid[myIndex - 1 + targetIndex].cubeType >= CubeTypes.BaseVictoryCube
                        && grid.kuboGrid[myIndex - 1 + targetIndex].cubeType <= CubeTypes.ChaosBall) return true;
                    else return false;
                }
                else return false;

            }
            else return false;
        }
    }
}