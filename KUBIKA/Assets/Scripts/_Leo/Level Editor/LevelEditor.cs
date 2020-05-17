﻿using Kubika.Game;
using Kubika.Saving;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Kubika.CustomLevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        private static LevelEditor _instance;
        public static LevelEditor instance { get { return _instance; } }

        RaycastHit hit;
        int hitIndex;
        int moveWeight;
        _Grid grid;

        public CubeTypes currentCube;
        public StaticEnums staticViz;

        List<RaycastHit> placeHits = new List<RaycastHit>();
        List<RaycastHit> deleteHits = new List<RaycastHit>();


        //PLACING CUBES
        private bool placeMultiple = true;
        Vector3 userInputPosition;

        public LevelSetup levelSetup;
        public List<TextAsset> prefabLevels = new List<TextAsset>();

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;

            currentCube = CubeTypes.StaticCube;
        }

        private void Start()
        {
            grid = _Grid.instance;
        }

        private void Update()
        {
            //make sure the user isn't interacting with a UI element
            if (!EventSystem.current.IsPointerOverGameObject()
                || !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                if (ScenesManager.isDevScene || ScenesManager.isLevelEditor)
                {
                    PlaceAndDelete();
                    CubeSelection();
                }
            }
            else return;
        }

        #region PLACE AND REMOVE CUBES
        private void PlaceAndDelete()
        {
            /*
            //add the cubes you hit to a list of RaycastHits
            if (Input.GetMouseButton(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                CheckUserPlatform();

                if (Physics.Raycast(Camera.main.ScreenPointToRay(userInputPosition), out hit))
                    if (!placeHits.Contains(hit)) placeHits.Add(hit);
            }

            //when the user releases the mouse, place all the cubes at once
            if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                foreach (RaycastHit hit in placeHits) PlaceCube(hit);
                placeHits.Clear();
            }

            //add the cubes you hit to a list of RaycastHits
            if (Input.GetMouseButton(1))
            {
                CheckUserPlatform();

                if (Physics.Raycast(Camera.main.ScreenPointToRay(userInputPosition), out hit))
                    if (!deleteHits.Contains(hit)) deleteHits.Add(hit);
            }

            //when the user releases the mouse, delete alll cubes at once
            if (Input.GetMouseButtonUp(1) || Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Ended)
            {
                foreach (RaycastHit hit in deleteHits) DeleteCube(hit);
                deleteHits.Clear();
            }*/

            //single click and place
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                CheckUserPlatform();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(userInputPosition), out hit)) PlaceCube(hit);
            }

            //single click and delete
            if (Input.GetMouseButtonDown(1) || Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Began)
            {
                CheckUserPlatform();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(userInputPosition), out hit)) DeleteCube(hit);
            }
        }

        private void PlaceCube(RaycastHit hit)
        {
            //get the index of the cube you just hit
            hitIndex = hit.collider.gameObject.GetComponent<CubeBase>().myIndex;

            //calculate where you're placing the new cube
            CubeOffset(hit.normal);

            //if the current node doesn't have a cube on it, place a new cube
            if (currentCube != CubeTypes.None && IndexIsEmpty())
            {
                //create a new Cube and add the CubeObject component to store its index
                GameObject newCube = Instantiate(SaveAndLoad.instance.cubePrefab);

                newCube.transform.position = GetCubePosition(newCube);
                newCube.transform.parent = grid.transform;

                CubeType(newCube);
                grid.placedObjects.Add(newCube);
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

            grid.placedObjects.Remove(hit.collider.gameObject);

            if (!grid.placedObjects.Any()) grid.RefreshGrid();
        }
        #endregion

        #region // GENERATE BASE GRID
        public void GenerateBaseGrid()
        {
            switch (levelSetup)
            {
                case LevelSetup.none:
                    string emptyGridJson = prefabLevels.Find(item => item.name.Contains("Empty")).ToString();
                    LevelEditorData emptyGrid = JsonUtility.FromJson<LevelEditorData>(emptyGridJson);
                    SaveAndLoad.instance.ExtractAndRebuildLevel(emptyGrid);
                    break;

                case LevelSetup.baseGrid:
                    string baseGridJson = prefabLevels.Find(item => item.name.Contains("Base")).ToString();
                    LevelEditorData baseGrid = JsonUtility.FromJson<LevelEditorData>(baseGridJson);
                    SaveAndLoad.instance.ExtractAndRebuildLevel(baseGrid);
                    break;

                case LevelSetup.plane:
                    string planeJson = prefabLevels.Find(item => item.name.Contains("Plane")).ToString();
                    LevelEditorData planeData = JsonUtility.FromJson<LevelEditorData>(planeJson);
                    SaveAndLoad.instance.ExtractAndRebuildLevel(planeData);
                    break;

                case LevelSetup.rightDoublePlane:
                    string rightJson = prefabLevels.Find(item => item.name.Contains("Right")).ToString();
                    LevelEditorData rightData = JsonUtility.FromJson<LevelEditorData>(rightJson);
                    SaveAndLoad.instance.ExtractAndRebuildLevel(rightData);
                    break;

                case LevelSetup.leftDoublePlane:
                    string leftJson = prefabLevels.Find(item => item.name.Contains("Left")).ToString();
                    LevelEditorData leftData = JsonUtility.FromJson<LevelEditorData>(leftJson);
                    SaveAndLoad.instance.ExtractAndRebuildLevel(leftData);
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region CHANGE CUBE SELECTION
        private void CubeSelection()
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

        // Set all of the cube's information when it is placed
        private void CubeType(GameObject newCube)
        {
            switch (currentCube)
            {
                case CubeTypes.StaticCube:
                    newCube.AddComponent(typeof(_StaticCube));

                    _StaticCube staticCube = newCube.GetComponent<_StaticCube>();
                    staticCube.myIndex = GetCubeIndex();
                    SetCubeType(staticCube.myIndex, CubeTypes.StaticCube);
                    staticCube.isStatic = true;

                    staticCube.staticEnum = StaticEnums.Empty;
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
                    newCube.AddComponent(typeof(_RotateLocker));
                    _RotateLocker mirrorCube = newCube.GetComponent<_RotateLocker>();

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
            if (cubeNormal == Vector3.up) moveWeight = _DirectionCustom.up;  //+ 1
            if (cubeNormal == Vector3.down) moveWeight = _DirectionCustom.down;  //- 1
            if (cubeNormal == Vector3.right) moveWeight = _DirectionCustom.right; //+ the grid size
            if (cubeNormal == Vector3.left) moveWeight = _DirectionCustom.left; //- the grid size
            if (cubeNormal == Vector3.forward) moveWeight = _DirectionCustom.forward; //+ the grid size squared
            if (cubeNormal == Vector3.back) moveWeight = _DirectionCustom.backward; //- the grid size squared
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

        Vector3 CheckUserPlatform()
        {
            if (Input.GetMouseButton(0)) userInputPosition = Input.mousePosition;
            else if (Input.GetMouseButton(1)) userInputPosition = Input.mousePosition;
            if (Input.touchCount == 1) userInputPosition = Input.GetTouch(0).position;
            else if (Input.touchCount == 2) userInputPosition = Input.GetTouch(1).position;
            else userInputPosition = Vector3.zero;

            return userInputPosition;
        }

        #endregion
    }
}
