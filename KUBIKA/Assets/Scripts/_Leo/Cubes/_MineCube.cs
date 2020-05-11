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

            if (nbrCubeEmptyBelow > 1) //&&hasFallen)
            {
                BlowUp();
            }

            // if a chaos ball hits the mine cube
        }

        void BlowUp()
        {
            for (int currentCube = 0; currentCube < myIndex + grid.gridSize; currentCube++)
            {
                //if i != the cube at the top of the row
                if (currentCube % grid.gridSize != 0)
                {
                    grid.kuboGrid[myIndex - 1 + currentCube].cubeOnPosition.GetComponent<CubeBase>().DisableCube();
                }
            }

            //DisableCube();
            
            Debug.Log("Mon principal but est de explose");
        }
    }
}