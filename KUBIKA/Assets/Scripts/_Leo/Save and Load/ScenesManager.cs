using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kubika.Game
{
    public class ScenesManager : MonoBehaviour
    {
        ScenesIndex loadToScene;
        ScenesIndex currentActiveScene;
        AsyncOperation loadingSceneOp;

        // Start is called before the first frame update
        void Start()
        {
            currentActiveScene = loadToScene;
            loadingSceneOp = SceneManager.LoadSceneAsync((int)loadToScene, LoadSceneMode.Additive);
        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator LoadScene(ScenesIndex targetScene)
        {
            SceneManager.UnloadSceneAsync((int)currentActiveScene);
            
            loadingSceneOp = SceneManager.LoadSceneAsync((int)targetScene, LoadSceneMode.Additive);
            
            currentActiveScene = targetScene;
            yield return null;
        }
    }
}