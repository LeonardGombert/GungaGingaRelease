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
        public float moveTime = 0.25f;
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

            TEMPORARY______SHIT();
        }

        public void CheckIfFalling()
        {
            /*
            if (gridRef.kuboGrid[myIndex +_DirectionCustom.down -1].cubeLayers == CubeLayers.cubeEmpty)
            {
                //"garbage collection"
                //-1 because you're looking at the curretn NODE
                grid.kuboGrid[myIndex - 1].cubeOnPosition = null;
                grid.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;

                //falling     

                //set the new index
                myIndex = gridRef.kuboGrid[myIndex +_DirectionCustom.down - 1].nodeIndex;
                
                //move to updated index
                transform.position = gridRef.kuboGrid[myIndex + _DirectionCustom.down].worldPosition;
                //set updated index to cubeMoveable
                gridRef.kuboGrid[myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
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

            if (gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeEmpty)
            {
                Debug.Log("EmptyDetected --" + (myIndex - 1 + _DirectionCustom.down * nbrCubeBelowParam) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                thereIsEmpty = true;
                nbrCubeEmptyBelow += 1;
                Fall(nbrCubeBelowParam + 1); 
            }
            else if (gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeMoveable)
            {
                Debug.Log("MoveDetected --" + (myIndex - 1 + _DirectionCustom.down * nbrCubeBelowParam) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                nbrCubeMouvableBelow += 1;
                Fall(nbrCubeBelowParam + 1);
            }
            else if (gridRef.kuboGrid[myIndex - 1 + (_DirectionCustom.down * nbrCubeBelowParam)].cubeLayers == CubeLayers.cubeFull)
            {
                Debug.Log("FULLDetected --" + (myIndex + (_DirectionCustom.down * nbrCubeBelowParam) - 1) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex -1));
                nbrCubeBelow = nbrCubeBelowParam;

                indexTargetNode = myIndex + (_DirectionCustom.down * nbrCubeBelow) + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1));
                nextPosition = gridRef.kuboGrid[indexTargetNode - 1].worldPosition;

                Debug.Log("FULLDetected -- Final Destination " + (myIndex - 1 + (_DirectionCustom.down * nbrCubeBelow) + (_DirectionCustom.up * (nbrCubeMouvableBelow + 1))) + " || myIndex " + myIndex + " || myIndexGrid " + (myIndex - 1));
                isChecking = false;
            }
        }

        #region MOVE

        public void FallMoveFunction()
        {
            Debug.Log("FallMoveFunction");
            if ( thereIsEmpty == true)
                StartCoroutine(FallMove(nextPosition, nbrCubeEmptyBelow, nbrCubeBelow));
        }

        public IEnumerator FallMove(Vector3 fallPosition, int nbrCub, int nbrCubeBelowParam)
        {
            Debug.Log("MOVING ");
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

            myIndex = indexTargetNode;
            gridRef.kuboGrid[indexTargetNode - 1].cubeOnPosition = gameObject;
            //set updated index to cubeMoveable
            gridRef.kuboGrid[indexTargetNode - 1].cubeLayers = CubeLayers.cubeMoveable;
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