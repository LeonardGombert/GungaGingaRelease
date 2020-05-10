using UnityEngine;

namespace Kubika.Game
{
    public class _DeliveryCube : CubeBase
    {
        bool touchingCube;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            forward = backward = left = right = up = down = true;
            SetScanDirections();
        }

        // Update is called once per frame
        new void Update()
        {
            //checks in every "direction"
            foreach (int index in indexesToCheck)
            {
                touchingCube = ProximityChecker(index, CubeTypes.VictoryCube);
                if (touchingCube) Debug.Log("Touching a Cube");
            }
        }
    }
}