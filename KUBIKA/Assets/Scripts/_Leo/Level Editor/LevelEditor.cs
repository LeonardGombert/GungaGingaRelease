using Kubika.Game;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        RaycastHit hit;
        [SerializeField] List<RaycastHit> hits = new List<RaycastHit>();
        int hitIndex;
        int moveWeight;
        Grid gridRef;

        private void Start()
        {
            gridRef = Grid.instance;

            if (!gridRef.setupBaseLevel)
            {
                GameObject firstCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                firstCube.AddComponent(typeof(CubeBase));

                CubeBase cubeObj = firstCube.GetComponent<CubeBase>();

                cubeObj.transform.position = gridRef.kuboGrid[0].worldPosition;
                gridRef.kuboGrid[0].cubeOnPosition = firstCube;

                cubeObj.myIndex = 1;
            }
        }

        void Update()
        {
            CheckIfPlacing();
        }

        private void CheckIfPlacing()
        {
            //Drag and Release placement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //add the cubes you hit to a list of RaycastHits
                if (Input.GetMouseButton(0)) if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) if (!hits.Contains(hit)) hits.Add(hit);

                //when the user releases the mouse, place all the cubes at once
                if (Input.GetMouseButtonUp(0))
                {
                    foreach (RaycastHit hit in hits) PlaceCube(hit);
                    hits.Clear();
                }

                //add the cubes you hit to a list of RaycastHits
                if (Input.GetMouseButton(1))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) 
                        if (!hits.Contains(hit)) hits.Add(hit);
                    
                    foreach (RaycastHit hit in hits) DeleteCube(hit);
                    hits.Clear();
                }
            }

            //single click and place
            else if (Input.GetMouseButtonDown(0)) if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) PlaceCube(hit);

            if (Input.GetMouseButtonDown(1)) if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) DeleteCube(hit);
        }

        private void PlaceCube(RaycastHit hit)
        {
            //get the index of the cube you just hit
            hitIndex = hit.collider.gameObject.GetComponent<CubeBase>().myIndex;

            //calculate where you're placing the new cube
            CubeOffset(hit.normal);

            //if the current node doesn't have a cube on it, place a new cube
            if (IndexIsEmpty())
            {
                //create a new Cube and add the CubeObject component to store its index
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                newCube.transform.position = GetCubePosition(newCube);
                newCube.transform.parent = gridRef.transform;

                newCube.AddComponent(typeof(CubeBase));

                CubeBase cubeObj = newCube.GetComponent<CubeBase>();
                cubeObj.myIndex = GetCubeIndex();

                cubeObj.isStatic = true;
            }
        }

        private void DeleteCube(RaycastHit hit)
        {
            hitIndex = hit.collider.gameObject.GetComponent<CubeBase>().myIndex;

            moveWeight = 0;

            //if there is a cube
            if (!IndexIsEmpty())
            {
                Destroy(gridRef.kuboGrid[hitIndex - 1].cubeOnPosition);
                //gridRef.kuboGrid[hitIndex - 1].cubeOnPosition = null; //maybe be unecessary if cube is getting destroyed anyway(?)
                gridRef.kuboGrid[hitIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
            }
        }

        #region GET OFFSET AND CHECK INDEX STATE
        void CubeOffset(Vector3 cubeNormal)
        {
            if (cubeNormal == Vector3.up) moveWeight = 1; //+ 1
            if (cubeNormal == Vector3.down) moveWeight = -1; //- 1
            if (cubeNormal == Vector3.right) moveWeight = gridRef.gridSize; //+ the grid size
            if (cubeNormal == Vector3.left) moveWeight = -gridRef.gridSize; //- the grid size
            if (cubeNormal == Vector3.forward) moveWeight = gridRef.gridSize * gridRef.gridSize; //+ the grid size squared
            if (cubeNormal == Vector3.back) moveWeight = -(gridRef.gridSize * gridRef.gridSize); //- the grid size squared
        }

        bool IndexIsEmpty()
        {
            if (gridRef.kuboGrid[hitIndex - 1 + moveWeight].cubeOnPosition == null) return true;
            else return false;
        }
        #endregion

        #region SET NEW VALUES FOR CUBE
        //get the position of the cube you are placing and set the cubeOnPosition
        Vector3 GetCubePosition(GameObject newCube)
        {
            //set the cubeOnPosition of the target node
            gridRef.kuboGrid[hitIndex - 1 + moveWeight].cubeOnPosition = newCube;

            return gridRef.kuboGrid[hitIndex - 1 + moveWeight].worldPosition;
        }

        int GetCubeIndex()
        {
            return gridRef.kuboGrid[hitIndex - 1 + moveWeight].nodeIndex;
        }
        #endregion
    }
}
