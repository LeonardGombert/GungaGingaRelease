using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    public class Grid : MonoBehaviour
    {
        public int gridSize; //the square root of the Matrix
        Vector3Int gridSizeVector;

        public Node[] grid; //3D jagged array of nodes

        [Range(0.5f, 5)] [SerializeField] float offset;
        public GameObject yes;
        // Start is called before the first frame update
        private void Start()
        {
            CreateGrid();
        }

        private void CreateGrid()
        {
            gridSizeVector = new Vector3Int(gridSize, gridSize, gridSize);

            //grid = new Node[gridSize* gridSize* gridSize];
            grid = new Node[100];

            for (int i = 1, z = 0; z < gridSizeVector.z; z++)
            {
                for (int x = 0; x < gridSizeVector.x; x++)
                {
                    for (int y = 0; y < gridSizeVector.y; y++, i++)
                    {
                        Vector3 nodePosition = new Vector3(x * offset, y * offset, z * offset);

                        /*
                        GameObject gridNode = Instantiate(yes, nodePosition, Quaternion.identity, transform);
                        gridNode.AddComponent(typeof(NodeInterface));
                        gridNode.name = i.ToString();
                        */

                        Node currentNode = new Node();
                        currentNode.nodeIndex = i;
                        currentNode.xCoord = x;
                        currentNode.yCoord = y;
                        currentNode.zCoord = z;

                        currentNode.worldPosition = nodePosition;

                        grid[i-1] = currentNode;
                    }
                }
            }
        }
    }
}