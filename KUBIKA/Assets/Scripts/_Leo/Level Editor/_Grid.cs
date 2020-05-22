using Kubika.Game;
using Kubika.Saving;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kubika.CustomLevelEditor
{
    public class _Grid : MonoBehaviour
    {
        private static _Grid _instance;
        public static _Grid instance { get { return _instance; } }

        Vector3Int gridSizeVector;

        [HideInInspector] public int gridSize = 12; //the square root of the Matrix

        [HideInInspector] public int gridMargin = 4;
        [HideInInspector] public int centerPosition;

        [Range(0f, 2f)] public float offset;

        public Node[] kuboGrid;
        public List<GameObject> placedObjects = new List<GameObject>();

        public GameObject nodeVizPrefab;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;

            gridSize = 12;
            gridMargin = 4;
        }

        // Start is called before the first frame update
        private void Start()
        {
            CreateGrid();
            _DirectionCustom.matrixLengthDirection = gridSize;
        }

        private void CreateGrid()
        {
            gridSizeVector = new Vector3Int(gridSize, gridSize, gridSize);

            centerPosition = gridSize * gridSize * gridMargin + gridSize * gridMargin + gridMargin;

            kuboGrid = new Node[gridSize * gridSize * gridSize];

            for (int index = 1, z = 0; z < gridSizeVector.z; z++)
            {
                for (int x = 0; x < gridSizeVector.x; x++)
                {
                    for (int y = 0; y < gridSizeVector.y; y++, index++)
                    {
                        Vector3 nodePosition = new Vector3(x * offset, y * offset, z * offset);

                        Node currentNode = new Node();

                        currentNode.xCoord = x;
                        currentNode.yCoord = y;
                        currentNode.zCoord = z;

                        currentNode.nodeIndex = index;
                        currentNode.worldPosition = nodePosition;
                        currentNode.cubeLayers = CubeLayers.cubeEmpty;
                        currentNode.cubeType = CubeTypes.None;

                        kuboGrid[index - 1] = currentNode;
                    }
                }
            }

            if (ScenesManager.isDevScene || ScenesManager.isLevelEditor && LevelEditor.instance != null) 
                LevelEditor.instance.GenerateBaseGrid();
        }

        public void RefreshGrid()
        {
            ResetGrid();
            CreateGrid();
        }

        //used to clear a level
        public void ResetGrid()
        {
            //destroy all cubes in grid and reset nodes to base state
            foreach (Node node in kuboGrid)
            {
                node.cubeLayers = CubeLayers.cubeEmpty;
                Destroy(node.cubeOnPosition);
            }
        }

        //set all index to their default state
        public void ResetIndexGrid()
        {
            for (int i = 0; i < kuboGrid.Length; i++)
            {
                kuboGrid[i].cubeOnPosition = null;
                kuboGrid[i].cubeLayers = CubeLayers.cubeEmpty;
            }
        }
    }
}