using Kubika.Saving;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class LevelsQueue : MonoBehaviour
    {
        #region LEVELS
        [FoldoutGroup("Biomes")] public List<LevelFileInfo> masterList = new List<LevelFileInfo>();
        [FoldoutGroup("Biomes")] public Queue<LevelFileInfo> levels = new Queue<LevelFileInfo>();

        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome1 = new List<LevelFileInfo>();
        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome2 = new List<LevelFileInfo>();
        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome3 = new List<LevelFileInfo>();
        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome4 = new List<LevelFileInfo>();
        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome5 = new List<LevelFileInfo>();
        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome6 = new List<LevelFileInfo>();
        [FoldoutGroup("Full Biomes")][SerializeField] List<LevelFileInfo> biome7 = new List<LevelFileInfo>();
        #endregion

        //keep a reference to the save/load instance to quickly extract levels from files
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

        // Copy all of the individual lists to the master list
        private void InitializeLists()
        {
            // copy lists into the master list
            foreach (LevelFileInfo level in biome1) masterList.Add(level);
            foreach (LevelFileInfo level in biome2) masterList.Add(level);
            foreach (LevelFileInfo level in biome3) masterList.Add(level);
            foreach (LevelFileInfo level in biome4) masterList.Add(level);
            foreach (LevelFileInfo level in biome5) masterList.Add(level);
            foreach (LevelFileInfo level in biome6) masterList.Add(level);
            foreach (LevelFileInfo level in biome7) masterList.Add(level);

            ResetQueue();
        }

        // Reset the queue to its base state
        private void ResetQueue()
        {
            // copy the master list into the queue
            foreach (LevelFileInfo level in masterList) levels.Enqueue(level);
            DequeueNextLevel();
        }

        // Get the next level's information and remove it from the queue
        void DequeueNextLevel()
        {
            _levelName = levels.Peek().levelName;
            _levelFile = levels.Peek().levelFile;
            _minimumMoves = levels.Peek().minimumMoves;

            levels.Dequeue();
        }

        // Load the next level (extract the file)
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
    }
}