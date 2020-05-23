namespace Kubika.Game
{
    public class ConcreteCube : _CubeMove
    {
        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.ConcreteCube;
            myCubeLayer = CubeLayers.cubeMoveable;
            dynamicEnum = DynamicEnums.Beton;

            isSelectable = false;

            //call base.start AFTER assigning the cube's layers
            base.Start();

            //starts as a static cube
            isStatic = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }
    }
}