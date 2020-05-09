﻿using UnityEngine;

namespace Kubika.Game
{
    //base cube class
    public class CubeBase : MonoBehaviour
    {
        //starts at 1
        public int myIndex; 
        public bool isStatic;

        public LevelEditor._Grid grid;

        // Start is called before the first frame update
        public void Start()
        {
            grid = LevelEditor._Grid.instance;
            isStatic = true;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeFull;
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
    }
}