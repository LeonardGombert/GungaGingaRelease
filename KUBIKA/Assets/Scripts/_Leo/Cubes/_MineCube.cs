namespace Kubika.Game
{
    public class _MineCube : CubeScanner
    {
        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.MineCube;
            myCubeLayer = CubeLayers.cubeMoveable;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            //starts as a static cube
            isStatic = true;
        }

        // Update is called once per frame
        new void Update()
        {

        }
    }
}