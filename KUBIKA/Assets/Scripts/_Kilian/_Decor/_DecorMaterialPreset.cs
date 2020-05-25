using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _DecorMaterialPreset : MonoBehaviour
    {

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
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetMaterial()
        {
            MatProp = new MaterialPropertyBlock();
            meshRenderer = GetComponent<MeshRenderer>();
            meshFilter = GetComponent<MeshFilter>();

            meshRenderer.GetPropertyBlock(MatProp);

            meshFilter.mesh = _MainMesh;

            MatProp.SetTexture("_MainTex", _MainTex);
            MatProp.SetTexture("_InsideTex", _InsideTex);
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
    }
}
