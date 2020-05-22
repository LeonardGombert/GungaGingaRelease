using System;
using UnityEngine;

namespace Kubika.Game
{
    public class _DeliveryCube : CubeScanner
    {
        bool touchingVictory;

        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.DeliveryCube;
            myCubeLayer = CubeLayers.cubeFull;
            dynamicEnum = DynamicEnums.Pastille;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            // ONLY CALL THIS WHEN CUBES HAVE FINISHED MOVING
            CheckForVictory();
        }

        private void CheckForVictory()
        {
            touchingVictory = ProximityChecker(_DirectionCustom.forward, CubeTypes.VictoryCube);
            Debug.DrawRay(transform.position, _DirectionCustom.vectorForward, Color.green);
            if (touchingVictory) Debug.Log("Touching a Cube");
        }
    }
}