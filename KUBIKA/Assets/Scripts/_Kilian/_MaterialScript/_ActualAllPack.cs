using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{

    [CreateAssetMenu(fileName = "_ActualAllPack", menuName = "_MaterialPacks/_ActualAllPack", order = 5)]
    public class _ActualAllPack : ScriptableObject
    {
        #region DYNAMIC
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

        #region STATIC

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

        #region EMOTE
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

        #region FX
        #endregion

        public Texture _VoidTex;
    }
}
