using System;
using UnityEngine;

namespace Kubika.Game
{
    public class _MineCube : CubeMove
    {
        int cubeTopIndex;

        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.MineCube;
            myCubeLayer = CubeLayers.cubeMoveable;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;

            forward = backward = left = right = up = down = true;

            SetScanDirections();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if(Input.GetKeyDown(KeyCode.F1)) BlowUp();
            if(Input.GetKeyDown(KeyCode.F2)) BlowAcross(myIndex - 1 + _DirectionCustom.right);
            if(Input.GetKeyDown(KeyCode.F3)) BlowAcross(myIndex - 1 + _DirectionCustom.forward);

            if (nbrCubeEmptyBelow > 1) /*&&hasFallen)*/ BlowUp();

            // if a chaos ball hits the mine cube
            //BlowAcross();
        }

        void BlowUp()
        {
            //shoot up
            for (int position = myIndex; position < grid.gridSize * grid.gridSize * grid.gridSize; position++)
            {
                RemoveCube(position);
                if (!MatrixLimitCalcul(position, _DirectionCustom.up)) break;
            }

            //shoot down
            for (int position = myIndex - 1; position > 0; position--)
            {
                RemoveCube(position);
                if (!MatrixLimitCalcul(position, _DirectionCustom.down)) break;
            }
        }

        private void BlowAcross(int ballIndex)
        {
            //explode on Z axis
            if(ballIndex == myIndex -1 + _DirectionCustom.forward || ballIndex == myIndex - 1 + _DirectionCustom.backward)
            {
                //shoot forwards
                for (int position = myIndex; position < grid.gridSize * grid.gridSize * grid.gridSize; position += _DirectionCustom.forward)
                {
                    RemoveCube(position);
                    if (!MatrixLimitCalcul(position, _DirectionCustom.forward)) break;
                } 
                
                //shoot back
                for (int position = myIndex; position < grid.gridSize * grid.gridSize * grid.gridSize; position += _DirectionCustom.backward)
                {
                    RemoveCube(position);
                    if (!MatrixLimitCalcul(position, _DirectionCustom.backward)) break;
                }
            }

            //explode on X axis
            else if (ballIndex == myIndex - 1 + _DirectionCustom.right || ballIndex == myIndex - 1 + _DirectionCustom.left)
            {
                //shoot right
                for (int position = myIndex; position < grid.gridSize * grid.gridSize * grid.gridSize; position += _DirectionCustom.right)
                {
                    RemoveCube(position);
                    if (!MatrixLimitCalcul(position, _DirectionCustom.right)) break;
                }

                //shoot left
                for (int position = myIndex; position < grid.gridSize * grid.gridSize * grid.gridSize; position += _DirectionCustom.left)
                {
                    RemoveCube(position);
                    if (!MatrixLimitCalcul(position, _DirectionCustom.left)) break;
                }
            }
        }

        void RemoveCube(int position)
        {
            if (grid.kuboGrid[position - 1].cubeOnPosition != null)
                grid.kuboGrid[position - 1].cubeOnPosition.GetComponent<CubeBase>().DisableCube();
            else return;
        }
    }
}