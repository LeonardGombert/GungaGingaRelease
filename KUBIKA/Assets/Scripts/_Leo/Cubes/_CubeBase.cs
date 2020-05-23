﻿using Kubika.CustomLevelEditor;
using UnityEngine;

namespace Kubika.Game
{
    //base cube class
    public class _CubeBase : MonoBehaviour
    {
        //starts at 1
        public int myIndex;
        //use this to set node data
        public CubeTypes myCubeType;
        public CubeLayers myCubeLayer;

        public bool isStatic;
        public _Grid grid;

        // MATERIAL / FEEDBACK PRESENT
        [Space]
        public DynamicEnums dynamicEnum;
        public StaticEnums staticEnum;

        [Space]
        [Header("MATERIAL INFOS")]
        public Texture _MainTex;
        public Mesh _MainMesh;
        public Color _MainColor;

        [Range(-360, 360)] public float _Hue;
        [Range(0, 2)] public float _Contrast;
        [Range(0, 2)] public float _Saturation;
        [Range(-1, 1)] public float _Brightness;

        public Texture _EmoteTex;
        public float _EmoteStrength;

        public Texture _InsideTex;
        public Color _InsideColor;
        public float _InsideStrength;

        public Texture _EdgeTex;
        public Color _EdgeColor;
        public float _EdgeStrength;

        [HideInInspector] public MeshRenderer meshRenderer;
        [HideInInspector] public MeshFilter meshFilter;
        [HideInInspector] public MaterialPropertyBlock MatProp; // To change Mat Properties

        // Start is called before the first frame update
        public virtual void Start()
        {
            grid = _Grid.instance;
            grid.kuboGrid[myIndex - 1].cubeLayers = myCubeLayer;
            grid.kuboGrid[myIndex - 1].cubeType = myCubeType;
            
            SetScriptablePreset();
        }

        public virtual void Update()
        {
            SetScriptablePreset();
        }

        //Use to update Cube Info in Matrix
        public void SetCubeInfoInMatrix()
        {
            grid.kuboGrid[myIndex - 1].cubeLayers = myCubeLayer;
            grid.kuboGrid[myIndex - 1].cubeType = myCubeType;
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

        public void SetMaterial()
        {
            MatProp = new MaterialPropertyBlock();
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();

            meshRenderer.GetPropertyBlock(MatProp);

            meshFilter.mesh = _MainMesh;

            MatProp.SetTexture("_MainTex", _MainTex);
            MatProp.SetTexture("_InsideTex",_InsideTex);
            MatProp.SetTexture("_EdgeTex", _EdgeTex);
            MatProp.SetTexture("_Emote", _EmoteTex);

            MatProp.SetColor("_MainColor", _MainColor);
            MatProp.SetColor("_InsideColor", _InsideColor);
            MatProp.SetColor("_EdgeColor", _EdgeColor);

            MatProp.SetFloat("_InsideTexStrength", _InsideStrength);
            MatProp.SetFloat("_EdgeTexStrength", _EdgeStrength);
            MatProp.SetFloat("_EmoteStrength", _EmoteStrength);

            MatProp.SetFloat("_Hue", _Hue);
            MatProp.SetFloat("_Contrast", _Contrast);
            MatProp.SetFloat("_Saturation", _Saturation);
            MatProp.SetFloat("_Brightness", _Brightness);

            meshRenderer.SetPropertyBlock(MatProp);
        }

        public void SetScriptablePreset()
        {
            if (dynamicEnum != DynamicEnums.Null && staticEnum == StaticEnums.Null)
            {
                switch(dynamicEnum)
                {
                    case DynamicEnums.Base:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._BaseTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._BaseMesh; 
                            _MainColor = _MaterialCentral.instance.actualPack._BaseColor; 

                            _Hue = _MaterialCentral.instance.actualPack.Base_Hue; 
                            _Contrast = _MaterialCentral.instance.actualPack.Base_Contrast; 
                            _Saturation = _MaterialCentral.instance.actualPack.Base_Saturation; 
                            _Brightness = _MaterialCentral.instance.actualPack.Base_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._BaseTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._BaseColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._BaseInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._BaseEmoteTex; 
                            _EmoteStrength = 1; 
                        }
                        break;
                    case DynamicEnums.Beton:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._BetonTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._BetonMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._BetonColor;

                            _Hue = _MaterialCentral.instance.actualPack.Beton_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Beton_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Beton_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Beton_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._BetonTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._BetonColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._BetonInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._BetonEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Counter:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._CounterTex9; /////
                            _MainMesh = _MaterialCentral.instance.actualPack._CounterMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._CounterColor;

                            _Hue = _MaterialCentral.instance.actualPack.Counter_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Counter_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Counter_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Counter_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._CounterTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._CounterColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._CounterInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._CounterEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Bomb:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._BombTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._BombMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._BombColor;

                            _Hue = _MaterialCentral.instance.actualPack.Bomb_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Bomb_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Bomb_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Bomb_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._BombTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._BombColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._BombInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._BombEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Elevator:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._ElevatorTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._ElevatorMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._ElevatorColor;

                            _Hue = _MaterialCentral.instance.actualPack.Elevator_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Elevator_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Elevator_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Elevator_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._ElevatorTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._ElevatorColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._ElevatorInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._ElevatorEmoteTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case DynamicEnums.Ball:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._BallTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._BallMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._BallColor;

                            _Hue = _MaterialCentral.instance.actualPack.Ball_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Ball_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Ball_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Ball_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._BallTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._BallColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._BallInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._BallEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Switch:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._SwitchTexOn; ///////
                            _MainMesh = _MaterialCentral.instance.actualPack._SwitchMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._SwitchColor;

                            _Hue = _MaterialCentral.instance.actualPack.Switch_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Switch_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Switch_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Switch_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._SwitchTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._SwitchColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._SwitchInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._SwitchEmoteTex;
                            _EmoteStrength = 1;
                        }
                        break;
                    case DynamicEnums.Rotators:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._RotatorsTexLeft; ////////
                            _MainMesh = _MaterialCentral.instance.actualPack._RotatorsMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._RotatorsColor;

                            _Hue = _MaterialCentral.instance.actualPack.Rotators_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Rotators_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Rotators_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Rotators_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._RotatorsTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._RotatorsColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._RotatorsInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._RotatorsEmoteTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case DynamicEnums.Pastille:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._PastilleTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._PastilleMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._PastilleColor;

                            _Hue = _MaterialCentral.instance.actualPack.Pastille_Hue;
                            _Contrast = _MaterialCentral.instance.actualPack.Pastille_Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack.Pastille_Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack.Pastille_Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._PastilleTexInside;
                            _InsideColor = _MaterialCentral.instance.actualPack._PastilleColorInside;
                            _InsideStrength = _MaterialCentral.instance.actualPack._PastilleInsideStrength;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._PastilleEmoteTex;
                            _EmoteStrength = 0;
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
                            _MainTex = _MaterialCentral.instance.actualPack._CornerTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._CornerMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._TextureColor;

                            _Hue = _MaterialCentral.instance.actualPack._Hue;
                            _Contrast = _MaterialCentral.instance.actualPack._Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack._Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack._Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _InsideColor = Color.white;
                            _InsideStrength = 0;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case StaticEnums.Empty:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._EmptyTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._EmptyMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._TextureColor;

                            _Hue = _MaterialCentral.instance.actualPack._Hue;
                            _Contrast = _MaterialCentral.instance.actualPack._Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack._Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack._Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _InsideColor = Color.white;
                            _InsideStrength = 0;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case StaticEnums.Full:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._FullTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._FullMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._TextureColor;

                            _Hue = _MaterialCentral.instance.actualPack._Hue;
                            _Contrast = _MaterialCentral.instance.actualPack._Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack._Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack._Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _InsideColor = Color.white;
                            _InsideStrength = 0;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case StaticEnums.Quad:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._QuadTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._QuadMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._TextureColor;

                            _Hue = _MaterialCentral.instance.actualPack._Hue;
                            _Contrast = _MaterialCentral.instance.actualPack._Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack._Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack._Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _InsideColor = Color.white;
                            _InsideStrength = 0;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case StaticEnums.Top:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._TopTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._TopMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._TextureColor;

                            _Hue = _MaterialCentral.instance.actualPack._Hue;
                            _Contrast = _MaterialCentral.instance.actualPack._Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack._Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack._Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _InsideColor = Color.white;
                            _InsideStrength = 0;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _EmoteStrength = 0;
                        }
                        break;
                    case StaticEnums.Triple:
                        {
                            _MainTex = _MaterialCentral.instance.actualPack._TripleTex;
                            _MainMesh = _MaterialCentral.instance.actualPack._TripleMesh;
                            _MainColor = _MaterialCentral.instance.actualPack._TextureColor;

                            _Hue = _MaterialCentral.instance.actualPack._Hue;
                            _Contrast = _MaterialCentral.instance.actualPack._Contrast;
                            _Saturation = _MaterialCentral.instance.actualPack._Saturation;
                            _Brightness = _MaterialCentral.instance.actualPack._Brightness;

                            _InsideTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _InsideColor = Color.white;
                            _InsideStrength = 0;

                            _EdgeTex = _MaterialCentral.instance.actualPack._EdgeTex;
                            _EdgeColor = _MaterialCentral.instance.actualPack._EdgeColor;
                            _EdgeStrength = _MaterialCentral.instance.actualPack._EdgeTexStrength;

                            _EmoteTex = _MaterialCentral.instance.actualPack._VoidTex;
                            _EmoteStrength = 0;
                        }
                        break;
                }
            }

            SetMaterial();

    }

        #endregion
    }
}