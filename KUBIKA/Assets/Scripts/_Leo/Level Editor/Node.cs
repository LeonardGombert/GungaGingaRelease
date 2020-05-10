using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kubika.Game;

namespace Kubika.LevelEditor
{
    [System.Serializable]
    public class Node
    {
        public int nodeIndex;
        public int nodeIndex0;
        public int nodeIndex1;
        public int nodeIndex2;
        public GameObject cubeOnPosition;

        public CubeLayers cubeLayers;

        public int xCoord, yCoord, zCoord;
        public Vector3 worldPosition;
    }
}
