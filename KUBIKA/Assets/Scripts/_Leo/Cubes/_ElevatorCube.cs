using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _ElevatorCube : CubeScanner
    {
        // Start is called before the first frame update
        public override  void Start()
        {
            myCubeType = CubeTypes.ElevatorCube;
            myCubeLayer = CubeLayers.cubeFull;
            dynamicEnum = DynamicEnums.Elevator;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            //starts as a static cube
            isStatic = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }
    }
}