using Kubika.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.CustomLevelEditor
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
        public List<GameObject> placedObjects = new List<GameObject>();

        public GameObject nodeVizPrefab;
        
        public LevelSetup levelSetup;

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

                        if(LevelEditor.isDevScene || LevelEditor.isLevelEditor)
                        {
                            switch (levelSetup)
                            {
                                case LevelSetup.none:
                                    // if the index is at the bottom left corner of the cube, spawn a starter cube
                                    if (index == 1 && startingPos == LevelEditorStart.bottomCorner)
                                        SpawnBaseGrid(index, nodePosition, currentNode);

                                    // if the index is at the center of the cube, spawn a starter cube
                                    if (index == centerPosition && startingPos == LevelEditorStart.centerOfKubo)
                                        SpawnBaseGrid(index, nodePosition, currentNode);
                                    break;

                                case LevelSetup.baseGrid:
                                    if (x == 0 || y == 0 || z == 0) SpawnBaseGrid(index, nodePosition, currentNode);
                                    break;

                                case LevelSetup.plane:
                                    if (y == 0) SpawnBaseGrid(index, nodePosition, currentNode);
                                    break;

                                case LevelSetup.rightDoublePlane:
                                    if (x == 0 || y == 0) SpawnBaseGrid(index, nodePosition, currentNode);
                                    break;

                                case LevelSetup.leftDoublePlane:
                                    if (z == 0 || y == 0) SpawnBaseGrid(index, nodePosition, currentNode);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void SpawnBaseGrid(int index, Vector3 position = default, Node node = default)
        {
            GameObject levelCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            levelCube.AddComponent(typeof(_StaticCube));
            levelCube.transform.parent = gameObject.transform;

            _StaticCube cubeObj = levelCube.GetComponent<_StaticCube>();
            cubeObj.transform.position = position;
            node.cubeOnPosition = levelCube;

            cubeObj.isStatic = true;
            node.cubeLayers = CubeLayers.cubeFull;
            node.cubeType = CubeTypes.StaticCube;

            cubeObj.myIndex = index;

            placedObjects.Add(levelCube);
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

        public void RefreshGrid()
        {
            ResetGrid();
            CreateGrid();
        }

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