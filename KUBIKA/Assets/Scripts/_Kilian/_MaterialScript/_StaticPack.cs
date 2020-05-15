using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kubika.Game
{

    [CreateAssetMenu(fileName = "_StaticPack", menuName = "_MaterialPacks/_StaticPack", order = 1)]
    public class _StaticPack : ScriptableObject
    {
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


    }
}
