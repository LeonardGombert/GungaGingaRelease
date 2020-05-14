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
        public CubeMove[] moveCube;

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
        public CubeBase[] baseCube;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update

        public void GameSet()
        {
            moveCube = FindObjectsOfType<CubeMove>(); // TODO : DEGEULASSE
            baseCube = FindObjectsOfType<CubeBase>();
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

            /////// DEMON SCRIPT TODO DEGEULASS

            switch (rotationState)
            {
                case 0:
                    {
                        foreach (CubeBase cBase in baseCube)
                        {
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = null;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
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

                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = null;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex1;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.myCubeLayer;
                            Debug.Log("-1- " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || " + (cBase.myIndex - 1));

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
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = null;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex2;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            _Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.myCubeLayer;
                            Debug.Log("-2- " + _Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || " + (cBase.myIndex - 1));


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
            while (AreCubesCheckingMove(moveCube) == false)
            {
                yield return null;
            }
            EndFalling.RemoveAllListeners();
            StartMoving.Invoke();
            StartCoroutine(CubesAreEndingToMove());

        }


        public IEnumerator CubesAreEndingToMove()
        {
            while (AreCubesEndingToMove(moveCube) == false)
            {
                yield return null;
            }
            StartMoving.RemoveAllListeners();
            EndMoving.Invoke();
            MakeFall();
        }

        public IEnumerator CubesAreCheckingFall()
        {
            while (AreCubesCheckingFall(moveCube) == false)
            {
                yield return null;
            }
            EndMoving.RemoveAllListeners();
            StartFalling.Invoke();
            StartCoroutine(CubesAreEndingToFall());

        }

        public IEnumerator CubesAreEndingToFall()
        {
            while (AreCubesEndingToFall(moveCube) == false)
            {
                yield return null;
            }
            StartFalling.RemoveAllListeners();
            EndFalling.Invoke();
        }


        //////////////////////

        public bool AreCubesCheckingMove(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isCheckingMove == false)
                {
                    return true;
                }
            }

            return false;
        }

        public bool AreCubesCheckingFall(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isCheckingFall == false)
                {
                    return true;
                }
            }

            return false;
        }


        public bool AreCubesEndingToMove(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isMoving == false)
                {
                    return true;
                }
            }

            return false;
        }


        public bool AreCubesEndingToFall(CubeMove[] cubeMove)
        {
            for (int i = 0; i < cubeMove.Length; i++)
            {
                if (cubeMove[i].isFalling == false)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion



    }

}
