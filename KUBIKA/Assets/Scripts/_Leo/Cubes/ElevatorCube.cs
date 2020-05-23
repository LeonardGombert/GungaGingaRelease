using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class ElevatorCube : _CubeScanner
    {
        // Start is called before the first frame update

        public bool isGreen;

        public override  void Start()
        {
            myCubeType = CubeTypes.ElevatorCube;
            myCubeLayer = CubeLayers.cubeFull;
            dynamicEnum = DynamicEnums.Elevator;

            ScannerSet();

            //call base.start AFTER assigning the cube's layers
            base.Start();

            //starts as a static cube
            isStatic = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if(Input.GetKeyDown(KeyCode.B))
            {
                ScannerSet();
            }
        }

        // Appel a la fin des mouvements de tous les cubes
        void Scanning()
        {
            if(grid.kuboGrid[ myIndex - 1 + _DirectionCustom.LocalScanner(baseCubeRotation)].cubeLayers == CubeLayers.cubeMoveable)
            {
                // Detect wall
                Debug.Log("TEST CHECK NODE SCAN " + grid.kuboGrid[myIndex - 1 + _DirectionCustom.LocalScanner(baseCubeRotation)].nodeIndex);
            }
            Debug.LogError("_baseCubeRotation_ | " + baseCubeRotation);
        }
    }
}