using Kubika.Game;
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
            SetScanDirections();
        }

        public override void Update()
        {
            base.Update();
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

        // Checks if the targeted index has a specific cube OfType on it
        public bool ProximityChecker(int index, CubeTypes checkForType = CubeTypes.None, CubeLayers checkForLayer = CubeLayers.None)
        {
            if (grid.kuboGrid[myIndex - 1 + index] != null)
            {
                if (grid.kuboGrid[myIndex - 1 + index].cubeOnPosition != null)
                {
                    //check for a cube type on a cube layer
                    if (checkForLayer != CubeLayers.None && checkForType != CubeTypes.None)
                        if (grid.kuboGrid[myIndex - 1 + index].cubeType == checkForType
                            && grid.kuboGrid[myIndex - 1 + index].cubeLayers == checkForLayer) return true;
                        else return false;

                    //check for any cube type on a specific layer
                    else if (checkForLayer != CubeLayers.None
                        && grid.kuboGrid[myIndex - 1 + index].cubeLayers == checkForLayer) return true;

                    //check for a specific cube type on any layer
                    else if (checkForType != CubeTypes.None &&
                        grid.kuboGrid[myIndex - 1 + index].cubeType == checkForType) return true;

                    else return false;
                }
                else return false;
            }

            else return false;
        }

        public bool VictoryChecker(int targetIndex)
        {
            if (grid.kuboGrid[targetIndex - 1 + targetIndex] != null)
            {
                if (grid.kuboGrid[targetIndex - 1 + targetIndex].cubeOnPosition != null)
                {
                    if (grid.kuboGrid[targetIndex - 1 + targetIndex].cubeType >= CubeTypes.BaseVictoryCube 
                        && grid.kuboGrid[targetIndex - 1 + targetIndex].cubeType <= CubeTypes.SwitchVictoryCube) return true;
                    else return false;
                }
                else return false;

            }
            else return false;
        }
    }
}