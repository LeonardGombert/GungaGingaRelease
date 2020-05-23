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

        void ScannerSet()
        {
            baseCubeRotation = _DirectionCustom.ScannerSet(Vector3Int.up, transform);
            _DirectionCustom.LocalScanner(baseCubeRotation);
            Debug.LogError("_baseCubeRotation_ | " + baseCubeRotation);
        }
    }
}