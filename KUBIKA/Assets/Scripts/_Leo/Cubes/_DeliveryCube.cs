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

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            CheckForVictory();
        }

        private void CheckForVictory()
        {
            //checks in every "direction"
            foreach (int index in indexesToCheck)
            {
                touchingVictory = ProximityChecker(index, CubeTypes.VictoryCube);
                if (touchingVictory) Debug.Log("Touching a Cube");
            }
        }
    }
}