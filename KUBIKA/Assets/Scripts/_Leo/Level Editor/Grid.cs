using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    public class Grid : MonoBehaviour
    {
        private static Grid _instance;
        public static Grid instance { get { return _instance; } }

        public int gridSize; //the square root of the Matrix
        [Range(0.5f, 5)] [SerializeField] float offset;
        Vector3Int gridSizeVector;

        public Node[] kuboGrid; //3D jagged array of nodes
        List<GameObject> nodeVizList = new List<GameObject>(); //list of node visualisations

        public bool visualizeNodes;
        public GameObject nodeVizPrefab;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        private void Start()
        {
            CreateGrid();
        }

        private void Update()
        {
            /*if (visualizeNodes) foreach (GameObject item in nodeVizList) item.SetActive(true);
            else if (!visualizeNodes) foreach (GameObject item in nodeVizList) item.SetActive(false);*/
        }

        private void CreateGrid()
        {
            gridSizeVector = new Vector3Int(gridSize, gridSize, gridSize);

            kuboGrid = new Node[gridSize* gridSize* gridSize];

            for (int i = 1, z = 0; z < gridSizeVector.z; z++)
            {
                for (int x = 0; x < gridSizeVector.x; x++)
                {
                    for (int y = 0; y < gridSizeVector.y; y++, i++)
                    {
                        Vector3 nodePosition = new Vector3(x * offset, y * offset, z * offset);

                        //instantiate gameObjects, but only for visualization
                        GameObject gridNode = Instantiate(nodeVizPrefab, nodePosition, Quaternion.identity, transform);
                        gridNode.AddComponent(typeof(NodeInterface));
                        gridNode.name = i.ToString();

                        nodeVizList.Add(gridNode);

                        Node currentNode = new Node();
                        
                        currentNode.xCoord = x;
                        currentNode.yCoord = y;
                        currentNode.zCoord = z;

                        currentNode.nodeIndex = i;
                        currentNode.worldPosition = nodePosition;

                        currentNode.cubeOnPosition = gridNode;

                        if (x == 0 || y == 0 || z == 0) gridNode.SetActive(true);
                        else gridNode.SetActive(false);

                        kuboGrid[i-1] = currentNode;
                    }
                }
            }
        }
    }
}