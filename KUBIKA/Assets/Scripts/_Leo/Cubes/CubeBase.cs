using Kubika.CustomLevelEditor;
using UnityEditor;
using UnityEngine;

namespace Kubika.Game
{
    //base cube class
    public class CubeBase : MonoBehaviour
    {
        //starts at 1
        public int myIndex; 
        public bool isStatic;

        public _Grid grid;
        public int[] indexesToCheck = new int[6];
        public bool forward, backward, left, right, up, down;

        // Start is called before the first frame update
        public void Start()
        {
            grid = _Grid.instance;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeFull;
            isStatic = true;
        }

        public void Update()
        {
            
        }

        //EVERYTHING TO DO WHEN YOU MOVE A CUBE
        void MoveCube()
        {
            /* I - GET CURRENT NODE
             *      a. get this cube's current node with gridRef.kuboGrid[myIndex - 1]
             * 
             * II - LEAVE CURRENT NODE
             *      a. set the cubeOnPosition to empty
             *      b. set the node layer to empty 
             * 
             * III - MOVE TO NEW NODE
             *      a. IMPORTANT -> change this cube's index
             *      b. lerp to target node's transform
             *      c. set the target node's cubeOnPosition as this object
             *      d. set the target node's layer as this object's layer
             */

            //clear the old cubeOnPos. Use (myIndex -1) because myIndex starts at 1 and array starts at 0
            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;

            //set the old node's layer as CUBE EMPTY
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

            //change the index to this cube's old index + value based on how the cube is moving (here let's say it's +4)
            myIndex = grid.kuboGrid[myIndex - 1 + 4].nodeIndex;

            //the index in this case is the cube's "new" index
            transform.position = grid.kuboGrid[myIndex - 1].worldPosition;

            //set this gameobject as the cubeOnPosition of the new node
            grid.kuboGrid[myIndex - 1].cubeOnPosition = gameObject;

            //set the new node's layer as CUBE FULL
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
        }

        // Set "directions" to check in
        public void SetScanDirections()
        {
            if (up) indexesToCheck[0] = 1; //+ 1
            else indexesToCheck[0] = 0;

            if (down) indexesToCheck[1] = -1; //- 1
            else indexesToCheck[1] = 0;

            if (right) indexesToCheck[2] = grid.gridSize; //+ the grid size
            else indexesToCheck[2] = 0;

            if (left) indexesToCheck[3] = -grid.gridSize; //- the grid size
            else indexesToCheck[3] = 0;

            if (forward) indexesToCheck[4] = grid.gridSize * grid.gridSize; //+ the grid size squared
            else indexesToCheck[4] = 0;

            if (backward) indexesToCheck[5] = -(grid.gridSize * grid.gridSize); //- the grid size squared
            else indexesToCheck[5] = 0;
        }

        // Checks if the targeted index has a specific cube OfType on it
        public bool ProximityChecker(int index, CubeTypes checkFor)
        {
            if (grid.kuboGrid[myIndex - 1 + index] != null)
            {
                if (grid.kuboGrid[myIndex - 1 + index].cubeOnPosition != null && grid.kuboGrid[myIndex - 1 + index].cubeType == checkFor) return true;
                else return false;
            }

            else return false;
        }
    }
}