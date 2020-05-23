namespace Kubika.Game
{
    public class ChaosBall : _CubeMove
    {
        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.ChaosBall;
            myCubeLayer = CubeLayers.cubeMoveable;
            dynamicEnum = DynamicEnums.Ball;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }
    }
}