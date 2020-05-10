using UnityEngine;

namespace Kubika.Game
{
    public class _DeliveryCube : CubeScanner
    {
        bool touchingVictory;

        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.DeliveryCube;
            myCubeLayer = CubeLayers.cubeFull;

            forward = backward = left = right = up = down = true;

            SetScanDirections();

            base.Start();
        }

        // Update is called once per frame
        new void Update()
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