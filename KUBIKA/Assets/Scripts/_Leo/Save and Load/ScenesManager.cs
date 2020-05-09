using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kubika.Game
{
    public class ScenesManager : MonoBehaviour
    {
        ScenesIndex loadToScene;

        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadSceneAsync((int)loadToScene, LoadSceneMode.Additive);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void LoadScene(ScenesIndex targetScene)
        {
            SceneManager.LoadSceneAsync((int)targetScene, LoadSceneMode.Additive);
        }
    }
}