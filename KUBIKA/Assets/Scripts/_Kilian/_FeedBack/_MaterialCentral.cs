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

        public _StaticPack staticPack;
        public _DynamicPack dynamicPack;
        public _EmotePack emotePack;
        public _FXPack fxPack;
        [Space]
        public _ActualAllPack actualPack;
        [Space]
        public Texture EmptyTex;

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
            ResetDynamicPacks();
            ResetStaticPacks();
            ResetEmotePacks();
            ResetFxPacks();
        }


        public void ResetDynamicPacks()
        {

            #region DYNAMIC SETTINGS
            actualPack._BaseTex = dynamicPack._BaseTex;
            actualPack._BaseMesh = dynamicPack._BaseMesh;
            actualPack._BaseColor = dynamicPack._BaseColor;
            actualPack.Base_Hue = dynamicPack.Base_Hue;
            actualPack.Base_Contrast = dynamicPack.Base_Contrast;
            actualPack.Base_Saturation = dynamicPack.Base_Saturation;
            actualPack.Base_Brightness = dynamicPack.Base_Brightness;

            actualPack._BetonTex = dynamicPack._BetonTex;
            actualPack._BetonMesh = dynamicPack._BetonMesh;
            actualPack._BetonColor = dynamicPack._BetonColor;
            actualPack.Beton_Hue = dynamicPack.Beton_Hue;
            actualPack.Beton_Contrast = dynamicPack.Beton_Contrast;
            actualPack.Beton_Saturation = dynamicPack.Beton_Saturation;
            actualPack.Beton_Brightness = dynamicPack.Beton_Brightness;

            actualPack._ElevatorTex = dynamicPack._ElevatorTex;
            actualPack._ElevatorMesh = dynamicPack._ElevatorMesh;
            actualPack._ElevatorColor = dynamicPack._ElevatorColor;
            actualPack.Elevator_Hue = dynamicPack.Elevator_Hue;
            actualPack.Elevator_Contrast = dynamicPack.Elevator_Contrast;
            actualPack.Elevator_Saturation = dynamicPack.Elevator_Saturation;
            actualPack.Elevator_Brightness = dynamicPack.Elevator_Brightness;

            actualPack._CounterTex = dynamicPack._CounterTex;
            actualPack._CounterMesh = dynamicPack._CounterMesh;
            actualPack._CounterColor = dynamicPack._CounterColor;
            actualPack.Counter_Hue = dynamicPack.Counter_Hue;
            actualPack.Counter_Contrast = dynamicPack.Counter_Contrast;
            actualPack.Counter_Saturation = dynamicPack.Counter_Saturation;
            actualPack.Counter_Brightness = dynamicPack.Counter_Brightness;

            actualPack._RotatorsTex = dynamicPack._RotatorsTex;
            actualPack._RotatorsMesh = dynamicPack._RotatorsMesh;
            actualPack._RotatorsColor = dynamicPack._RotatorsColor;
            actualPack.Rotators_Hue = dynamicPack.Rotators_Hue;
            actualPack.Rotators_Contrast = dynamicPack.Rotators_Contrast;
            actualPack.Rotators_Saturation = dynamicPack.Rotators_Saturation;
            actualPack.Rotators_Brightness = dynamicPack.Rotators_Brightness;

            actualPack._BombTex = dynamicPack._BombTex;
            actualPack._BombMesh = dynamicPack._BombMesh;
            actualPack._BombColor = dynamicPack._BombColor;
            actualPack.Bomb_Hue = dynamicPack.Bomb_Hue;
            actualPack.Bomb_Contrast = dynamicPack.Bomb_Contrast;
            actualPack.Bomb_Saturation = dynamicPack.Bomb_Saturation;
            actualPack.Bomb_Brightness = dynamicPack.Bomb_Brightness;

            actualPack._SwitchTex = dynamicPack._SwitchTex;
            actualPack._SwitchMesh = dynamicPack._SwitchMesh;
            actualPack._SwitchColor = dynamicPack._SwitchColor;
            actualPack.Switch_Hue = dynamicPack.Switch_Hue;
            actualPack.Switch_Contrast = dynamicPack.Switch_Contrast;
            actualPack.Switch_Saturation = dynamicPack.Switch_Saturation;
            actualPack.Switch_Brightness = dynamicPack.Switch_Brightness;

            actualPack._BallTex = dynamicPack._BallTex;
            actualPack._BallMesh = dynamicPack._BallMesh;
            actualPack._BallColor = dynamicPack._BallColor;
            actualPack.Ball_Hue = dynamicPack.Ball_Hue;
            actualPack.Ball_Contrast = dynamicPack.Ball_Contrast;
            actualPack.Ball_Saturation = dynamicPack.Ball_Saturation;
            actualPack.Ball_Brightness = dynamicPack.Ball_Brightness;

            actualPack._PastilleTex = dynamicPack._PastilleTex;
            actualPack._PastilleMesh = dynamicPack._PastilleMesh;
            actualPack._PastilleColor = dynamicPack._PastilleColor;
            actualPack.Pastille_Hue = dynamicPack.Pastille_Hue;
            actualPack.Pastille_Contrast = dynamicPack.Pastille_Contrast;
            actualPack.Pastille_Saturation = dynamicPack.Pastille_Saturation;
            actualPack.Pastille_Brightness = dynamicPack.Pastille_Brightness;
            #endregion

        }

        public void ResetStaticPacks()
        {

            #region STATIC SETTINGS
            actualPack._EmptyTex = staticPack._EmptyTex;
            actualPack._EmptyMesh = staticPack._EmptyMesh;

            actualPack._FullTex = staticPack._FullTex;
            actualPack._FullMesh = staticPack._FullMesh;

            actualPack._TopTex = staticPack._TopTex;
            actualPack._TopMesh = staticPack._TopMesh;

            actualPack._CornerTex = staticPack._CornerTex;
            actualPack._CornerMesh = staticPack._CornerMesh;

            actualPack._TripleTex = staticPack._TripleTex;
            actualPack._TripleMesh = staticPack._TripleMesh;

            actualPack._QuadTex = staticPack._QuadTex;
            actualPack._QuadMesh = staticPack._QuadMesh;

            actualPack._TextureColor = staticPack._TextureColor;
            actualPack._Hue = staticPack._Hue;
            actualPack._Contrast = staticPack._Contrast;
            actualPack._Saturation = staticPack._Saturation;
            actualPack._Brightness = staticPack._Brightness;
            #endregion

        }

        public void ResetEmotePacks()
        {

            #region EMOTE SETTINGS
            actualPack._BaseEmoteTex = emotePack._BaseEmoteTex;

            actualPack._BetonEmoteTex = emotePack._BetonEmoteTex;

            actualPack._ElevatorEmoteTex = emotePack._ElevatorEmoteTex;

            actualPack._CounterEmoteTex = emotePack._CounterEmoteTex;

            actualPack._RotatorsEmoteTex = emotePack._RotatorsEmoteTex;

            actualPack._BombEmoteTex = emotePack._BombEmoteTex;

            actualPack._SwitchEmoteTex = emotePack._SwitchEmoteTex;

            actualPack._BallEmoteTex = emotePack._BallEmoteTex;

            actualPack._PastilleEmoteTex = emotePack._PastilleEmoteTex;
            #endregion

        }

        public void ResetFxPacks()
        {

            #region FX SETTINGS
            #endregion

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
