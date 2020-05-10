namespace Kubika.Game
{
    public class _VictoryCube : _MoveableCube
    {
        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.VictoryCube;
            myCubeLayer = CubeLayers.cubeMoveable;

            isStatic = false;

            base.Start();
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
        }
    }
}