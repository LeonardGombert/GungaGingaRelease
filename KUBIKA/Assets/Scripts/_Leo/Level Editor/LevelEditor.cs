using Kubika.Game;
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
            firstCube.AddComponent(typeof(CubeBase));

            CubeBase cubeObj = firstCube.GetComponent<CubeBase>();

            cubeObj.transform.position = gridRef.kuboGrid[0].worldPosition;
            gridRef.kuboGrid[0].cubeOnPosition = firstCube;

            cubeObj.myIndex = 1;
        }

        void Update()
        {
            PlaceCube();
        }

        private void PlaceCube()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    hitIndex = hit.collider.gameObject.GetComponent<CubeBase>().myIndex;
                    //create a new Cube and add the CubeObject component to store its index
                    GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

                    newCube.transform.position = GetCubePosition(hit.normal, newCube);
                    newCube.transform.parent = gridRef.transform;

                    newCube.AddComponent(typeof(CubeBase));

                    CubeBase cubeObj = newCube.GetComponent<CubeBase>();
                    cubeObj.myIndex = GetCubeIndex();
                }
            }
        }

        //get the position of the cube you are placing and set it as the cubeOnPosition
        private Vector3 GetCubePosition(Vector3 cubeNormal, GameObject newCube)
        {
            if (cubeNormal == Vector3.up) moveWeight = 1; //+ 1
            if (cubeNormal == Vector3.down) moveWeight = -1; //- 1
            if (cubeNormal == Vector3.right) moveWeight = gridRef.gridSize; //+ the grid size
            if (cubeNormal == Vector3.left) moveWeight = -gridRef.gridSize; //- the grid size
            if (cubeNormal == Vector3.forward) moveWeight = gridRef.gridSize * gridRef.gridSize; //+ the grid size squared
            if (cubeNormal == Vector3.back) moveWeight = -(gridRef.gridSize * gridRef.gridSize); //- the grid size squared

            gridRef.kuboGrid[hitIndex - 1 + moveWeight].cubeOnPosition = newCube;

            return gridRef.kuboGrid[hitIndex - 1 + moveWeight].worldPosition;
        }

        private int GetCubeIndex()
        {
            return gridRef.kuboGrid[hitIndex - 1 + moveWeight].nodeIndex;
        }
    }
}
