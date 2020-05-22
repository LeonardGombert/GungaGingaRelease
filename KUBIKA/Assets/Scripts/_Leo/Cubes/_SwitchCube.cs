using Kubika.CustomLevelEditor;

namespace Kubika.Game
{
    public class _SwitchCube : CubeMove
    {
        public bool isActive;

        // Start is called before the first frame update
        public override void Start()
        {
            myCubeType = CubeTypes.StaticCube;
            myCubeLayer = CubeLayers.cubeFull;
            dynamicEnum = DynamicEnums.Switch;

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

        public void StatusUpdate()
        {
            if(!isActive)
            {
                isStatic = true;
                myCubeLayer = CubeLayers.cubeFull;
                myCubeType = CubeTypes.StaticCube;

                SetCubeInfoInMMatrix();
            }

            else if (isActive)
            {
                isStatic = false;
                myCubeLayer = CubeLayers.cubeMoveable;
                myCubeType = CubeTypes.SwitchCube;

                SetCubeInfoInMMatrix();
            }
        }
    }
}