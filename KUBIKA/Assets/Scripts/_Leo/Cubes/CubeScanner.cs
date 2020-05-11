using Kubika.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Kubika.Game
{
    public class CubeScanner : CubeBase
    {
        public int[] indexesToCheck = new int[6];
        public bool forward, backward, left, right, up, down;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
        }

        private void OnEnable()
        {
            SetScanDirections();
        }

        // Set "directions" to check in
        public void SetScanDirections()
        {
            if (up) indexesToCheck[0] = _DirectionCustom.up; //+ 1
            else indexesToCheck[0] = 0;

            if (down) indexesToCheck[1] = _DirectionCustom.down; //- 1
            else indexesToCheck[1] = 0;

            if (right) indexesToCheck[2] = _DirectionCustom.right; //+ the grid size
            else indexesToCheck[2] = 0;

            if (left) indexesToCheck[3] = _DirectionCustom.left; //- the grid size
            else indexesToCheck[3] = 0;

            if (forward) indexesToCheck[4] = _DirectionCustom.forward; //+ the grid size squared
            else indexesToCheck[4] = 0;

            if (backward) indexesToCheck[5] = _DirectionCustom.backward;//- the grid size squared
            else indexesToCheck[5] = 0;
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

                    //check for cubes on a layer
                    else if (checkForLayer != CubeLayers.None
                        && grid.kuboGrid[myIndex - 1 + index].cubeLayers == checkForLayer) return true;

                    //check for specific type of cube
                    else if (checkForType != CubeTypes.None && 
                        grid.kuboGrid[myIndex - 1 + index].cubeType == checkForType) return true;
                    
                    else return false;
                }
                else return false;
            }

            else return false;
        }
    }
}