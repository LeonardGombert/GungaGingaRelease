using Kubika.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        RaycastHit hit;
        int hitIndex;
        int moveWeight;
        Grid gridRef;

        private void Start()
        {
            gridRef = Grid.instance;

            GameObject firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            firstCube.AddComponent(typeof(CubeObject));

            CubeObject cubeObj = firstCube.GetComponent<CubeObject>();

            cubeObj.transform.position = gridRef.kuboGrid[0].worldPosition;
            gridRef.kuboGrid[0].cubeOnPosition = firstCube;

            cubeObj.myIndex = 1;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    hitIndex = hit.collider.gameObject.GetComponent<CubeObject>().myIndex;
                    PlaceCube(hit.normal);
                }
            }
        }

        private void PlaceCube(Vector3 cubeNormal)
        {
            if (cubeNormal == Vector3.up) moveWeight = 1; //+ 1
            if (cubeNormal == Vector3.down) moveWeight = -1; //- 1
            if (cubeNormal == Vector3.right) moveWeight = gridRef.gridSize; //+ the grid size
            if (cubeNormal == Vector3.left) moveWeight = -gridRef.gridSize; //- the grid size
            if (cubeNormal == Vector3.forward) moveWeight = gridRef.gridSize * gridRef.gridSize; //+ the grid size squared
            if (cubeNormal == Vector3.back) moveWeight = - (gridRef.gridSize * gridRef.gridSize); //- the grid size squared

            GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newCube.AddComponent(typeof(CubeObject));

            CubeObject cubeObj = newCube.GetComponent<CubeObject>();

            cubeObj.myIndex = gridRef.kuboGrid[hitIndex - 1 + moveWeight].nodeIndex;
            cubeObj.transform.position = gridRef.kuboGrid[hitIndex - 1 + moveWeight].worldPosition;

            gridRef.kuboGrid[hitIndex - 1 + moveWeight].cubeOnPosition = newCube;
        }
    }
}
