using Kubika.Game;
using Kubika.CustomLevelEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Kubika.Saving
{
    public class SaveAndLoad : MonoBehaviour
    {
        private static SaveAndLoad _instance;
        public static SaveAndLoad instance { get { return _instance; } }

        //a list of the nodes in grid node that have cubes on them
        List<Node> activeNodes = new List<Node>();

        LevelEditorData levelData;

        public GameObject cubePrefab;

        private void Awake()
        {
            if (_instance != null && _instance != this) Destroy(this);
            else _instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            CreateEditorData();
        }

        private LevelEditorData CreateEditorData()
        {
            levelData = new LevelEditorData();
            levelData.nodesToSave = new List<Node>();
            return levelData;
        }

        public void DevSavingLevel(string levelName, int minimumMoves = 0)
        {
            for (int i = 0; i < _Grid.instance.kuboGrid.Length; i++)
            {
                if (_Grid.instance.kuboGrid[i].cubeOnPosition != null) activeNodes.Add(_Grid.instance.kuboGrid[i]);
            }

            //storing data in levelDataFile
            levelData.levelName = levelName;
            levelData.minimumMoves = minimumMoves;
            foreach (Node node in activeNodes) levelData.nodesToSave.Add(node);

            string json = JsonUtility.ToJson(levelData);
            string folder = Application.dataPath + "/Resources/MainLevels";

            string levelFile = "";

            if (levelName != "") levelFile = levelName + ".json";
            else levelFile = "New_Level.json";

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path)) File.Delete(path);

            File.WriteAllText(path, json);

            levelData.nodesToSave.Clear();
            activeNodes.Clear();

            Debug.Log("Level Saved by Dev at " + path);
        }

        public void DevLoadLevel(string levelName)
        {
            Debug.Log("Dev is Loading a Level !");
            string folder = Application.dataPath + "/Resources/MainLevels";

            string levelFile = levelName + ".json";

            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                levelData = JsonUtility.FromJson<LevelEditorData>(json);

                ExtractAndRebuildLevel(levelData);
            }

            levelData.nodesToSave.Clear();
            activeNodes.Clear();
        }

        public void UserSavingLevel(string levelName)
        {
            for (int i = 0; i < _Grid.instance.kuboGrid.Length; i++)
            {
                if (_Grid.instance.kuboGrid[i].cubeOnPosition != null) activeNodes.Add(_Grid.instance.kuboGrid[i]);
            }

            //storing data in levelDataFile
            levelData.levelName = levelName;
            foreach (Node node in activeNodes) levelData.nodesToSave.Add(node);

            string json = JsonUtility.ToJson(levelData);
            string levelFile = levelName + ".json";

            string folder = Application.persistentDataPath + "/UserLevels";

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path)) File.Delete(path);

            File.WriteAllText(path, json);

            LevelFiles.AddNewUserLevel(levelName);
            LevelsManager.instance.RefreshUserLevels();

            levelData.nodesToSave.Clear();
            activeNodes.Clear();

            Debug.Log("Level Saved by User at " + path);
        }

        public void UserLoadLevel(string levelName)
        {
            Debug.Log("User is Loading a Level !");

            string folder = Application.persistentDataPath + "/UserLevels";
            string levelFile = levelName + ".json";
            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                levelData = JsonUtility.FromJson<LevelEditorData>(json);

                ExtractAndRebuildLevel(levelData);
            }

            levelData.nodesToSave.Clear();
            activeNodes.Clear();
        }

        public void UserDeleteLevel(string levelName)
        {
            LevelFiles.DeleteUserLevel(levelName);
            LevelsManager.instance.RefreshUserLevels();
        }

        public void ExtractAndRebuildLevel(LevelEditorData recoveredData)
        {
            // start by resetting the grid's nodes to their base states
            _Grid.instance.ResetGrid();
            _Grid.instance.placedObjects.Clear();

            foreach (Node recoveredNode in recoveredData.nodesToSave)
            {
                GameObject newCube = Instantiate(cubePrefab);

                _Grid.instance.placedObjects.Add(newCube);

                Debug.Log(recoveredNode.cubeType);

                newCube.transform.position = recoveredNode.worldPosition;
                newCube.transform.parent = _Grid.instance.gameObject.transform;

                switch (recoveredNode.cubeType)
                {
                    case CubeTypes.StaticCube:
                        newCube.AddComponent(typeof(_StaticCube));
                        _StaticCube staticCube = newCube.GetComponent<_StaticCube>();

                        //set the cube's index so that you can assign its other variables (position, leyer, type, etc.)
                        staticCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].cubeType = CubeTypes.StaticCube;

                        staticCube.staticEnum = StaticEnums.Empty;
                        break;

                    case CubeTypes.MoveableCube:
                        newCube.AddComponent(typeof(_MoveableCube));
                        _MoveableCube moveableCube = newCube.GetComponent<_MoveableCube>();

                        moveableCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].cubeType = CubeTypes.MoveableCube;
                        break;

                    case CubeTypes.VictoryCube:
                        newCube.AddComponent(typeof(_VictoryCube));
                        _VictoryCube victoryCube = newCube.GetComponent<_VictoryCube>();

                        victoryCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].cubeType = CubeTypes.VictoryCube;
                        break;

                    case CubeTypes.DeliveryCube:
                        newCube.AddComponent(typeof(_DeliveryCube));
                        _DeliveryCube deliveryCube = newCube.GetComponent<_DeliveryCube>();

                        deliveryCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].cubeType = CubeTypes.DeliveryCube;
                        break;

                    case CubeTypes.ElevatorCube:
                        break;

                    case CubeTypes.ConcreteCube:
                        break;

                    case CubeTypes.MineCube:
                        break;

                    case CubeTypes.TimerCube:
                        break;

                    case CubeTypes.SwitchCube:
                        break;

                    case CubeTypes.MirrorCube:
                        break;

                    case CubeTypes.ChaosBall:
                        break;

                    default:
                        //set epmty cubes as cubeEmpty
                        _Grid.instance.kuboGrid[recoveredNode.nodeIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
                        break;
                }
            }

            Debug.Log("Level Loaded !");
        }
    }
}
