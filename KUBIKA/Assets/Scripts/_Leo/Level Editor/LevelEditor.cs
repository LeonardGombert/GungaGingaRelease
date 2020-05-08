using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        RaycastHit hit;

        Vector3 selectedCubeNormal;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    //Grid.instance.kuboGrid[hit.collider.gameObject.name];
                    Debug.Log(hit.collider.gameObject.name);
                    Debug.Log(hit.normal);

                    selectedCubeNormal = hit.normal;

                    int tempValue;
                    int.TryParse(hit.collider.gameObject.name, out tempValue);

                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = Grid.instance.kuboGrid[tempValue - 1 + Grid.instance.gridSize].worldPosition;

                    Grid.instance.kuboGrid[tempValue - 1 + Grid.instance.gridSize].cubeOnPosition = cube;
                    cube.name = (tempValue + Grid.instance.gridSize).ToString();
                }
            }


            if (selectedCubeNormal == Vector3.up) Debug.Log("up"); //+ 1
            if (selectedCubeNormal == Vector3.down) Debug.Log("down"); //- 1
            if (selectedCubeNormal == Vector3.left) Debug.Log("left"); //- the grid size
            if (selectedCubeNormal == Vector3.right) Debug.Log("right"); //+ the grid size
            if (selectedCubeNormal == Vector3.forward) Debug.Log("forward"); //+ the grid size squared
            if (selectedCubeNormal == Vector3.back) Debug.Log("back"); //- the grid size squared
        }
    }
}
