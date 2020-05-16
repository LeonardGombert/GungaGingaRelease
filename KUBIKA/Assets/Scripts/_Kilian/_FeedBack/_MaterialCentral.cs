using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _MaterialCentral : MonoBehaviour
    {
        // INSTANCE
        private static _MaterialCentral _instance;
        public static _MaterialCentral instance { get { return _instance; } }

        [Space]
        [Header("ALL CUBES")]
        CubeBase[] allCube;

        [Space]
        public _StaticPack[] staticPack;
        public int staticIndex;
        public _DynamicPack[] dynamicPack;
        public int dynamicIndex;
        public _EmotePack[] emotePack;
        public int emoteIndex;
        public _FXPack[] fxPack;
        public int fxIndex;
        [Space]
        public _ActualAllPack actualPack;

        [Header("ACTUAL GRAPH SETTINGS")]
        #region DYNAMIC SETTINGS
        [Space]
        [Header("DYNAMIC-----")]
        [Header("Base")]
        public Texture _BaseTex;
        public Mesh _BaseMesh;
        public Color _BaseColor;
        [Range(-360, 360)] public float Base_Hue;
        [Range(0, 2)] public float Base_Contrast;
        [Range(0, 2)] public float Base_Saturation;
        [Range(-1, 1)] public float Base_Brightness;

        [Header("Beton")]
        public Texture _BetonTex;
        public Mesh _BetonMesh;
        public Color _BetonColor;
        [Range(-360, 360)] public float Beton_Hue;
        [Range(0, 2)] public float Beton_Contrast;
        [Range(0, 2)] public float Beton_Saturation;
        [Range(-1, 1)] public float Beton_Brightness;

        [Header("Elevator")]
        public Texture _ElevatorTex;
        public Mesh _ElevatorMesh;
        public Color _ElevatorColor;
        [Range(-360, 360)] public float Elevator_Hue;
        [Range(0, 2)] public float Elevator_Contrast;
        [Range(0, 2)] public float Elevator_Saturation;
        [Range(-1, 1)] public float Elevator_Brightness;

        [Header("Counter")]
        public Texture _CounterTex;
        public Mesh _CounterMesh;
        public Color _CounterColor;
        [Range(-360, 360)] public float Counter_Hue;
        [Range(0, 2)] public float Counter_Contrast;
        [Range(0, 2)] public float Counter_Saturation;
        [Range(-1, 1)] public float Counter_Brightness;

        [Header("Rotators")]
        public Texture _RotatorsTex;
        public Mesh _RotatorsMesh;
        public Color _RotatorsColor;
        [Range(-360, 360)] public float Rotators_Hue;
        [Range(0, 2)] public float Rotators_Contrast;
        [Range(0, 2)] public float Rotators_Saturation;
        [Range(-1, 1)] public float Rotators_Brightness;

        [Header("Bomb")]
        public Texture _BombTex;
        public Mesh _BombMesh;
        public Color _BombColor;
        [Range(-360, 360)] public float Bomb_Hue;
        [Range(0, 2)] public float Bomb_Contrast;
        [Range(0, 2)] public float Bomb_Saturation;
        [Range(-1, 1)] public float Bomb_Brightness;

        [Header("Switch")]
        public Texture _SwitchTex;
        public Mesh _SwitchMesh;
        public Color _SwitchColor;
        [Range(-360, 360)] public float Switch_Hue;
        [Range(0, 2)] public float Switch_Contrast;
        [Range(0, 2)] public float Switch_Saturation;
        [Range(-1, 1)] public float Switch_Brightness;

        [Header("Ball")]
        public Texture _BallTex;
        public Mesh _BallMesh;
        public Color _BallColor;
        [Range(-360, 360)] public float Ball_Hue;
        [Range(0, 2)] public float Ball_Contrast;
        [Range(0, 2)] public float Ball_Saturation;
        [Range(-1, 1)] public float Ball_Brightness;

        [Header("Pastille")]
        public Texture _PastilleTex;
        public Mesh _PastilleMesh;
        public Color _PastilleColor;
        [Range(-360, 360)] public float Pastille_Hue;
        [Range(0, 2)] public float Pastille_Contrast;
        [Range(0, 2)] public float Pastille_Saturation;
        [Range(-1, 1)] public float Pastille_Brightness;
        #endregion

        #region STATIC SETTINGS
        [Space]
        [Header("STATIC-----")]
        [Header("Empty")]
        public Texture _EmptyTex;
        public Mesh _EmptyMesh;

        [Header("Full")]
        public Texture _FullTex;
        public Mesh _FullMesh;

        [Header("Top")]
        public Texture _TopTex;
        public Mesh _TopMesh;

        [Header("Corner")]
        public Texture _CornerTex;
        public Mesh _CornerMesh;

        [Header("Triple")]
        public Texture _TripleTex;
        public Mesh _TripleMesh;

        [Header("Quad")]
        public Texture _QuadTex;
        public Mesh _QuadMesh;

        [Header("Color Parameters")]
        public Color _TextureColor;
        [Range(-360, 360)] public float _Hue;
        [Range(0, 2)] public float _Contrast;
        [Range(0, 2)] public float _Saturation;
        [Range(-1, 1)] public float _Brightness;
        #endregion

        #region EMOTE SETTINGS
        [Space]
        [Header("EMOTE-----")]
        [Header("Base")]
        public Texture _BaseEmoteTex;

        [Header("Beton")]
        public Texture _BetonEmoteTex;

        [Header("Elevator")]
        public Texture _ElevatorEmoteTex;

        [Header("Counter")]
        public Texture _CounterEmoteTex;

        [Header("Rotators")]
        public Texture _RotatorsEmoteTex;

        [Header("Bomb")]
        public Texture _BombEmoteTex;

        [Header("Switch")]
        public Texture _SwitchEmoteTex;

        [Header("Ball")]
        public Texture _BallEmoteTex;

        [Header("Pastille")]
        public Texture _PastilleEmoteTex;
        #endregion

        #region FX SETTINGS
        #endregion

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            allCube = FindObjectsOfType<CubeBase>();
            
            ResetDynamicPacks(dynamicIndex);
            ResetStaticPacks(staticIndex);
            ResetEmotePacks(emoteIndex);
            ResetFxPacks(fxIndex);

            foreach(CubeBase cube in allCube)
            {
                cube.SetScriptablePreset();
            }
        }


        public void ResetDynamicPacks(int index)
        {

            #region DYNAMIC SETTINGS
            actualPack._BaseTex = dynamicPack[index]._BaseTex;
            actualPack._BaseMesh = dynamicPack[index]._BaseMesh;
            actualPack._BaseColor = dynamicPack[index]._BaseColor;
            actualPack.Base_Hue = dynamicPack[index].Base_Hue;
            actualPack.Base_Contrast = dynamicPack[index].Base_Contrast;
            actualPack.Base_Saturation = dynamicPack[index].Base_Saturation;
            actualPack.Base_Brightness = dynamicPack[index].Base_Brightness;

            actualPack._BetonTex = dynamicPack[index]._BetonTex;
            actualPack._BetonMesh = dynamicPack[index]._BetonMesh;
            actualPack._BetonColor = dynamicPack[index]._BetonColor;
            actualPack.Beton_Hue = dynamicPack[index].Beton_Hue;
            actualPack.Beton_Contrast = dynamicPack[index].Beton_Contrast;
            actualPack.Beton_Saturation = dynamicPack[index].Beton_Saturation;
            actualPack.Beton_Brightness = dynamicPack[index].Beton_Brightness;

            actualPack._ElevatorTex = dynamicPack[index]._ElevatorTex;
            actualPack._ElevatorMesh = dynamicPack[index]._ElevatorMesh;
            actualPack._ElevatorColor = dynamicPack[index]._ElevatorColor;
            actualPack.Elevator_Hue = dynamicPack[index].Elevator_Hue;
            actualPack.Elevator_Contrast = dynamicPack[index].Elevator_Contrast;
            actualPack.Elevator_Saturation = dynamicPack[index].Elevator_Saturation;
            actualPack.Elevator_Brightness = dynamicPack[index].Elevator_Brightness;

            actualPack._CounterTex = dynamicPack[index]._CounterTex;
            actualPack._CounterMesh = dynamicPack[index]._CounterMesh;
            actualPack._CounterColor = dynamicPack[index]._CounterColor;
            actualPack.Counter_Hue = dynamicPack[index].Counter_Hue;
            actualPack.Counter_Contrast = dynamicPack[index].Counter_Contrast;
            actualPack.Counter_Saturation = dynamicPack[index].Counter_Saturation;
            actualPack.Counter_Brightness = dynamicPack[index].Counter_Brightness;

            actualPack._RotatorsTex = dynamicPack[index]._RotatorsTex;
            actualPack._RotatorsMesh = dynamicPack[index]._RotatorsMesh;
            actualPack._RotatorsColor = dynamicPack[index]._RotatorsColor;
            actualPack.Rotators_Hue = dynamicPack[index].Rotators_Hue;
            actualPack.Rotators_Contrast = dynamicPack[index].Rotators_Contrast;
            actualPack.Rotators_Saturation = dynamicPack[index].Rotators_Saturation;
            actualPack.Rotators_Brightness = dynamicPack[index].Rotators_Brightness;

            actualPack._BombTex = dynamicPack[index]._BombTex;
            actualPack._BombMesh = dynamicPack[index]._BombMesh;
            actualPack._BombColor = dynamicPack[index]._BombColor;
            actualPack.Bomb_Hue = dynamicPack[index].Bomb_Hue;
            actualPack.Bomb_Contrast = dynamicPack[index].Bomb_Contrast;
            actualPack.Bomb_Saturation = dynamicPack[index].Bomb_Saturation;
            actualPack.Bomb_Brightness = dynamicPack[index].Bomb_Brightness;

            actualPack._SwitchTex = dynamicPack[index]._SwitchTex;
            actualPack._SwitchMesh = dynamicPack[index]._SwitchMesh;
            actualPack._SwitchColor = dynamicPack[index]._SwitchColor;
            actualPack.Switch_Hue = dynamicPack[index].Switch_Hue;
            actualPack.Switch_Contrast = dynamicPack[index].Switch_Contrast;
            actualPack.Switch_Saturation = dynamicPack[index].Switch_Saturation;
            actualPack.Switch_Brightness = dynamicPack[index].Switch_Brightness;

            actualPack._BallTex = dynamicPack[index]._BallTex;
            actualPack._BallMesh = dynamicPack[index]._BallMesh;
            actualPack._BallColor = dynamicPack[index]._BallColor;
            actualPack.Ball_Hue = dynamicPack[index].Ball_Hue;
            actualPack.Ball_Contrast = dynamicPack[index].Ball_Contrast;
            actualPack.Ball_Saturation = dynamicPack[index].Ball_Saturation;
            actualPack.Ball_Brightness = dynamicPack[index].Ball_Brightness;

            actualPack._PastilleTex = dynamicPack[index]._PastilleTex;
            actualPack._PastilleMesh = dynamicPack[index]._PastilleMesh;
            actualPack._PastilleColor = dynamicPack[index]._PastilleColor;
            actualPack.Pastille_Hue = dynamicPack[index].Pastille_Hue;
            actualPack.Pastille_Contrast = dynamicPack[index].Pastille_Contrast;
            actualPack.Pastille_Saturation = dynamicPack[index].Pastille_Saturation;
            actualPack.Pastille_Brightness = dynamicPack[index].Pastille_Brightness;
            #endregion

        }

        public void ResetStaticPacks(int index)
        {

            #region STATIC SETTINGS
            actualPack._EmptyTex = staticPack[index]._EmptyTex;
            actualPack._EmptyMesh = staticPack[index]._EmptyMesh;

            actualPack._FullTex = staticPack[index]._FullTex;
            actualPack._FullMesh = staticPack[index]._FullMesh;

            actualPack._TopTex = staticPack[index]._TopTex;
            actualPack._TopMesh = staticPack[index]._TopMesh;

            actualPack._CornerTex = staticPack[index]._CornerTex;
            actualPack._CornerMesh = staticPack[index]._CornerMesh;

            actualPack._TripleTex = staticPack[index]._TripleTex;
            actualPack._TripleMesh = staticPack[index]._TripleMesh;

            actualPack._QuadTex = staticPack[index]._QuadTex;
            actualPack._QuadMesh = staticPack[index]._QuadMesh;

            actualPack._TextureColor = staticPack[index]._TextureColor;
            actualPack._Hue = staticPack[index]._Hue;
            actualPack._Contrast = staticPack[index]._Contrast;
            actualPack._Saturation = staticPack[index]._Saturation;
            actualPack._Brightness = staticPack[index]._Brightness;
            #endregion

        }

        public void ResetEmotePacks(int index)
        {

            #region EMOTE SETTINGS
            actualPack._BaseEmoteTex = emotePack[index]._BaseEmoteTex;

            actualPack._BetonEmoteTex = emotePack[index]._BetonEmoteTex;

            actualPack._ElevatorEmoteTex = emotePack[index]._ElevatorEmoteTex;

            actualPack._CounterEmoteTex = emotePack[index]._CounterEmoteTex;

            actualPack._RotatorsEmoteTex = emotePack[index]._RotatorsEmoteTex;

            actualPack._BombEmoteTex = emotePack[index]._BombEmoteTex;

            actualPack._SwitchEmoteTex = emotePack[index]._SwitchEmoteTex;

            actualPack._BallEmoteTex = emotePack[index]._BallEmoteTex;

            actualPack._PastilleEmoteTex = emotePack[index]._PastilleEmoteTex;
            #endregion

        }

        public void ResetFxPacks(int index)
        {

            #region FX SETTINGS
            #endregion

        }

        public void ChangeUniverseRight()
        {
            Debug.Log("RIGHT UNIVERS");
            if(staticIndex < staticPack.Length)
            {
                Debug.Log("Index = " + staticIndex);
                staticIndex += 1;
                ResetStaticPacks(staticIndex);
                foreach (CubeBase cube in allCube)
                {
                    cube.SetScriptablePreset();
                }
            }
        }

        public void ChangeUniverseLeft()
        {
            Debug.Log("LEFT UNIVERS");
            if (staticIndex > 0)
            {
                Debug.Log("Index = " + staticIndex);
                staticIndex -= 1;
                ResetStaticPacks(staticIndex);
                foreach (CubeBase cube in allCube)
                {
                    cube.SetScriptablePreset();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
