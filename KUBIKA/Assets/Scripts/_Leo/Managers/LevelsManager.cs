using Kubika.Saving;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Kubika.Game
{
    public class LevelsManager : MonoBehaviour
    {
        private static LevelsManager _instance;
        public static LevelsManager instance { get { return _instance; } }

        #region MAIN LEVELS
        [FoldoutGroup("Biomes")] public List<LevelFileInfo> masterList = new List<LevelFileInfo>();
        [FoldoutGroup("Biomes")] public Queue<LevelFileInfo> levels = new Queue<LevelFileInfo>();

        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome1 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome2 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome3 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome4 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome5 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome6 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome7 = new List<TextAsset>();

        List<List<TextAsset>> listOfLists = new List<List<TextAsset>>();
        #endregion

        #region LEVEL EDITOR
        [FoldoutGroup("Level Editor ")] public List<string> levelNames = new List<string>();
        [FoldoutGroup("Level Editor ")] public List<LevelFileInfo> playerLevelsInfo = new List<LevelFileInfo>();
        #endregion

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
            /*listOfLists.Add(biome1);
            listOfLists.Add(biome2);
            listOfLists.Add(biome3);
            listOfLists.Add(biome4);
            listOfLists.Add(biome5);
            listOfLists.Add(biome6);
            listOfLists.Add(biome7);

            InitializeLists();*/
            if (ScenesManager.isLevelEditor) RefreshUserLevels();
        }

        // Copy all of the individual lists to the master list
        private void InitializeLists()
        {
            foreach (List<TextAsset> levelFileList in listOfLists)
            {
                foreach (TextAsset level in levelFileList)
                {
                    LevelFileInfo levelInfo = LevelFiles.ConvertToLevelInfo(level);
                    masterList.Add(levelInfo);
                }
            }

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

        public void RefreshUserLevels()
        {
            levelNames = LevelFiles.GetUserLevelNames();

            while (UIManager.instance == null) return;
            UIManager.instance.playerLevelsDropdown.ClearOptions();

            Debug.Log("Cleared");

            foreach (string levelName in levelNames)
            {
                UIManager.instance.playerLevelsDropdown.options.Add(new Dropdown.OptionData(levelName));
            }

            UIManager.instance.playerLevelsDropdown.RefreshShownValue();
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