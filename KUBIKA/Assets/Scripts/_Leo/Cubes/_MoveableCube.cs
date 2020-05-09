﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _MoveableCube : CubeBase
    {
        //FALLING 
        int nbrCubeMouvableBelow;
        int nbrCubeEmptyBelow;
        int nbrCubeBelow;
        int indexTargetNode;
        bool thereIsEmpty = false;
        public bool isChecking;
        public bool isFalling;
        Vector3 nextPosition;

        //FALL MOVE
        Vector3 currentPos;
        Vector3 basePos;
        float currentTime;
        public float moveTime = 1;
        float time;

        //LAYERS
        public CubeLayers cubeLayers;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            isStatic = false;

            _DataManager.instance.EndChecking.AddListener(FallMoveFunction);
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            //CheckIfFalling();//gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;

            if (Input.GetKeyDown(KeyCode.W))
            {
                // Put Actual Node as Moveable
                gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                cubeLayers = gridRef.kuboGrid[myIndex - 1].cubeLayers;
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                isChecking = true;
                StartCoroutine(_DataManager.instance.CheckIfCubeAreChecking());
                thereIsEmpty = false;
                nbrCubeMouvableBelow = 0;
                nbrCubeEmptyBelow = 0;
                Fall(1);
            }

            TEMPORARY______SHIT();
        }

        private void CheckIfFalling()
        {
            if (gridRef.kuboGrid[myIndex +_DirectionCustom.down -1].cubeLayers == CubeLayers.cubeEmpty)
            {
                //"garbage collection"
                //-1 because you're looking at the curretn NODE
                gridRef.kuboGrid[myIndex - 1].cubeOnPosition = null;
                gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

                //falling     

                //set the new index
                myIndex = gridRef.kuboGrid[myIndex +_DirectionCustom.down - 1].nodeIndex;
                
                //move to updated index
                transform.position = gridRef.kuboGrid[myIndex + _DirectionCustom.down].worldPosition;
                //set updated index to cubeMoveable
                gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
            }
        }

        public void Fall(int nbrCubeBelowParam)
        {
            //Debug.Log("Index 1==" + (myIndex + (_DirectionCustom.down * nbrCubeBelow) - 1));
            //Debug.Log("Index 2==" + (myIndex + (_DirectionCustom.down * nbrCubeBelow)));
            //Debug.Log("Index 3==" + (myIndex + (_DirectionCustom.down) - 1));
            //Debug.Log("Index 4==" + (myIndex + (_DirectionCustom.down)));

            if (gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeEmpty)
            {
                Debug.Log("EmptyDetected --" + (_DirectionCustom.down * nbrCubeBelowParam) + " || myIndex " + myIndex);
                thereIsEmpty = true;
                nbrCubeEmptyBelow += 1;
                Fall(nbrCubeBelowParam + 1); 
            }
            else if (gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeMoveable)
            {
                Debug.Log("MoveDetected --" + (_DirectionCustom.down * nbrCubeBelowParam) + " || myIndex " + myIndex);
                nbrCubeMouvableBelow += 1;
                Fall(nbrCubeBelowParam + 1);
            }
            else if (gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeFull)
            {
                Debug.Log("FULLDetected --" + (myIndex + (_DirectionCustom.down * nbrCubeBelowParam) - 1) + " || myIndex " + myIndex);
                nbrCubeBelow = nbrCubeBelowParam;
                nextPosition = gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelow) + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1))].worldPosition;
                isChecking = false;
            }
        }

        #region MOVE

        public void FallMoveFunction()
        {
            if ( thereIsEmpty == true && !isChecking)
                StartCoroutine(FallMove(nextPosition, nbrCubeEmptyBelow, nbrCubeBelow));
        }

        public IEnumerator FallMove(Vector3 fallPosition, int nbrCub, int nbrCubeBelowParam)
        {
            Debug.Log("MOVING");
            isFalling = true;
            gridRef.kuboGrid[myIndex - 1].cubeOnPosition = null;
            gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;


            basePos = transform.position;
            currentTime = 0;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime / nbrCub;
                currentTime = (currentTime / moveTime);

                currentPos = Vector3.Lerp(basePos, fallPosition, currentTime);

                transform.position = currentPos;
                yield return transform.position;
            }

            isFalling = false;

            myIndex = gridRef.kuboGrid[myIndex + (_DirectionCustom.down * nbrCubeBelowParam) - 1 + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1))].nodeIndex;
            gridRef.kuboGrid[myIndex + (_DirectionCustom.down * nbrCubeBelowParam) - 1 + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1))].cubeOnPosition = gameObject;
            //set updated index to cubeMoveable
            gridRef.kuboGrid[myIndex + (_DirectionCustom.down * nbrCubeBelowParam) - 1 + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1))].cubeLayers = CubeLayers.cubeMoveable;
        }

        public IEnumerator Move(Vector3 nextPosition)
        {
            basePos = transform.position;
            currentTime = 0;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime;
                currentTime = (currentTime / moveTime);

                currentPos = Vector3.Lerp(basePos, nextPosition, currentTime);

                transform.position = currentPos;
                yield return transform.position;
            }
        }

        #endregion 


        void TEMPORARY______SHIT()
        {
            // X Axis
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (((myIndex - gridRef.gridSize) + (gridRef.gridSize * gridRef.gridSize) - 1) / ((gridRef.gridSize * gridRef.gridSize) * (myIndex / (gridRef.gridSize * gridRef.gridSize)) + (gridRef.gridSize * gridRef.gridSize)) != 0)
                {
                    StartCoroutine(Move(gridRef.kuboGrid[myIndex + _DirectionCustom.left - 1].worldPosition));
                    myIndex = myIndex - gridRef.gridSize;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // -X Axis
                if ((myIndex + gridRef.gridSize) / ((gridRef.gridSize * gridRef.gridSize) * (myIndex / (gridRef.gridSize * gridRef.gridSize) + 1)) != 1)
                {
                    StartCoroutine(Move(gridRef.kuboGrid[myIndex + _DirectionCustom.right - 1].worldPosition));
                    myIndex = myIndex + gridRef.gridSize;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // -Z Axis
                if (myIndex - (gridRef.gridSize * gridRef.gridSize) >= 0)
                {
                    StartCoroutine(Move(gridRef.kuboGrid[myIndex + _DirectionCustom.backward - 1].worldPosition));
                    myIndex = myIndex - (gridRef.gridSize * gridRef.gridSize);
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // Z Axis
                if ((myIndex + (gridRef.gridSize * gridRef.gridSize)) / ((gridRef.gridSize * gridRef.gridSize * gridRef.gridSize)) != 1)
                {
                    StartCoroutine(Move(gridRef.kuboGrid[myIndex + _DirectionCustom.forward - 1].worldPosition));
                    myIndex = myIndex + (gridRef.gridSize * gridRef.gridSize);
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // Y Axis
                if (myIndex % gridRef.gridSize != 0)
                {
                    StartCoroutine(Move(gridRef.kuboGrid[myIndex + _DirectionCustom.up - 1].worldPosition));
                    myIndex = myIndex + 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                // -Y Axis
                if ((myIndex - 1) % gridRef.gridSize != 0)
                {
                    StartCoroutine(Move(gridRef.kuboGrid[myIndex + _DirectionCustom.down - 1].worldPosition));
                    myIndex = myIndex - 1;
                }
            }
        }
    }
}