﻿using System.Collections;
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
        public Texture _BaseTexInside;
        public float _BaseInsideStrength;
        public Color _BaseColorInside;
        [Range(-360, 360)] public float Base_Hue;
        [Range(0, 2)] public float Base_Contrast;
        [Range(0, 2)] public float Base_Saturation;
        [Range(-1, 1)] public float Base_Brightness;

        [Header("Beton")]
        public Texture _BetonTex;
        public Mesh _BetonMesh;
        public Color _BetonColor;
        public Texture _BetonTexInside;
        public float _BetonInsideStrength;
        public Color _BetonColorInside;
        [Range(-360, 360)] public float Beton_Hue;
        [Range(0, 2)] public float Beton_Contrast;
        [Range(0, 2)] public float Beton_Saturation;
        [Range(-1, 1)] public float Beton_Brightness;

        [Header("Elevator")]
        public Texture _ElevatorTex;
        public Texture _ElevatorBackTex;
        public Mesh _ElevatorMesh;
        public Color _ElevatorColor;
        public Texture _ElevatorTexInside;
        public Texture _ElevatorBackTexInside;
        public float _ElevatorInsideStrength;
        public Color _ElevatorColorInside;
        [Range(-360, 360)] public float Elevator_Hue;
        [Range(0, 2)] public float Elevator_Contrast;
        [Range(0, 2)] public float Elevator_Saturation;
        [Range(-1, 1)] public float Elevator_Brightness;

        [Header("Counter")]
        public Texture _CounterTex1;
        public Texture _CounterTex2;
        public Texture _CounterTex3;
        public Texture _CounterTex4;
        public Texture _CounterTex5;
        public Texture _CounterTex6;
        public Texture _CounterTex7;
        public Texture _CounterTex8;
        public Texture _CounterTex9;
        public Mesh _CounterMesh;
        public Color _CounterColor;
        public Texture _CounterTexInside;
        public float _CounterInsideStrength;
        public Color _CounterColorInside;
        [Range(-360, 360)] public float Counter_Hue;
        [Range(0, 2)] public float Counter_Contrast;
        [Range(0, 2)] public float Counter_Saturation;
        [Range(-1, 1)] public float Counter_Brightness;

        [Header("Rotators")]
        public Texture _RotatorsTexLeft;
        public Texture _RotatorsTexRight;
        public Texture _RotatorsTexUI;
        public Mesh _RotatorsMesh;
        public Color _RotatorsColor;
        public Texture _RotatorsTexInside;
        public float _RotatorsInsideStrength;
        public Color _RotatorsColorInside;
        [Range(-360, 360)] public float Rotators_Hue;
        [Range(0, 2)] public float Rotators_Contrast;
        [Range(0, 2)] public float Rotators_Saturation;
        [Range(-1, 1)] public float Rotators_Brightness;

        [Header("Bomb")]
        public Texture _BombTex;
        public Mesh _BombMesh;
        public Color _BombColor;
        public Texture _BombTexInside;
        public float _BombInsideStrength;
        public Color _BombColorInside;
        [Range(-360, 360)] public float Bomb_Hue;
        [Range(0, 2)] public float Bomb_Contrast;
        [Range(0, 2)] public float Bomb_Saturation;
        [Range(-1, 1)] public float Bomb_Brightness;

        [Header("Switch")]
        public Texture _SwitchTexOn;
        public Texture _SwitchTexOff;
        public Texture _SwitchTexInside;
        public Mesh _SwitchMesh;
        public Color _SwitchColor;
        public Color _SwitchColorInside;
        public float _SwitchInsideStrength;
        [Range(-360, 360)] public float Switch_Hue;
        [Range(0, 2)] public float Switch_Contrast;
        [Range(0, 2)] public float Switch_Saturation;
        [Range(-1, 1)] public float Switch_Brightness;

        [Header("Ball")]
        public Texture _BallTex;
        public Mesh _BallMesh;
        public Color _BallColor;
        public Texture _BallTexInside;
        public float _BallInsideStrength;
        public Color _BallColorInside;
        [Range(-360, 360)] public float Ball_Hue;
        [Range(0, 2)] public float Ball_Contrast;
        [Range(0, 2)] public float Ball_Saturation;
        [Range(-1, 1)] public float Ball_Brightness;

        [Header("Pastille")]
        public Texture _PastilleTex;
        public Mesh _PastilleMesh;
        public Color _PastilleColor;
        public Texture _PastilleTexInside;
        public float _PastilleInsideStrength;
        public Color _PastilleColorInside;
        [Range(-360, 360)] public float Pastille_Hue;
        [Range(0, 2)] public float Pastille_Contrast;
        [Range(0, 2)] public float Pastille_Saturation;
        [Range(-1, 1)] public float Pastille_Brightness;
        #endregion

        #region STATIC

        [Header("Empty")]
        public Texture _EmptyTex;
        public Texture _EmptyTex2;
        public Mesh _EmptyMesh;

        [Header("Full")]
        public Texture _FullTex;
        public Texture _FullTex2;
        public Mesh _FullMesh;

        [Header("Top")]
        public Texture _TopTex;
        public Texture _TopTex2;
        public Mesh _TopMesh;

        [Header("Corner")]
        public Texture _CornerTex;
        public Texture _CornerTex2;
        public Mesh _CornerMesh;

        [Header("Triple")]
        public Texture _TripleTex;
        public Texture _TripleTex2;
        public Mesh _TripleMesh;

        [Header("Quad")]
        public Texture _QuadTex;
        public Texture _QuadTex2;
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

        [Space]
        public Texture _EdgeTex;
        public Color _EdgeColor;
        public float _EdgeTexStrength;
    }
}
