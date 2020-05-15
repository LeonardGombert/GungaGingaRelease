using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kubika.CustomLevelEditor;

namespace Kubika.Game
{
    public class _DataManager : MonoBehaviour
    {
        // INSTANCE
        private static _DataManager _instance;
        public static _DataManager instance { get { return _instance; } }

        // MOVEABLE CUBE
        public  CubeMove[] moveCubeArray;
        public List<CubeMove> moveCube = new List<CubeMove>();

        //UNITY EVENT
        public UnityEvent StartChecking;
        public UnityEvent StartMoving;
        public UnityEvent EndMoving;
        public UnityEvent StartFalling;
        public UnityEvent EndFalling;

        // _DIRECTION_CUSTOM
        public int actualRotation;

        //INDEX BANK
        [Space]
        [Header("INDEX BANK")]
        public _DataMatrixScriptable indexBankScriptable;
        public CubeBase[] baseCubeArray;
        public List<CubeBase> baseCube = new List<CubeBase>();

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update

        public void GameSet()
        {
            moveCubeArray = FindObjectsOfType<CubeMove>(); // TODO : DEGEULASSE
            baseCubeArray = FindObjectsOfType<CubeBase>();

            foreach(CubeMove cube in moveCubeArray)
            {
                moveCube.Add(cube);
            }

            foreach (CubeBase cube in baseCubeArray)
            {
                baseCube.Add(cube);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                GameSet();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                MakeFall();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _DirectionCustom.rotationState = actualRotation;
            }
        }


        #region MAKE FALL
        public void MakeFall()
        {
            foreach (CubeMove cubes in moveCube)
            {
                cubes.CheckIfFalling();
            }
            StartCoroutine(CubesAreCheckingFall());
        }
        #endregion

        #region INDEX RESET

        public void ResetIndex(int rotationState)
        {
            Debug.LogError("RotSte " + rotationState);

            foreach (CubeBase cBase in baseCube)
            {
                _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = null;
                _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
                //Debug.Log("DEELTE ALL = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers + " || Index = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex);
            }

            /////// DEMON SCRIPT TODO DEGEULASS

            switch (rotationState)
            {
                case 0:
                    {
                        foreach (CubeBase cBase in baseCube)
                        {
                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex0;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.myCubeLayer;

                        }
                    }
                    break;
                case 1:
                    {


                        foreach (CubeBase cBase in baseCube)
                        {
                            //Debug.Log("cBase INdex = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || node0 = " + indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex0 + " || node1 = " + indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex1 + " || Name = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition.name);
                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex1;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.myCubeLayer;
                            //Debug.Log("-1- " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || Layer = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers);

                        }
                        /*for (int i = 0; i < baseCube.Length; i++)
                        {

                            //Debug.Log("-2- _Grid " + (baseCube[i].myIndex - 1) + " || LOCAL" + (baseCube[i].myIndex) + " || LE " + _Grid.instance.kuboGrid[baseCube[i].myIndex - 1].nodeIndex);
                        }*/
                    }
                    break;
                case 2:
                    {


                        foreach (CubeBase cBase in baseCube)
                        {
                            //Debug.Log("cBase INdex = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || node0 = " + indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex0 + " || node2 = " + indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex2 + " || Name = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition.name);
                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex2;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.myCubeLayer;
                            //Debug.Log("-2- " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || Layer = " + _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers);


                        }

                        /*for (int i = 0; i < baseCube.Length; i++)
                        {
                            _Grid.instance.kuboGrid[baseCube[i].myIndex - 1].cubeOnPosition = baseCube[i].gameObject;
                            _Grid.instance.kuboGrid[baseCube[i].myIndex - 1].cubeLayers = baseCube[i].layer;
                            //Debug.Log("-2- _Grid " + (baseCube[i].myIndex - 1) + " || LOCAL" + (baseCube[i].myIndex) + " || LE " + _Grid.instance.kuboGrid[baseCube[i].myIndex - 1].nodeIndex);
                        }*/
                    }
                    break;
            }

        }

        #endregion

        #region TIMED EVENT
        public IEnumerator CubesAreCheckingMove()
        {
            while (AreCubesCheckingMove(moveCube.ToArray()) == false)
            {
                yield return null;
            }
            Debug.LogError("DATA- CHECK-END");
            //EndFalling.RemoveAllListeners();
            StartMoving.Invoke();
            StartCoroutine(CubesAreEndingToMove());
        }


        public IEnumerator CubesAreEndingToMove()
        {
            while (AreCubesEndingToMove(moveCube.ToArray()) == false)
            {
                yield return null;
            }
            Debug.LogError("DATA- MOVE-END");
            StartMoving.RemoveAllListeners();
            EndMoving.Invoke();
            MakeFall();
        }

        public IEnumerator CubesAreCheckingFall()
        {
            while (AreCubesCheckingFall(moveCube.ToArray()) == false)
            {
                yield return null;
            }
            Debug.LogError("DATA- FALLCHECK-END");
            //EndMoving.RemoveAllListeners();
            StartFalling.Invoke();
            StartCoroutine(CubesAreEndingToFall());
        }

        public IEnumerator CubesAreEndingToFall()
        {
            while (AreCubesEndingToFall(moveCube.ToArray()) == false)
            {
                yield return null;
            }
            Debug.LogError("DATA- FALLING-END");
            //StartFalling.RemoveAllListeners();
            EndFalling.Invoke();
        }


        //////////////////////

        public bool AreCubesCheckingMove(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isCheckingMove == true)
                {
                    return false;
                }
            }

            return true;
        }

        public bool AreCubesCheckingFall(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isCheckingFall == true)
                {
                    return false;
                }
            }

            return true;
        }


        public bool AreCubesEndingToMove(CubeMove[] cubeMove)
        {
            Debug.LogError("CUBE-MOVE-LENGTH  = " + cubeMove.Length);

            for (int i = 0; i < cubeMove.Length; i++)
            {
                Debug.LogError("WHO IS MOVING IN LIST" + i);

                if (cubeMove[i].isMoving == true)
                {
                    Debug.LogError("WHO IS MOVING + cubeMove-NAME = " + cubeMove[i].gameObject.name + " || isMoving = " + cubeMove[i].isMoving);
                    return false;
                }
            }

            return true;
        }


        public bool AreCubesEndingToFall(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isFalling == true)
                {
                    return false;
                }
            }

            return true;
        }
        #endregion



    }

}
