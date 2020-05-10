using UnityEngine;
using Kubika.Game;

namespace Kubika.CustomLevelEditor
{
    [System.Serializable]
    public class Node
    {
        public int nodeIndex;
        public GameObject cubeOnPosition;

        //used for saving and loading levels
        public CubeTypes cubeType;

        public CubeLayers cubeLayers;

        public int xCoord, yCoord, zCoord;
        public Vector3 worldPosition;
    }
}
