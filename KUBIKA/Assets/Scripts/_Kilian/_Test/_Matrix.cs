using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Test
{
    public class _Matrix : MonoBehaviour
    {
        public Transform[] nodeMatrix;
        public int matrixLength;

        // CUBE MOVABLE
        public _CubeTestSynchronized Cube1;
        public int indexC1;
        public _CubeTestSynchronized Cube2;
        public int indexC2;


        // MOVE LERP
        Vector3 currentPos;
        Vector3 basePos;
        float currentTime;
        public float moveTime;
        float time;

        // Start is called before the first frame update
        void Start()
        {
            Cube1.transform.position = nodeMatrix[indexC1 - 1].position;
            Cube2.transform.position = nodeMatrix[indexC2 - 1].position;

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (((indexC1 - matrixLength) + (matrixLength * matrixLength) - 1) / ((matrixLength * matrixLength) * (indexC1 / (matrixLength * matrixLength)) + (matrixLength * matrixLength)) != 0)
                {
                    StartCoroutine(Cube1.Move(nodeMatrix[indexC1 - matrixLength - 1].position));
                    indexC1 = indexC1 - matrixLength;
                }
                if (((indexC2 - matrixLength) + (matrixLength * matrixLength) - 1) / ((matrixLength * matrixLength) * (indexC2 / (matrixLength * matrixLength)) + (matrixLength * matrixLength)) != 0)
                {
                    StartCoroutine(Cube2.Move(nodeMatrix[indexC2 - matrixLength - 1].position));
                    indexC2 = indexC2 - matrixLength;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if ((indexC1 + matrixLength) / ((matrixLength * matrixLength) * (indexC1 / (matrixLength * matrixLength) + 1)) != 1)
                {
                    StartCoroutine(Cube1.Move(nodeMatrix[indexC1 + matrixLength - 1].position));
                    indexC1 = indexC1 + matrixLength;
                }
                if ((indexC2 + matrixLength) / ((matrixLength * matrixLength) * (indexC2 / (matrixLength * matrixLength) + 1)) != 1)
                {
                    StartCoroutine(Cube2.Move( nodeMatrix[indexC2 + matrixLength - 1].position));
                    indexC2 = indexC2 + matrixLength;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                if (indexC1 -(matrixLength * matrixLength) >= 0)
                {
                    StartCoroutine(Cube1.Move( nodeMatrix[indexC1 - (matrixLength * matrixLength) - 1].position));
                    indexC1 = indexC1 - (matrixLength * matrixLength);
                }
                if (indexC1 - (matrixLength * matrixLength) >= 0)
                {
                    StartCoroutine(Cube2.Move( nodeMatrix[indexC2 - (matrixLength * matrixLength) - 1].position));
                    indexC2 = indexC2 - (matrixLength * matrixLength);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if ((indexC1 + (matrixLength * matrixLength)) / ((matrixLength * matrixLength * matrixLength)) != 1)
                {
                    StartCoroutine(Cube1.Move(nodeMatrix[indexC1 + (matrixLength * matrixLength) - 1].position));
                    indexC1 = indexC1 + (matrixLength * matrixLength);
                }
                if ((indexC2 + (matrixLength * matrixLength)) / ((matrixLength * matrixLength * matrixLength)) != 1)
                {
                    StartCoroutine(Cube2.Move( nodeMatrix[indexC2 + (matrixLength * matrixLength) - 1].position));
                    indexC2 = indexC2 + (matrixLength * matrixLength);
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                if (indexC1 % matrixLength != 0)
                {
                    StartCoroutine(Cube1.Move(nodeMatrix[indexC1 +1 -1].position));
                    indexC1 = indexC1 + 1;
                }
                if (indexC2 % matrixLength != 0)
                {
                    StartCoroutine(Cube2.Move(nodeMatrix[indexC2 + 1 - 1].position));
                    indexC2 = indexC2 + 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                if ((indexC1 - 1) % matrixLength != 0)
                {
                    StartCoroutine(Cube1.Move( nodeMatrix[indexC1 -1 -1].position));
                    indexC1 = indexC1 - 1;
                }
                if ((indexC2 - 1) % matrixLength != 0)
                {
                    StartCoroutine(Cube2.Move( nodeMatrix[indexC2 - 1 - 1].position));
                    indexC2 = indexC2 - 1;
                }
            }
        }

    }

}
