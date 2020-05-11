namespace Kubika.Game
{
    public class _TimerCube : CubeScanner
    {
        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.TimerCube;
            myCubeLayer = CubeLayers.cubeFull;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = true;
        }

        // Update is called once per frame
        new void Update()
        {

        }
    }
}