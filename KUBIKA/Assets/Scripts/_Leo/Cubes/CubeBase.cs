using Kubika.CustomLevelEditor;
using UnityEngine;

namespace Kubika.Game
{
    //base cube class
    public class CubeBase : MonoBehaviour
    {
        //starts at 1
        public int myIndex;

        //use this to set node data
        public CubeTypes myCubeType;
        public CubeLayers myCubeLayer;

        public bool isStatic;
        public _Grid grid;

        // MATERIAL / FEEDBACK PRESENT
        public DynamicEnums dynamicEnum;
        public StaticEnums staticEnum;

        public Texture _MainTex;
        public Mesh _MainMesh;
        public Color _MainColor;

        [Range(-360, 360)] public float _Hue;
        [Range(0, 2)] public float _Contrast;
        [Range(0, 2)] public float _Saturation;
        [Range(-1, 1)] public float _Brightness;

        public Texture _EmoteTex;
        public float _EmoteStrength;

        // Start is called before the first frame update
        public virtual void Start()
        {
            grid = _Grid.instance;
            grid.kuboGrid[myIndex - 1].cubeLayers = myCubeLayer;
            grid.kuboGrid[myIndex - 1].cubeType = myCubeType;
        }

        public virtual void Update()
        {

        }

        public void DisableCube()
        {
            gameObject.SetActive(false);
            HideCubeProcedure();
        }

        public void EnableCube()
        {
            ReviveCubeProcedure();
        }

        //gets called when you "hide"/"destroy a cube
        void HideCubeProcedure()
        {
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
            grid.kuboGrid[myIndex - 1].cubeType = CubeTypes.None;
            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
        }

        // call when "reactivating" cubes
        void ReviveCubeProcedure()
        {
            grid.kuboGrid[myIndex - 1].cubeLayers = myCubeLayer;
            grid.kuboGrid[myIndex - 1].cubeType = myCubeType;
            grid.kuboGrid[myIndex - 1].cubeOnPosition = gameObject;
        }

        #region MATERIAL

        void SetMaterialPreset()
        {
            if (dynamicEnum != DynamicEnums.Null && staticEnum == StaticEnums.Null)
            {
                switch(dynamicEnum)
                {
                    case DynamicEnums.Base:
                        {
                            _MainTex = _MaterialCentral.instance._BaseTex;
                            _MainMesh = _MaterialCentral.instance._BaseMesh; 
                            _MainColor = _MaterialCentral.instance._BaseColor; 

                            _Hue = _MaterialCentral.instance.Base_Hue; 
                            _Contrast = _MaterialCentral.instance.Base_Contrast; 
                            _Saturation = _MaterialCentral.instance.Base_Saturation; 
                            _Brightness = _MaterialCentral.instance.Base_Brightness; 

                            _EmoteTex = _MaterialCentral.instance._BaseEmoteTex; 
                            _EmoteStrength = 1; 
                        }
                        break;
                    case DynamicEnums.Beton:
                        {
                            _MainTex = _MaterialCentral.instance._BetonTex;
                            _MainMesh = _MaterialCentral.instance._BetonMesh;
                            _MainColor = _MaterialCentral.instance._BetonColor;

                            _Hue = _MaterialCentral.instance.Beton_Hue;
                            _Contrast = _MaterialCentral.instance.Beton_Contrast;
                            _Saturation = _MaterialCentral.instance.Beton_Saturation;
                            _Brightness = _MaterialCentral.instance.Beton_Brightness;

                            _EmoteTex = _MaterialCentral.instance._BetonEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Counter:
                        {
                            _MainTex = _MaterialCentral.instance._CounterTex;
                            _MainMesh = _MaterialCentral.instance._CounterMesh;
                            _MainColor = _MaterialCentral.instance._CounterColor;

                            _Hue = _MaterialCentral.instance.Counter_Hue;
                            _Contrast = _MaterialCentral.instance.Counter_Contrast;
                            _Saturation = _MaterialCentral.instance.Counter_Saturation;
                            _Brightness = _MaterialCentral.instance.Counter_Brightness;

                            _EmoteTex = _MaterialCentral.instance._CounterEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Bomb:
                        {
                            _MainTex = _MaterialCentral.instance._BombTex;
                            _MainMesh = _MaterialCentral.instance._BombMesh;
                            _MainColor = _MaterialCentral.instance._BombColor;

                            _Hue = _MaterialCentral.instance.Bomb_Hue;
                            _Contrast = _MaterialCentral.instance.Bomb_Contrast;
                            _Saturation = _MaterialCentral.instance.Bomb_Saturation;
                            _Brightness = _MaterialCentral.instance.Bomb_Brightness;

                            _EmoteTex = _MaterialCentral.instance._BombEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Elevator:
                        {
                            _MainTex = _MaterialCentral.instance._ElevatorTex;
                            _MainMesh = _MaterialCentral.instance._ElevatorMesh;
                            _MainColor = _MaterialCentral.instance._ElevatorColor;

                            _Hue = _MaterialCentral.instance.Elevator_Hue;
                            _Contrast = _MaterialCentral.instance.Elevator_Contrast;
                            _Saturation = _MaterialCentral.instance.Elevator_Saturation;
                            _Brightness = _MaterialCentral.instance.Elevator_Brightness;

                            _EmoteTex = _MaterialCentral.instance._ElevatorEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Ball:
                        {
                            _MainTex = _MaterialCentral.instance._BallTex;
                            _MainMesh = _MaterialCentral.instance._BallMesh;
                            _MainColor = _MaterialCentral.instance._BallColor;

                            _Hue = _MaterialCentral.instance.Ball_Hue;
                            _Contrast = _MaterialCentral.instance.Ball_Contrast;
                            _Saturation = _MaterialCentral.instance.Ball_Saturation;
                            _Brightness = _MaterialCentral.instance.Ball_Brightness;

                            _EmoteTex = _MaterialCentral.instance._BallEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Switch:
                        {
                            _MainTex = _MaterialCentral.instance._SwitchTex;
                            _MainMesh = _MaterialCentral.instance._SwitchMesh;
                            _MainColor = _MaterialCentral.instance._SwitchColor;

                            _Hue = _MaterialCentral.instance.Switch_Hue;
                            _Contrast = _MaterialCentral.instance.Switch_Contrast;
                            _Saturation = _MaterialCentral.instance.Switch_Saturation;
                            _Brightness = _MaterialCentral.instance.Switch_Brightness;

                            _EmoteTex = _MaterialCentral.instance._SwitchEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Rotators:
                        {
                            _MainTex = _MaterialCentral.instance._RotatorsTex;
                            _MainMesh = _MaterialCentral.instance._RotatorsMesh;
                            _MainColor = _MaterialCentral.instance._RotatorsColor;

                            _Hue = _MaterialCentral.instance.Rotators_Hue;
                            _Contrast = _MaterialCentral.instance.Rotators_Contrast;
                            _Saturation = _MaterialCentral.instance.Rotators_Saturation;
                            _Brightness = _MaterialCentral.instance.Rotators_Brightness;

                            _EmoteTex = _MaterialCentral.instance._RotatorsEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Pastille:
                        {
                            _MainTex = _MaterialCentral.instance._PastilleTex;
                            _MainMesh = _MaterialCentral.instance._PastilleMesh;
                            _MainColor = _MaterialCentral.instance._PastilleColor;

                            _Hue = _MaterialCentral.instance.Pastille_Hue;
                            _Contrast = _MaterialCentral.instance.Pastille_Contrast;
                            _Saturation = _MaterialCentral.instance.Pastille_Saturation;
                            _Brightness = _MaterialCentral.instance.Pastille_Brightness;

                            _EmoteTex = _MaterialCentral.instance._PastilleEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                }
            }
            else if (dynamicEnum == DynamicEnums.Null && staticEnum != StaticEnums.Null)
            {
                switch(staticEnum)
                {
                    case StaticEnums.Corner:
                        {

                        }
                        break;
                    case StaticEnums.Empty:
                        {

                        }
                        break;
                    case StaticEnums.Full:
                        {

                        }
                        break;
                    case StaticEnums.Quad:
                        {

                        }
                        break;
                    case StaticEnums.Top:
                        {

                        }
                        break;
                    case StaticEnums.Triple:
                        {

                        }
                        break;
                }
            }


    }

        #endregion
    }
}