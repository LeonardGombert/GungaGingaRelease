namespace Kubika.Game
{
    public class _MineCube : CubeMove
    {
        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.MineCube;
            myCubeLayer = CubeLayers.cubeMoveable;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            //starts as a static cube
            isStatic = false;
        }

        // Update is called once per frame
        new void Update()
        {

        }
    }
}