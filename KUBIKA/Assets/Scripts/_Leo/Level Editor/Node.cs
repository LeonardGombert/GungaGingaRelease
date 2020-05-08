using UnityEngine;
using Kubika.Game;

namespace Kubika.LevelEditor
{
    [System.Serializable]
    public class Node
    {
        public int nodeIndex;
        public GameObject cubeOnPosition;

        public CubeLayers cubeLayers;

        public int xCoord, yCoord, zCoord;
        public Vector3 worldPosition;
    }
}
