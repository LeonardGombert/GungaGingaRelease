﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class CubeMove : CubeScanner
    {
        //FALLING 
        int nbrCubeMouvableBelow;
        int nbrCubeEmptyBelow;
        int nbrCubeBelow;
        int indexTargetNode;
        bool thereIsEmpty = false;
        Vector3 nextPosition;

        // BOOL ACTION
        public bool isChecking;
        public bool isFalling;
        public bool isMoving;
        public bool isOutside;

        //FALL MOVE
        Vector3 currentPos;
        Vector3 basePos;
        float currentTime;
        public float moveTime = 0.5f;
        float time;

        //FALL OUTSIDE
        [Space]
        [Header("OUTSIDE")]
        public int nbrDeCubeFallOutside = 10;
        Vector3 moveOutsideTarget;
        Vector3 fallOutsideTarget;

        // COORD SYSTEM
        [Space]
        [Header("COORD SYSTEM")]
        public int xCoordLocal;
        public int yCoordLocal;
        public int zCoordLocal;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            _DataManager.instance.EndChecking.AddListener(FallMoveFunction);
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            //CheckIfFalling();//grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;

            if (Input.GetKeyDown(KeyCode.W))
            {
                // Put Actual Node as Moveable
                myCubeLayer = CubeLayers.cubeMoveable;
                grid.kuboGrid[myIndex - 1].cubeLayers = myCubeLayer;
            }

            TEMPORARY______SHIT();
        }

        public void CheckIfFalling()
        {
            /*
            if (grid.kuboGrid[myIndex +_DirectionCustom.down -1].cubeLayers == CubeLayers.cubeEmpty)
            {
                //"garbage collection"
                //-1 because you're looking at the curretn NODE
                grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
                grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

                //falling     

                //set the new index
                myIndex = grid.kuboGrid[myIndex +_DirectionCustom.down - 1].nodeIndex;
                
                //move to updated index
                transform.position = grid.kuboGrid[myIndex + _DirectionCustom.down].worldPosition;
                //set updated index to cubeMoveable
                grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
            }*/

            isChecking = true;
            thereIsEmpty = false;
            nbrCubeMouvableBelow = 0;
            nbrCubeEmptyBelow = 0;
            indexTargetNode = 0;

            Fall(1);
        }

        public void Fall(int nbrCubeBelowParam)
        {
            //Debug.Log("Index 1==" + (myIndex + (_DirectionCustom.down * nbrCubeBelow) - 1));
            //Debug.Log("Index 2==" + (myIndex + (_DirectionCustom.down * nbrCubeBelow)));
            //Debug.Log("Index 3==" + (myIndex + (_DirectionCustom.down) - 1));
            //Debug.Log("Index 4==" + (myIndex + (_DirectionCustom.down)));

            if (grid.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeEmpty)
            {
                Debug.Log("EmptyDetected --" + (myIndex - 1 + _DirectionCustom.down * nbrCubeBelowParam) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                thereIsEmpty = true;
                nbrCubeEmptyBelow += 1;
                Fall(nbrCubeBelowParam + 1);
            }
            else if (grid.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeMoveable)
            {
                Debug.Log("MoveDetected --" + (myIndex - 1 + _DirectionCustom.down * nbrCubeBelowParam) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                nbrCubeMouvableBelow += 1;
                Fall(nbrCubeBelowParam + 1);
            }
            else if (grid.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeFull)
            {
                Debug.Log("FULLDetected --" + (myIndex + (_DirectionCustom.down * nbrCubeBelowParam) - 1) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                nbrCubeBelow = nbrCubeBelowParam;

                indexTargetNode = myIndex + (_DirectionCustom.down * nbrCubeBelow) + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1));
                nextPosition = grid.kuboGrid[indexTargetNode - 1].worldPosition;

                Debug.Log("FULLDetected -- Final Destination " + (myIndex - 1 + (_DirectionCustom.down * nbrCubeBelow) + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1))) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                isChecking = false;
            }
        }

        #region MOVE

        public void FallMoveFunction()
        {
            Debug.Log("FallMoveFunction");
            if (thereIsEmpty == true && isOutside == false)
                StartCoroutine(FallMove(nextPosition, nbrCubeEmptyBelow, nbrCubeBelow));
            else if (isOutside == true)
                StartCoroutine(FallFromMap(fallOutsideTarget, nbrDeCubeFallOutside));
        }

        public IEnumerator FallMove(Vector3 fallPosition, int nbrCub, int nbrCubeBelowParam)
        {
            isFalling = true;
            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
            grid.kuboGrid[myIndex - 1].cubeType = CubeTypes.None;

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


            myIndex = indexTargetNode;
            grid.kuboGrid[indexTargetNode - 1].cubeOnPosition = gameObject;
            //set updated index to cubeMoveable
            grid.kuboGrid[indexTargetNode - 1].cubeLayers = CubeLayers.cubeMoveable;

            xCoordLocal = grid.kuboGrid[indexTargetNode - 1].xCoord;
            yCoordLocal = grid.kuboGrid[indexTargetNode - 1].yCoord;
            zCoordLocal = grid.kuboGrid[indexTargetNode - 1].zCoord;


            isFalling = false;
        }

        public IEnumerator Move(Vector3 nextPosition)
        {
            isMoving = true;

            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

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

            myIndex = indexTargetNode;
            grid.kuboGrid[indexTargetNode - 1].cubeOnPosition = gameObject;
            //set updated index to cubeMoveable
            grid.kuboGrid[indexTargetNode - 1].cubeLayers = CubeLayers.cubeMoveable;

            xCoordLocal = grid.kuboGrid[indexTargetNode - 1].xCoord;
            yCoordLocal = grid.kuboGrid[indexTargetNode - 1].yCoord;
            zCoordLocal = grid.kuboGrid[indexTargetNode - 1].zCoord;

            isMoving = false;

        }

        public IEnumerator MoveFromMap(Vector3 nextPosition)
        {
            isMoving = true;
            isOutside = true;

            moveOutsideTarget = new Vector3(nextPosition.x * grid.offset, nextPosition.y * grid.offset, nextPosition.z * grid.offset);

            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

            basePos = transform.position;
            currentTime = 0;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime;
                currentTime = (currentTime / moveTime);

                currentPos = Vector3.Lerp(basePos, moveOutsideTarget, currentTime);

                transform.position = currentPos;
                yield return transform.position;
            }

            myIndex = indexTargetNode;

            xCoordLocal = Mathf.RoundToInt(moveOutsideTarget.x / grid.offset);
            yCoordLocal = Mathf.RoundToInt(moveOutsideTarget.y / grid.offset);
            zCoordLocal = Mathf.RoundToInt(moveOutsideTarget.z / grid.offset);

            isMoving = false;


            fallOutsideTarget = moveOutsideTarget;
            fallOutsideTarget += (_DirectionCustom.vectorDown * nbrDeCubeFallOutside);

            _DataManager.instance.EndChecking.Invoke();

        }

        public IEnumerator FallFromMap(Vector3 fallFromMapPosition, int nbrCaseBelow)
        {
            isFalling = true;


            basePos = transform.position;
            currentTime = 0;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime / nbrCaseBelow;
                currentTime = (currentTime / moveTime);

                currentPos = Vector3.Lerp(basePos, fallFromMapPosition, currentTime);

                transform.position = currentPos;
                yield return transform.position;
            }


            xCoordLocal = Mathf.RoundToInt(fallFromMapPosition.x / grid.offset);
            yCoordLocal = Mathf.RoundToInt(fallFromMapPosition.y / grid.offset);
            zCoordLocal = Mathf.RoundToInt(fallFromMapPosition.z / grid.offset);


            isFalling = false;
        }

        #endregion

        #region INPUT
        /*
        public void NextDirection()
        {
            //check if this gameObject is a mirror cube
            if (gameObject.GetComponent<_MirrorCube>() != null) mirrorMove = true;

            if (!isStatic)
            {
                // Calcul the swip angle
                currentSwipePos = DataManager.instance.inputPosition;

                distanceTouch = Vector3.Distance(baseSwipePos, currentSwipePos);

                angleDirection = Mathf.Abs(Mathf.Atan2(currentSwipePos.y - baseSwipePos.y, baseSwipePos.x - currentSwipePos.x) * 180 / Mathf.PI - 180);


                KUBNord = Camera_ZoomScroll.KUBNordScreenAngle;
                KUBWest = Camera_ZoomScroll.KUBWestScreenAngle;
                KUBSud = Camera_ZoomScroll.KUBSudScreenAngle;
                KUBEst = Camera_ZoomScroll.KUBEstScreenAngle;

                // Check in which direction the player swiped 

                if (angleDirection < KUBNord && angleDirection > KUBEst)
                {
                    enumSwipe = swipeDirection.Front;
                }
                else if (angleDirection < KUBWest && angleDirection > KUBNord)
                {
                    enumSwipe = swipeDirection.Left;
                }
                else if (angleDirection < KUBSud && angleDirection > KUBWest)
                {
                    enumSwipe = swipeDirection.Back;
                }
                else if (angleDirection < KUBEst && angleDirection > KUBSud)
                {
                    enumSwipe = swipeDirection.Right;
                }

                else
                {

                    if (angleDirection > 180)
                        inverseAngleDirection = angleDirection - 180;
                    else
                        inverseAngleDirection = angleDirection + 180;


                    if (inverseAngleDirection < KUBNord && inverseAngleDirection > KUBEst)
                    {
                        enumSwipe = swipeDirection.Back;
                    }
                    else if (inverseAngleDirection < KUBWest && inverseAngleDirection > KUBNord)
                    {
                        enumSwipe = swipeDirection.Right;
                    }
                    else if (inverseAngleDirection < KUBSud && inverseAngleDirection > KUBWest)
                    {
                        enumSwipe = swipeDirection.Front;
                    }
                    else if (inverseAngleDirection < KUBEst && inverseAngleDirection > KUBSud)
                    {
                        enumSwipe = swipeDirection.Left;
                    }
                }

                if (distanceTouch > DataManager.instance.swipeMinimalDistance)
                    CheckDirection(enumSwipe);
            }
        }

        public void GetBasePoint()
        {
            //Reset Base Touch position
            baseSwipePos = DataManager.instance.inputPosition;
        }

        public void CheckDirection(swipeDirection swipeDir)
        {
            // Check dans quel direction le joueur swipe
            switch (swipeDir)
            {
                case swipeDirection.Front:
                    CheckRaycast(Vector3Custom.forward);
                    break;
                case swipeDirection.Right:
                    CheckRaycast(Vector3Custom.right);
                    break;
                case swipeDirection.Left:
                    CheckRaycast(Vector3Custom.left);
                    break;
                case swipeDirection.Back:
                    CheckRaycast(Vector3Custom.back);
                    break;
            }
        }
        */
        #endregion
        void TEMPORARY______SHIT()
        {
            // X Axis
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (((myIndex - grid.gridSize) + (grid.gridSize * grid.gridSize) - 1) / ((grid.gridSize * grid.gridSize) * (myIndex / (grid.gridSize * grid.gridSize)) + (grid.gridSize * grid.gridSize)) != 0)
                {
                    indexTargetNode = myIndex + _DirectionCustom.left;
                    StartCoroutine(Move(grid.kuboGrid[indexTargetNode - 1].worldPosition));
                    myIndex = myIndex - grid.gridSize;
                }
                else
                {
                    Vector3 degeulasseTODO = new Vector3(xCoordLocal, yCoordLocal, zCoordLocal);
                    degeulasseTODO += _DirectionCustom.vectorLeft;
                    StartCoroutine(MoveFromMap(degeulasseTODO));
                    Debug.LogError("Z AU BORD");
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                // -X Axis
                if ((myIndex + grid.gridSize) / ((grid.gridSize * grid.gridSize) * (myIndex / (grid.gridSize * grid.gridSize) + 1)) != 1)
                {
                    indexTargetNode = myIndex + _DirectionCustom.right;
                    StartCoroutine(Move(grid.kuboGrid[indexTargetNode - 1].worldPosition));
                    myIndex = myIndex + grid.gridSize;
                }
                else
                {
                    Vector3 degeulasseTODO = new Vector3(xCoordLocal, yCoordLocal, zCoordLocal);
                    degeulasseTODO += _DirectionCustom.vectorRight;
                    StartCoroutine(MoveFromMap(degeulasseTODO));
                    Debug.LogError("S AU BORD");
                }
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                // -Z Axis
                if (myIndex - (grid.gridSize * grid.gridSize) >= 0)
                {
                    indexTargetNode = myIndex + _DirectionCustom.backward;
                    StartCoroutine(Move(grid.kuboGrid[indexTargetNode - 1].worldPosition));
                    myIndex = myIndex - (grid.gridSize * grid.gridSize);
                }
                else
                {
                    Vector3 degeulasseTODO = new Vector3(xCoordLocal, yCoordLocal, zCoordLocal);
                    degeulasseTODO += _DirectionCustom.vectorBack;
                    StartCoroutine(MoveFromMap(degeulasseTODO));
                    Debug.LogError("Q AU BORD");
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // Z Axis
                if ((myIndex + (grid.gridSize * grid.gridSize)) / ((grid.gridSize * grid.gridSize * grid.gridSize)) != 1)
                {
                    indexTargetNode = myIndex + _DirectionCustom.forward;
                    StartCoroutine(Move(grid.kuboGrid[indexTargetNode - 1].worldPosition));
                    myIndex = myIndex + (grid.gridSize * grid.gridSize);
                }
                else
                {
                    Vector3 degeulasseTODO = new Vector3(xCoordLocal, yCoordLocal, zCoordLocal);
                    degeulasseTODO += _DirectionCustom.vectorForward;
                    StartCoroutine(MoveFromMap(degeulasseTODO));
                    Debug.LogError("D AU BORD");
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // Y Axis
                if (myIndex % grid.gridSize != 0)
                {
                    indexTargetNode = myIndex + _DirectionCustom.up;
                    StartCoroutine(Move(grid.kuboGrid[indexTargetNode - 1].worldPosition));
                    myIndex = myIndex + 1;
                }
                else
                {
                    Debug.LogError("R AU BORD");
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                // -Y Axis
                if ((myIndex - 1) % grid.gridSize != 0)
                {
                    indexTargetNode = myIndex + _DirectionCustom.down;
                    StartCoroutine(Move(grid.kuboGrid[indexTargetNode - 1].worldPosition));
                    myIndex = myIndex - 1;
                }
                else
                {
                    Debug.LogError("F AU BORD");
                }
            }
        }
    }
}