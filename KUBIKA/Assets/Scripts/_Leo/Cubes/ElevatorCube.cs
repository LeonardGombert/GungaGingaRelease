using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kubika.CustomLevelEditor;

namespace Kubika.Game
{
    public class ElevatorCube : _CubeScanner
    {
        // Start is called before the first frame update

        public bool isGreen = true;

        // BOOL ACTION
        [Space]
        [Header("BOOL ACTION")]
        public bool isCheckingMove;
        public bool isMoving;
        public bool isOutside;
        public bool isReadyToMove;
        public bool cubeIsStillInPlace;

        // LERP
        Vector3 currentPos;
        Vector3 basePos;
        float currentTime;
        public float moveTime = 0.5f;

        // COORD SYSTEM
        [Space]
        [Header("COORD SYSTEM")]
        public int xCoordLocal;
        public int yCoordLocal;
        public int zCoordLocal;

        //FALL OUTSIDE
        [Space]
        [Header("OUTSIDE")]
        public int nbrDeCubeFallOutside = 10;
        Vector3 moveOutsideTarget;

        // MOVE
        [Space]
        [Header("MOVE")]
        public Node soloMoveTarget;
        public Node soloPileTarget;
        public Vector3 outsideMoveTarget;
        int indexTargetNode;

        //PUSH
        public _CubeMove pushNextNodeCubeMove;

        //PILE
        public _CubeMove pileNodeCubeMove;

        public override  void Start()
        {
            //ScannerSet();
            _DataManager.instance.EndFalling.AddListener(CheckingIfCanPush);
            _DataManager.instance.EndMoving.AddListener(ResetReadyToMove);

            //call base.start AFTER assigning the cube's layers
            base.Start();

            //starts as a static cube
            isStatic = true;
        }

        // Update is called once per frame
        public override void Update()
        {
            base.Update();
            if(Input.GetKeyDown(KeyCode.B))
            {
                //ScannerSet();
                CheckingIfCanPush();
            }
        }

        void CheckingIfCanPush()
        {
            isCheckingMove = true;
            if (isGreen)
            {
                Debug.Log("-0-");
                if (MatrixLimitCalcul(myIndex, _DirectionCustom.LocalScanner(facingDirection)))
                {
                    Debug.Log("-1-");
                    if (grid.kuboGrid[myIndex - 1 + _DirectionCustom.LocalScanner(facingDirection)].cubeLayers == CubeLayers.cubeEmpty)
                    {
                        isCheckingMove = false;
                        cubeIsStillInPlace = false;
                    }
                    else if (grid.kuboGrid[myIndex - 1 + _DirectionCustom.LocalScanner(facingDirection)].cubeLayers == CubeLayers.cubeMoveable && cubeIsStillInPlace == false)
                    {
                        Debug.Log("-3-");
                        isCheckingMove = true;
                        cubeIsStillInPlace = true;
                        CheckingMove(myIndex, _DirectionCustom.LocalScanner(facingDirection));
                        StartCoroutine(_DataManager.instance.CubesAndElevatorAreCheckingMove());
                    }
                    else
                    {
                        Debug.Log("-4-");
                        isCheckingMove = false;
                    }
                }
                else
                {
                    Debug.Log("-5-");
                    isCheckingMove = false;
                }
            }
            else
            {
                Debug.Log("-6-");
                if (MatrixLimitCalcul(myIndex, _DirectionCustom.LocalScanner(facingDirection)))
                {
                    Debug.Log("-7-");
                    if (grid.kuboGrid[myIndex - 1 + _DirectionCustom.LocalScanner(facingDirection)].cubeLayers == CubeLayers.cubeEmpty)
                    {
                        cubeIsStillInPlace = false;
                    }
                    else if (grid.kuboGrid[myIndex - 1 + (_DirectionCustom.LocalScanner(facingDirection))].cubeLayers == CubeLayers.cubeMoveable)
                    {
                        Debug.Log("-8-");                       
                        if (cubeIsStillInPlace == false)
                        {
                            Debug.Log("-9- | " + -_DirectionCustom.LocalScanner(facingDirection) + " || alcol " + _DirectionCustom.LocalScanner(facingDirection));
                            isCheckingMove = true;
                            CheckingMove(myIndex, -_DirectionCustom.LocalScanner(facingDirection));
                            StartCoroutine(_DataManager.instance.CubesAndElevatorAreCheckingMove());
                        }
                        else
                        {
                            isCheckingMove = false;
                        }

                    }
                    else
                    {
                        Debug.Log("-11-");
                        isCheckingMove = false;
                    }
                }
                else
                {
                    Debug.Log("-12-");
                    isCheckingMove = false;
                }
            }
        }

        #region MOVE

        public IEnumerator Move(Node nextNode)
        {
            isMoving = true;
            isCheckingMove = false;
            Debug.Log("IS MOVINGELEVATOR || isMoving = " + isMoving);

            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
            grid.kuboGrid[myIndex - 1].cubeType = CubeTypes.None;

            basePos = transform.position;
            currentTime = 0;

            while (currentTime <= 1)
            {
                currentTime += Time.deltaTime;
                currentTime = (currentTime / moveTime);

                currentPos = Vector3.Lerp(basePos, nextNode.worldPosition, currentTime);

                transform.position = currentPos;
                yield return transform.position;
            }

            //Debug.Log(" nextNode.nodeIndex-1 = " + nextNode.nodeIndex + " ||nextNode.cubeLayers " + nextNode.cubeLayers);


            myIndex = nextNode.nodeIndex;
            nextNode.cubeOnPosition = gameObject;
            //set updated index to cubeMoveable
            nextNode.cubeLayers = CubeLayers.cubeFull;
            nextNode.cubeType = myCubeType;

            //Debug.Log(" nextNode.nodeIndex-2 = " + nextNode.nodeIndex + " ||nextNode.cubeLayers " + nextNode.cubeLayers);

            xCoordLocal = grid.kuboGrid[nextNode.nodeIndex - 1].xCoord;
            yCoordLocal = grid.kuboGrid[nextNode.nodeIndex - 1].yCoord;
            zCoordLocal = grid.kuboGrid[nextNode.nodeIndex - 1].zCoord;

            isMoving = false;


            Debug.Log("END MOVING ELEVATOR");

        }

        public IEnumerator MoveFromMap(Vector3 nextPosition)
        {
            isMoving = true;
            isOutside = true;
            isCheckingMove = false;

            moveOutsideTarget = new Vector3(nextPosition.x * grid.offset, nextPosition.y * grid.offset, nextPosition.z * grid.offset);

            grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
            grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
            grid.kuboGrid[myIndex - 1].cubeType = CubeTypes.None;

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

        }

        public void MoveToTarget()
        {
            isGreen ^= true;

            Debug.Log("IS_GREEN_INVERTEEEEEEED = " + isGreen);

            if (isOutside == false)
            {
                Debug.Log("MoveToTarget-MOVINGELEVATOR");
                StartCoroutine(Move(soloMoveTarget));
            }
            else
            {
                Debug.Log("MoveToOUTSIDE-MOVINGELEVATOR");
                StartCoroutine(MoveFromMap(outsideMoveTarget));
            }
        }

        public void ResetReadyToMove()
        {
            Debug.Log("RESETELEVATOR");
            isReadyToMove = false;
            pileNodeCubeMove = null;
            pushNextNodeCubeMove = null;
        }

        void CheckingMove(int index, int nodeDirection)
        {
            isMoving = true;
            Debug.Log("CheckingMoveELEVATOR ");
            _DataManager.instance.StartMoving.AddListener(MoveToTarget);
            CheckSoloMove(index, nodeDirection);
        }

        public void CheckSoloMove(int index, int nodeDirection)
        {
            if (MatrixLimitCalcul(index, nodeDirection))
            {
                indexTargetNode = index + nodeDirection;
                Debug.Log("---CheckSoloMoveELEVATOR--- | " + nodeDirection);

                switch (grid.kuboGrid[indexTargetNode - 1].cubeLayers)
                {
                    case CubeLayers.cubeFull:
                        {
                            Debug.Log("STUCKELEVATOR ");
                            soloMoveTarget = grid.kuboGrid[myIndex - 1];
                            isCheckingMove = false;
                        }
                        break;
                    case CubeLayers.cubeEmpty:
                        {
                            Debug.Log("EMPTYELEVATOR ");

                            isReadyToMove = true;
                            soloMoveTarget = grid.kuboGrid[myIndex + nodeDirection - 1];
                            if (grid.kuboGrid[myIndex - 1 + _DirectionCustom.up].cubeLayers == CubeLayers.cubeMoveable && MatrixLimitCalcul(myIndex, _DirectionCustom.up))
                            {
                                pileNodeCubeMove = grid.kuboGrid[myIndex - 1 + _DirectionCustom.up].cubeOnPosition.GetComponent<_CubeMove>();
                                pileNodeCubeMove.CheckingPile(pileNodeCubeMove.myIndex - 1, nodeDirection);
                            }
                            Debug.Log("EMPTY-CAN MOVE-ELEVATOR");

                            isCheckingMove = false;
                        }
                        break;
                    case CubeLayers.cubeMoveable:
                        {
                            pushNextNodeCubeMove = grid.kuboGrid[indexTargetNode - 1].cubeOnPosition.GetComponent<_CubeMove>();
                            if (pushNextNodeCubeMove.isReadyToMove == false)
                            {
                                if (grid.kuboGrid[indexTargetNode + nodeDirection - 1].cubeLayers == CubeLayers.cubeEmpty && grid.kuboGrid[indexTargetNode + nodeDirection + _DirectionCustom.down - 1].cubeLayers == CubeLayers.cubeEmpty)
                                {
                                    Debug.Log("OVER NOTHING");
                                    pushNextNodeCubeMove.isOverNothing = true;
                                }
                                else if(!MatrixLimitCalcul(indexTargetNode, nodeDirection))
                                {
                                    Debug.Log("OUSIDE");
                                    pushNextNodeCubeMove.isOutside = true;
                                }

                                Debug.Log("APPLY DIRECTION " + nodeDirection);
                                pushNextNodeCubeMove.CheckingMove(indexTargetNode, nodeDirection);
                            }
                            CheckSoloMove(indexTargetNode, nodeDirection);

                        }
                        break;
                }

            }
            else
            {
                isOutside = true;
                outsideMoveTarget = outsideCoord(myIndex, -nodeDirection);
                Debug.Log("MATRIX LIMIT SOLOELEVATOR");
                isCheckingMove = false;
            }


        }

        Vector3 outsideCoord(int myIndexParam, int knowedDirection)
        {
            int newXCoord = grid.kuboGrid[myIndexParam - 1].xCoord - grid.kuboGrid[myIndexParam - 1 + knowedDirection].xCoord;
            int newYCoord = grid.kuboGrid[myIndexParam - 1].yCoord - grid.kuboGrid[myIndexParam - 1 + knowedDirection].yCoord;
            int newZCoord = grid.kuboGrid[myIndexParam - 1].zCoord - grid.kuboGrid[myIndexParam - 1 + knowedDirection].zCoord;

            newXCoord += grid.kuboGrid[myIndexParam - 1].xCoord;
            newYCoord += grid.kuboGrid[myIndexParam - 1].yCoord;
            newZCoord += grid.kuboGrid[myIndexParam - 1].zCoord;

            return new Vector3(newXCoord, newYCoord, newZCoord);
        }



        #endregion



    }
}