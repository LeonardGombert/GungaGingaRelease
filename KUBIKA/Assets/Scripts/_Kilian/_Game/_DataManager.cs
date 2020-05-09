using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            if(Input.GetKeyDown(KeyCode.W))
            {
                moveCube = FindObjectsOfType<_MoveableCube>(); // TODO : DEGEULASSE
            }
        }

        #region TIMED EVENT
        public IEnumerator CheckIfCubeAreChecking()
        {
            moveCube = FindObjectsOfType<_MoveableCube>();
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
            moveCube = FindObjectsOfType<_MoveableCube>();
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
