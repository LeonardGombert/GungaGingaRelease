﻿using System;
using UnityEngine;

namespace Kubika.Game
{
    public class _DeliveryCube : CubeScanner
    {
        bool touchingVictory;
        private bool locked;

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
            touchingVictory = ProximityChecker(_DirectionCustom.up, CubeTypes.VictoryCube);
            Debug.DrawRay(transform.position, Vector3.up, Color.green);
            
            if (touchingVictory && locked == false)
            {
                locked = true;
                VictoryConditionManager.instance.IncrementVictory();
            }

            // flip the bools when the delivery cube loses track of the victory cube
            if(touchingVictory == false && locked == true)
            {
                locked = false;
                VictoryConditionManager.instance.DecrementVictory();
            }
        }
    }
}