﻿using Kubika.Game;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kubika.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        private static LevelEditor _instance;
        public static LevelEditor instance { get { return _instance; } }

        RaycastHit hit;
        int hitIndex;
        int moveWeight;
        _Grid grid;

        [SerializeField] CubeTypes currentCube;
        List<RaycastHit> hits = new List<RaycastHit>();

        public static bool isInEditor;
        public static bool isDevScene;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;

            if (SceneManager.GetActiveScene().buildIndex == (int)ScenesIndex.LEVEL_EDITOR) isInEditor = true;
            if (SceneManager.GetActiveScene().name.Contains("Dev")) isDevScene = true;
        }

        private void Start()
        {
            Initialization();
        }

        private void Initialization()
        {
            grid = _Grid.instance;
        }

        private void Update()
        {
            PlacingCube();
            SelectCube();
        }

        #region PLACE AND REMOVE CUBES
        private void PlacingCube()
        {
            //Drag and Release placement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //add the cubes you hit to a list of RaycastHits
                if (Input.GetMouseButton(0))
                {
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) if (!hits.Contains(hit)) hits.Add(hit);
                }

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
                newCube.transform.parent = grid.transform;

                CubeType(newCube);
            }
        }
        private void DeleteCube(RaycastHit hit)
        {
            hitIndex = hit.collider.gameObject.GetComponent<CubeBase>().myIndex;

            moveWeight = 0;

            //if there is a cube
            if (!IndexIsEmpty())
            {
                Destroy(grid.kuboGrid[hitIndex - 1].cubeOnPosition);                
                grid.kuboGrid[hitIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
            }
        }
        #endregion

        #region CHANGE CUBE SELECTION
        private void SelectCube()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (currentCube == CubeTypes.StaticCube) currentCube = CubeTypes.ChaosBall;
                else currentCube--;
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (currentCube == CubeTypes.ChaosBall) currentCube = CubeTypes.StaticCube;
                else currentCube++;
            }
        }
        //use to place cubes in the editor
        private void CubeType(GameObject newCube)
        {
            switch (currentCube)
            {
                case CubeTypes.StaticCube:
                    newCube.AddComponent(typeof(CubeBase));

                    CubeBase staticCube = newCube.GetComponent<CubeBase>();
                    staticCube.myIndex = GetCubeIndex();
                    SetCubeType(staticCube.myIndex, CubeTypes.StaticCube);
                    staticCube.isStatic = true;
                    break;

                case CubeTypes.MoveableCube:
                    newCube.AddComponent(typeof(_MoveableCube));
                    _MoveableCube moveableCube = newCube.GetComponent<_MoveableCube>();

                    moveableCube.myIndex = GetCubeIndex();
                    SetCubeType(moveableCube.myIndex, CubeTypes.MoveableCube);
                    moveableCube.isStatic = false;
                    break;

                case CubeTypes.VictoryCube:
                    newCube.AddComponent(typeof(_VictoryCube));
                    _VictoryCube victoryCube = newCube.GetComponent<_VictoryCube>();

                    victoryCube.myIndex = GetCubeIndex();
                    SetCubeType(victoryCube.myIndex, CubeTypes.VictoryCube);

                    victoryCube.isStatic = false;
                    break;

                case CubeTypes.DeliveryCube:
                    newCube.AddComponent(typeof(_DeliveryCube));
                    _DeliveryCube deliveryCube = newCube.GetComponent<_DeliveryCube>();
                    deliveryCube.myIndex = GetCubeIndex();
                    SetCubeType(deliveryCube.myIndex, CubeTypes.DeliveryCube);
                    deliveryCube.isStatic = true;
                    break;

                case CubeTypes.ElevatorCube:
                    newCube.AddComponent(typeof(_ElevatorCube));
                    _ElevatorCube elevatorCube = newCube.GetComponent<_ElevatorCube>();

                    elevatorCube.myIndex = GetCubeIndex();
                    SetCubeType(elevatorCube.myIndex, CubeTypes.ElevatorCube);
                    elevatorCube.isStatic = true;
                    break;

                case CubeTypes.ConcreteCube:
                    newCube.AddComponent(typeof(_ConcreteCube));
                    _ConcreteCube concreteCube = newCube.GetComponent<_ConcreteCube>();

                    concreteCube.myIndex = GetCubeIndex();
                    SetCubeType(concreteCube.myIndex, CubeTypes.ConcreteCube);
                    concreteCube.isStatic = true;
                    break;

                case CubeTypes.MineCube:
                    newCube.AddComponent(typeof(_MineCube));
                    _MineCube mineCube = newCube.GetComponent<_MineCube>();

                    mineCube.myIndex = GetCubeIndex();
                    SetCubeType(mineCube.myIndex, CubeTypes.MineCube);
                    mineCube.isStatic = true;
                    break;

                case CubeTypes.TimerCube:
                    newCube.AddComponent(typeof(_TimerCube));
                    _TimerCube timerCube = newCube.GetComponent<_TimerCube>();

                    timerCube.myIndex = GetCubeIndex();
                    SetCubeType(timerCube.myIndex, CubeTypes.TimerCube);
                    timerCube.isStatic = true;
                    break;

                case CubeTypes.SwitchCube:
                    newCube.AddComponent(typeof(_SwitchCube));
                    _SwitchCube switchCube = newCube.GetComponent<_SwitchCube>();

                    switchCube.myIndex = GetCubeIndex();
                    SetCubeType(switchCube.myIndex, CubeTypes.SwitchCube);
                    switchCube.isStatic = true;
                    break;

                case CubeTypes.MirrorCube:
                    newCube.AddComponent(typeof(_MirrorCube));
                    _MirrorCube mirrorCube = newCube.GetComponent<_MirrorCube>();

                    mirrorCube.myIndex = GetCubeIndex();
                    SetCubeType(mirrorCube.myIndex, CubeTypes.MirrorCube);
                    mirrorCube.isStatic = true;
                    break;

                case CubeTypes.ChaosBall:
                    newCube.AddComponent(typeof(_ChaosBall));
                    _ChaosBall chaosBall = newCube.GetComponent<_ChaosBall>();

                    chaosBall.myIndex = GetCubeIndex();
                    SetCubeType(chaosBall.myIndex, CubeTypes.ChaosBall);
                    chaosBall.isStatic = true;
                    break;

                default:
                    break;
            }
        }
        #endregion

        #region GET OFFSET AND CHECK INDEX STATE
        void CubeOffset(Vector3 cubeNormal)
        {
            if (cubeNormal == Vector3.up) moveWeight = 1; //+ 1
            if (cubeNormal == Vector3.down) moveWeight = -1; //- 1
            if (cubeNormal == Vector3.right) moveWeight = grid.gridSize; //+ the grid size
            if (cubeNormal == Vector3.left) moveWeight = -grid.gridSize; //- the grid size
            if (cubeNormal == Vector3.forward) moveWeight = grid.gridSize * grid.gridSize; //+ the grid size squared
            if (cubeNormal == Vector3.back) moveWeight = -(grid.gridSize * grid.gridSize); //- the grid size squared
        }

        void SetCubeType(int cubeIndex, CubeTypes cubeType)
        {
            grid.kuboGrid[cubeIndex - 1].cubeType = cubeType;
        }

        bool IndexIsEmpty()
        {
            if (grid.kuboGrid[hitIndex - 1 + moveWeight].cubeOnPosition == null) return true;
            else return false;
        }
        #endregion

        #region SET NEW VALUES FOR CUBE
        //get the position of the cube you are placing and set the cubeOnPosition
        Vector3 GetCubePosition(GameObject newCube)
        {
            //set the cubeOnPosition of the target node
            grid.kuboGrid[hitIndex - 1 + moveWeight].cubeOnPosition = newCube;

            return grid.kuboGrid[hitIndex - 1 + moveWeight].worldPosition;
        }

        int GetCubeIndex()
        {
            return grid.kuboGrid[hitIndex - 1 + moveWeight].nodeIndex;
        }
        #endregion
    }
}
