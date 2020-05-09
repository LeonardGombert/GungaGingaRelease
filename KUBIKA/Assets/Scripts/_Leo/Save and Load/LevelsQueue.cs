using Kubika.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kubika.Game
{
    public class LevelsQueue : MonoBehaviour
    {
        public List<LevelFileInfo> listOfLevels = new List<LevelFileInfo>();
        public Queue<LevelFileInfo> levels;

        SaveAndLoad saveAndLoad;

        string _levelName;
        int _minimumMoves;
        TextAsset _levelFile;

        // Start is called before the first frame update
        void Start()
        {
            saveAndLoad = SaveAndLoad.instance;

            InitializeLists();
        }

        private void InitializeLists()
        {
            levels = new Queue<LevelFileInfo>();

            foreach (LevelFileInfo level in listOfLevels)
            {
                levels.Enqueue(level);
            }

            DequeueNextLevel();
        }

        // Update is called once per frame
        void Update()
        {

        }

        // Get the next level's information and remove it from the queue
        void DequeueNextLevel()
        {
            _levelName = levels.Peek().levelName;
            _levelFile = levels.Peek().levelFile;
            _minimumMoves = levels.Peek().minimumMoves;

            levels.Dequeue();
        }

        //load the next level (extract the file)
        public void LoadLevel()
        {
            string json = _levelFile.ToString();

            LevelEditorData levelData = JsonUtility.FromJson<LevelEditorData>(json);

            saveAndLoad.ExtractAndRebuildLevel(levelData);
        }
    }
}

namespace Kubika.Saving
{
    [Serializable]
    public class LevelFileInfo
    {
        public string levelName;
        public TextAsset levelFile;
        public int minimumMoves;
        
        //public int gridSize;
    }
}