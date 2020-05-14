using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kubika.CustomLevelEditor;

namespace Kubika.Game
{
    public class ScenesManager : MonoBehaviour
    {
        private static ScenesManager _instance;
        public static ScenesManager instance { get { return _instance; } }

        public ScenesIndex loadToScene;
        public ScenesIndex currentActiveScene;
        AsyncOperation loadingSceneOp;

        void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadSceneAsync((int)ScenesIndex.USER_INTERFACE, LoadSceneMode.Additive);
            loadingSceneOp = SceneManager.LoadSceneAsync((int)loadToScene, LoadSceneMode.Additive);
            currentActiveScene = loadToScene;

        }

        // Update is called once per frame
        void Update()
        {

        }
        
        public void _LoadScene(ScenesIndex targetScene)
        {
            StartCoroutine(LoadScene(targetScene));
        }

        IEnumerator LoadScene(ScenesIndex targetScene)
        {
            SceneManager.UnloadSceneAsync((int)currentActiveScene);
            
            loadingSceneOp = SceneManager.LoadSceneAsync((int)targetScene, LoadSceneMode.Additive);

            while (!loadingSceneOp.isDone) yield return null;

            currentActiveScene = targetScene;

            yield return null;
        }
    }
}