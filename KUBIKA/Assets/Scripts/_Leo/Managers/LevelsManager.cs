using Kubika.Saving;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Kubika.Game
{
    public class LevelsManager : MonoBehaviour
    {
        private static LevelsManager _instance;
        public static LevelsManager instance { get { return _instance; } }

        #region MAIN LEVELS
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

        #region LEVEL EDITOR
        public UnityEngine.Object[] levelObjects;
        public List<LevelFileInfo> playerLevelsInfo = new List<LevelFileInfo>();
        #endregion

        //keep a reference to the save/load instance to quickly extract levels from files
        SaveAndLoad saveAndLoad;

        string _levelName;
        int _minimumMoves;
        TextAsset _levelFile;

        void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
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

            if (ScenesManager.isLevelEditor) InitializePlayerLevels();
        }

        private void InitializePlayerLevels()
        {
            levelObjects = Resources.LoadAll("/PlayerLevels", typeof(TextAsset));

            foreach (TextAsset item in levelObjects)
            {
                LevelFileInfo levelInfo = new LevelFileInfo();

                LevelEditorData levelData = JsonUtility.FromJson<LevelEditorData>(item.ToString());

                levelInfo.levelFile = item;
                levelInfo.levelName = levelData.levelName;
                levelInfo.minimumMoves = levelData.minimumMoves;

                playerLevelsInfo.Add(levelInfo);
            }

            /*foreach (LevelFileInfo level in playerLevelsInfo)
            {
                UIManager.instance.playerLevelsDropdown.captionText.text = level.levelName;
            }*/
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

            SaveAndLoad.instance.ExtractAndRebuildLevel(levelData);
        }
        public void RestartLevel()
        {
            throw new NotImplementedException();
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