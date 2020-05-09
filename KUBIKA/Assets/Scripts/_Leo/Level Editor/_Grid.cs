using Kubika.Game;
using System;
using UnityEngine;

namespace Kubika.LevelEditor
{
    public class _Grid : MonoBehaviour
    {
        private static _Grid _instance;
        public static _Grid instance { get { return _instance; } }

        Vector3Int gridSizeVector;

        public int gridSize; //the square root of the Matrix
        public int gridMargin;
        public int centerPosition;
        public LevelEditorStart startingPos;

        [Range(0.5f, 5)] public float offset;

        public Node[] kuboGrid;

        public GameObject nodeVizPrefab;
        public bool setupBaseLevel;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
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

            kuboGrid = new Node[gridSize* gridSize* gridSize];

            for (int i = 1, z = 0; z < gridSizeVector.z; z++)
            {
                for (int x = 0; x < gridSizeVector.x; x++)
                {
                    for (int y = 0; y < gridSizeVector.y; y++, i++)
                    {
                        Vector3 nodePosition = new Vector3(x * offset, y * offset, z * offset);

                        Node currentNode = new Node();
                        
                        currentNode.xCoord = x;
                        currentNode.yCoord = y;
                        currentNode.zCoord = z;

                        currentNode.nodeIndex = i;
                        currentNode.worldPosition = nodePosition;
                        currentNode.cubeLayers = CubeLayers.cubeEmpty;

                        kuboGrid[i - 1] = currentNode;

                        if(LevelEditor.instance != null && LevelEditor.isInEditor)
                        {
                            // if the index is at the bottom left corner of the cube, spawn a starter cube
                            if (i == 1 && startingPos == LevelEditorStart.bottomCorner) SpawnStarterCube(i);

                            // if the index is at the center of the cube, spawn a starter cube
                            if (i == centerPosition && startingPos == LevelEditorStart.centerOfKubo) SpawnStarterCube(i);

                            //if you want to load the level editor with a base level
                            if (setupBaseLevel)
                            {
                                if (x == 0 || y == 0 || z == 0)
                                {
                                    GameObject baseLevelCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                    baseLevelCube.AddComponent(typeof(CubeBase));
                                    baseLevelCube.transform.parent = gameObject.transform;

                                    CubeBase cubeObj = baseLevelCube.GetComponent<CubeBase>();
                                    cubeObj.transform.position = nodePosition;
                                    cubeObj.isStatic = true;

                                    currentNode.cubeType = CubeTypes.StaticCube;
                                    currentNode.cubeLayers = CubeLayers.cubeFull;

                                    currentNode.cubeOnPosition = baseLevelCube;

                                    cubeObj.myIndex = i;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void SpawnStarterCube(int index)
        {
            GameObject firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            firstCube.AddComponent(typeof(CubeBase));

            CubeBase cubeObj = firstCube.GetComponent<CubeBase>();

            cubeObj.transform.position = kuboGrid[index - 1].worldPosition;
            kuboGrid[index - 1].cubeOnPosition = firstCube;

            cubeObj.myIndex = index;
        }

        public void ResetGrid()
        {
            //destroy all cubes in grid and reset nodes to base state
            foreach (Node node in kuboGrid)
            {
                node.cubeLayers = CubeLayers.cubeEmpty;
                Destroy(node.cubeOnPosition);
            }
        }
    }
}