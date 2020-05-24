﻿using UnityEngine;

namespace Kubika.CustomLevelEditor
{
    [System.Serializable]
    public class Node
    {
        public int nodeIndex;
        public int nodeIndex0;
        public int nodeIndex1;
        public int nodeIndex2;

        public GameObject cubeOnPosition;

        //used for saving and loading levels
        public string cubeType;

        public CubeLayers cubeLayers;

        public FacingDirection facingDirection;

        public int xCoord, yCoord, zCoord;
        
        public Vector3 worldPosition;        
        public Vector3 worldRotation;
    }
}
