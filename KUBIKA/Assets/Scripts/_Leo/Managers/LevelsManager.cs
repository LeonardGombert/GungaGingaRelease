using Kubika.Saving;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

namespace Kubika.Game
{
    public class LevelsManager : MonoBehaviour
    {
        private static LevelsManager _instance;
        public static LevelsManager instance { get { return _instance; } }

        #region MAIN LEVELS
        [FoldoutGroup("Biomes")] public List<LevelFile> masterList = new List<LevelFile>();
        [FoldoutGroup("Biomes")] [ShowInInspector] Queue<LevelFile> levelQueue = new Queue<LevelFile>();

        List<List<TextAsset>> listOfLists = new List<List<TextAsset>>();

        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome1 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome2 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome3 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome4 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome5 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome6 = new List<TextAsset>();
        [FoldoutGroup("Full Biomes")] [SerializeField] List<TextAsset> biome7 = new List<TextAsset>();
        #endregion

        #region LEVEL EDITOR
        [FoldoutGroup("Level Editor ")] public List<string> levelNames = new List<string>();
        [FoldoutGroup("Level Editor ")] public List<LevelFile> playerLevelsInfo = new List<LevelFile>();
        #endregion

        public TextAsset testLevel;
        public string testingKubiCode;

        public TextAsset _levelFile;
        public string _levelName;
        public string _Kubicode;
        public int _minimumMoves;
        public bool _lockRotate;

        void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;

            StartCoroutine(InitializeLevelsList());
        }

        // Start is called before the first frame update
        void Start()
        {
            if (ScenesManager.isLevelEditor) RefreshUserLevels();
        }

        IEnumerator InitializeLevelsList()
        {
            listOfLists.Add(biome1);
            listOfLists.Add(biome2);
            listOfLists.Add(biome3);
            listOfLists.Add(biome4);
            listOfLists.Add(biome5);
            listOfLists.Add(biome6);
            listOfLists.Add(biome7);

            InitializeLists();

            yield return null;
        }

        // Copy all of the individual lists to the master list
        private void InitializeLists()
        {
            masterList.Clear();

            foreach (List<TextAsset> levelFileList in listOfLists)
            {
                foreach (TextAsset level in levelFileList)
                {
                    LevelFile levelInfo = UserLevelFiles.ConvertToLevelInfo(level);
                    masterList.Add(levelInfo);
                }
            }
        }

        // called when Game Scene is loaded, load specific level or set to baseState
        public void BakeLevels(string levelKubicode)
        {
            Debug.Log("I'm looking for a specific level named " + levelKubicode);

            for (int i = 0; i < masterList.Count; i++)
            {
                if (masterList[i].Kubicode != levelKubicode) Debug.Log(masterList[i].Kubicode);

                else if (masterList[i].Kubicode == levelKubicode)
                {
                    Debug.Log("Found specific level");
                    LoadSpecific(i);
                    break;
                }
            }
        }

        private void LoadSpecific(int startingIndex)
        {
            levelQueue.Clear();

            for (int i = startingIndex; i < masterList.Count; i++)
            {
                levelQueue.Enqueue(masterList[i]);
            }
            _LoadNextLevel();
        }

        public void RefreshUserLevels()
        {
            levelNames = UserLevelFiles.GetUserLevelNames();

            while (UIManager.instance == null) return;
            UIManager.instance.playerLevelsDropdown.ClearOptions();

            Debug.Log("Cleared");

            foreach (string levelName in levelNames)
            {
                UIManager.instance.playerLevelsDropdown.options.Add(new Dropdown.OptionData(levelName));
            }

            UIManager.instance.playerLevelsDropdown.RefreshShownValue();
        }

        // Get the next level's information and remove it from the queue
        void GetNextLevelInfo()
        {
            _levelName = levelQueue.Peek().levelName;
            _Kubicode = levelQueue.Peek().Kubicode;
            _levelFile = levelQueue.Peek().levelFile;
            _minimumMoves = levelQueue.Peek().minimumMoves;
            _lockRotate = levelQueue.Peek().lockRotate;
        }

        public void _LoadNextLevel()
        {
            GetNextLevelInfo();
            StartCoroutine(LoadLevel());

            DequeueLevel();

            _DataManager.instance.GameSet();
            _MaterialCentral.instance.MaterialSet();
        }

        // Load the next level (extract the file)
        IEnumerator LoadLevel()
        {
            Debug.Log("1, 2, 3, 4");

            if (_lockRotate) UIManager.instance.TurnOffRotate();
            else UIManager.instance.TurnOnRotate();

            string json = _levelFile.ToString();

            LevelEditorData levelData = JsonUtility.FromJson<LevelEditorData>(json);

            SaveAndLoad.instance.ExtractAndRebuildLevel(levelData);

            VictoryConditionManager.instance.CheckVictoryCubes();

            yield return null;
        }

        private void DequeueLevel()
        {
            levelQueue.Dequeue();
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
    public struct LevelFile
    {
        public string levelName;
        public TextAsset levelFile;
        public int minimumMoves;
        public bool lockRotate;

        public string Kubicode;
    }
}