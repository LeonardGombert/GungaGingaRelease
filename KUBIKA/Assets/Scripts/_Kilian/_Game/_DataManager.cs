using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Kubika.LevelEditor;

namespace Kubika.Game
{
    public class _DataManager : MonoBehaviour
    {
        // INSTANCE
        private static _DataManager _instance;
        public static _DataManager instance { get { return _instance; } }

        // MOVEABLE CUBE
        public _MoveableCube[] moveCube;

        //UNITY EVENT
        public UnityEvent EndChecking;
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
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                moveCube = FindObjectsOfType<_MoveableCube>(); // TODO : DEGEULASSE
                baseCube = FindObjectsOfType<CubeBase>();
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
            foreach (_MoveableCube cubes in moveCube)
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

            /////// DEMON SCRIPT TODO DEGEULASSE

            LevelEditor.Grid.instance.ResetIndexGrid();

            switch (rotationState)
            {
                case 0:
                    {
                        foreach (CubeBase cBase in baseCube)
                        {

                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex0;
                            LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.layer;

                        }
                    }
                    break;
                case 1:
                    {


                        foreach (CubeBase cBase in baseCube)
                        {

                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex1;
                            LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.layer;
                            Debug.Log("-1- " + LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || " + (cBase.myIndex - 1));

                        }
                        /*for (int i = 0; i < baseCube.Length; i++)
                        {

                            //Debug.Log("-2- GRID " + (baseCube[i].myIndex - 1) + " || LOCAL" + (baseCube[i].myIndex) + " || LE " + LevelEditor.Grid.instance.kuboGrid[baseCube[i].myIndex - 1].nodeIndex);
                        }*/
                    }
                    break;
                case 2:
                    {


                        foreach (CubeBase cBase in baseCube)
                        {
                            cBase.myIndex = indexBankScriptable.indexBank[cBase.myIndex - 1].nodeIndex2;
                            LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].cubeOnPosition = cBase.gameObject;
                            LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].cubeLayers = cBase.layer;
                            Debug.Log("-2- " + LevelEditor.Grid.instance.kuboGrid[cBase.myIndex - 1].nodeIndex + " || " + (cBase.myIndex - 1));


                        }

                        /*for (int i = 0; i < baseCube.Length; i++)
                        {
                            LevelEditor.Grid.instance.kuboGrid[baseCube[i].myIndex - 1].cubeOnPosition = baseCube[i].gameObject;
                            LevelEditor.Grid.instance.kuboGrid[baseCube[i].myIndex - 1].cubeLayers = baseCube[i].layer;
                            //Debug.Log("-2- GRID " + (baseCube[i].myIndex - 1) + " || LOCAL" + (baseCube[i].myIndex) + " || LE " + LevelEditor.Grid.instance.kuboGrid[baseCube[i].myIndex - 1].nodeIndex);
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
                Debug.LogError("CheckError");
                yield return null;
            }

            Debug.LogError("GOOD");
            EndChecking.Invoke();
        }

        public bool EveryCubeAreChecking(_MoveableCube[] allMouvable)
        {

            for (int i = 0; i < allMouvable.Length; ++i)
            {
                if (allMouvable[i].isChecking == false)
                {
                    Debug.LogError("TRUE");
                    return true;
                }
                Debug.LogError("FALSE");
            }

            return false;
        }

        public IEnumerator CheckIfCubeAreFalling()
        {
            while (EveryCubeAreChecking(moveCube) == false)
            {
                yield return null;
            }
            EndFalling.Invoke();
        }

        public bool EveryCubeAreFalling(_MoveableCube[] allMouvable)
        {

            for (int i = 0; i < allMouvable.Length; ++i)
            {
                if (allMouvable[i].isFalling == false)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion
    }

}
