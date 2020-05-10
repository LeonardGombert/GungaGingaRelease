using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kubika.LevelEditor;

namespace Kubika.Game
{
    public class _CalculTripleMatrix : MonoBehaviour
    {
        public _DataMatrixScriptable indexBankScriptable;
        public Vector3 gridSizeVector;
        public int gridSize = 12;
        [Range(0.5f, 5)] public float offset;

        public Node[] grid0;
        public Node[] grid1;
        public Node[] grid2;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("CLIK");
                CreateGrid0();
                CreateGrid1();
                CreateGrid2();
            }

            if(Input.GetKeyDown(KeyCode.X))
            {

            }
        }

        private void CreateGrid0()
        {
            gridSizeVector = new Vector3Int(gridSize, gridSize, gridSize);

            grid0 = new Node[gridSize * gridSize * gridSize];

            ///// 1st Grid

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

                        grid0[i - 1] = currentNode;


                    }
                }
            }

        }

        private void CreateGrid1()
        {
            gridSizeVector = new Vector3Int(gridSize, gridSize, gridSize);

            grid1 = new Node[gridSize * gridSize * gridSize];

            /////// 2nd Grid

            for (int i = 1, x = 0; x < gridSizeVector.x; x++)
            {
                for (int y = 0; y < gridSizeVector.y; y++)
                {
                    for (int z = 0; z < gridSizeVector.z; z++, i++)
                    {
                        Vector3 nodePosition = new Vector3(x * offset, y * offset, z * offset);

                        Node currentNode = new Node();

                        currentNode.xCoord = x;
                        currentNode.yCoord = y;
                        currentNode.zCoord = z;

                        currentNode.nodeIndex = i;
                        currentNode.worldPosition = nodePosition;

                        grid1[i - 1] = currentNode;


                    }
                }
            }



        }

        private void CreateGrid2()
        {
            gridSizeVector = new Vector3Int(gridSize, gridSize, gridSize);

            grid2 = new Node[gridSize * gridSize * gridSize];


            /////// 3rd Grid


            for (int i = 1, y = 0; y < gridSizeVector.y; y++)
            {
                for (int z = 0; z < gridSizeVector.z; z++)
                {
                    for (int x = 0; x < gridSizeVector.x; x++, i++)
                    {
                        Vector3 nodePosition = new Vector3(x * offset, y * offset, z * offset);

                        Node currentNode = new Node();

                        currentNode.xCoord = x;
                        currentNode.yCoord = y;
                        currentNode.zCoord = z;

                        currentNode.nodeIndex = i;
                        currentNode.worldPosition = nodePosition;

                        grid2[i - 1] = currentNode;



                    }
                }
            }

        }
    }
}
