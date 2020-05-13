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
        public UnityEvent EndChecking;
        public UnityEvent EndMoveChecking;
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
            StartFalling.AddListener(MakeFall);
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
            StartCoroutine(CheckIfCubeAreChecking());
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
        public IEnumerator CheckIfCubeAreChecking()
        {
            while (EveryCubeAreChecking(moveCube) == false)
            {
                yield return null;
            }

            EndFalling.RemoveAllListeners();
            EndChecking.Invoke();
        }

        public IEnumerator CheckIfCubeAreMoveChecking()
        {
            while (EveryCubeAreChecking(moveCube) == false)
            {
                Debug.Log("DATA_CHECK");
                yield return null;
            }

            EndChecking.RemoveAllListeners();
            EndMoveChecking.Invoke();
            StartCoroutine(CheckIfCubeAreStopped());
        }

        public bool EveryCubeAreChecking(CubeMove[] allMouvable)
        {

            for (int i = 0; i < allMouvable.Length; i++)
            {
                if (allMouvable[i].isChecking == false)
                {
                    Debug.LogError("TRUE");
                    return true;
                }
                Debug.LogError("FALSE");
            }
            Debug.LogError("MEGA FALSE");
            return false;
        }

        public IEnumerator CheckIfCubeAreStopped()
        {
            while (EveryCubeAreStopping(moveCube) == false)
            {
                yield return null;
            }

            Debug.Log("DATA-STOP-CHECK ");
            EndMoveChecking.RemoveAllListeners();
            EndMoving.Invoke();
            StartCoroutine(CheckIfCubeAreStartingFalling());
        }

        public bool EveryCubeAreStopping(CubeMove[] allMouvable)
        {
            for (int i = 0; i < allMouvable.Length ; i++)
            {
                //Debug.Log("DATA-STOP-CHECK---1 " + allMouvable.Length + " || " + allMouvable[i].gameObject.name);
                if (allMouvable[i].isMoving == false)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator CheckIfCubeAreFalling()
        {
            while (EveryCubeAreStopping(moveCube) == false)
            {
                yield return null;
            }
            //StartFalling.RemoveAllListeners();
            EndFalling.Invoke();
        }

        public bool EveryCubeAreFalling(CubeMove[] allMouvable)
        {

            for (int i = 0; i < allMouvable.Length; i++)
            {
                if (allMouvable[i].isFalling == false)
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerator CheckIfCubeAreStartingFalling()
        {
            while (EveryCubeAreStartingFalling(moveCube) == false)
            {
                yield return null;
            }
            EndMoving.RemoveAllListeners();
            StartFalling.Invoke();
            StartCoroutine(CheckIfCubeAreFalling());
        }

        public bool EveryCubeAreStartingFalling(CubeMove[] allMouvable)
        {

            for (int i = 0; i < allMouvable.Length; i++)
            {
                if (allMouvable[i].isMoving == false)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion



    }

}
