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

            SetScanDirections();
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if(Input.GetKeyDown(KeyCode.F1)) BlowUp();

            if (nbrCubeEmptyBelow > 1) /*&&hasFallen)*/ BlowUp();

            // if a chaos ball hits the mine cube
            BlowAcross();
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

        private void BlowAcross()
        {

        }


        void RemoveCube(int position)
        {
            if (grid.kuboGrid[position - 1].cubeOnPosition != null)
                grid.kuboGrid[position - 1].cubeOnPosition.GetComponent<CubeBase>().DisableCube();
            else return;
        }
    }
}