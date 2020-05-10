namespace Kubika.Game
{
    public class _VictoryCube : _MoveableCube
    {
        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.VictoryCube;
            myCubeLayer = CubeLayers.cubeMoveable;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
        }
    }
}