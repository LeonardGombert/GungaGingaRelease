using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    [System.Serializable]
    public class Node
    {
        public int nodeIndex;
        public GameObject cubeOnPosition;

        public int xCoord, yCoord, zCoord;
        public Vector3 worldPosition;
    }
}
