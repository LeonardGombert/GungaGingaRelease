using Kubika.Game;
using Kubika.CustomLevelEditor;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Kubika.Gam;

namespace Kubika.Saving
{
    public class SaveAndLoad : MonoBehaviour
    {
        private static SaveAndLoad _instance;
        public static SaveAndLoad instance { get { return _instance; } }

        //a list of the nodes in grid node that have cubes on them
        List<Node> activeNodes = new List<Node>();

        Node currentNode;

        LevelEditorData levelData;

        public GameObject cubePrefab;

        public string currentOpenLevelName;
        public string currentKubicode;
        public bool currentLevelLockRotate;
        public int currentMinimumMoves;
        
        _Grid grid;

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

        public void DevSavingLevel(string levelName, string kubiCode, bool rotateLock, int minimumMoves = 0)
        {
            for (int i = 0; i < _Grid.instance.kuboGrid.Length; i++)
            {
                if (_Grid.instance.kuboGrid[i].cubeOnPosition != null) activeNodes.Add(_Grid.instance.kuboGrid[i]);
            }

            //storing data in levelDataFile
            levelData.levelName = levelName;
            levelData.lockRotate = rotateLock;
            levelData.minimumMoves = minimumMoves;
            levelData.Kubicode = kubiCode;

            currentOpenLevelName = levelName;
            currentKubicode = kubiCode;
            currentLevelLockRotate = rotateLock;
            currentMinimumMoves = minimumMoves;


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

        public void DevSavingCurrentLevel()
        {
            levelData.levelName = currentOpenLevelName;
            levelData.Kubicode = currentKubicode;
            levelData.lockRotate = currentLevelLockRotate;
            levelData.minimumMoves = currentMinimumMoves;

            DevSavingLevel(currentOpenLevelName, currentKubicode, currentLevelLockRotate, currentMinimumMoves);
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

            currentOpenLevelName = levelData.levelName;
            currentKubicode = levelData.Kubicode;
            currentLevelLockRotate = levelData.lockRotate;
            currentMinimumMoves = levelData.minimumMoves;

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

            currentOpenLevelName = levelName;

            foreach (Node node in activeNodes) levelData.nodesToSave.Add(node);

            string json = JsonUtility.ToJson(levelData);
            string levelFile = levelName + ".json";

            string folder = Application.persistentDataPath + "/UserLevels";

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path)) File.Delete(path);

            File.WriteAllText(path, json);

            UserLevelFiles.AddNewUserLevel(levelName);
            LevelsManager.instance.RefreshUserLevels();

            levelData.nodesToSave.Clear();
            activeNodes.Clear();

            Debug.Log("Level Saved by User at " + path);
        }

        public void UserSavingCurrentLevel()
        {
            levelData.levelName = currentOpenLevelName;
            levelData.Kubicode = currentKubicode;

            UserSavingLevel(currentOpenLevelName);
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

            currentOpenLevelName = levelData.levelName;

            levelData.nodesToSave.Clear();
            activeNodes.Clear();
        }

        public void UserDeleteLevel(string levelName)
        {
            UserLevelFiles.DeleteUserLevel(levelName);
            LevelsManager.instance.RefreshUserLevels();
        }

        public void ExtractAndRebuildLevel(LevelEditorData recoveredData)
        {
            grid = _Grid.instance;

            // start by resetting the grid's nodes to their base states
            grid.ResetIndexGrid();

            foreach (Node recoveredNode in recoveredData.nodesToSave)
            {
                currentNode = recoveredNode;
                
                GameObject newCube = Instantiate(cubePrefab);

                _Grid.instance.placedObjects.Add(newCube);

                grid.kuboGrid[recoveredNode.nodeIndex - 1].cubeOnPosition = newCube;
                grid.kuboGrid[recoveredNode.nodeIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                grid.kuboGrid[recoveredNode.nodeIndex - 1].worldPosition = recoveredNode.worldPosition;
                grid.kuboGrid[recoveredNode.nodeIndex - 1].worldRotation = recoveredNode.worldRotation;
                grid.kuboGrid[recoveredNode.nodeIndex - 1].facingDirection = recoveredNode.facingDirection;

                switch (recoveredNode.cubeType)
                {
                    case CubeTypes.StaticCube:
                        newCube.AddComponent(typeof(StaticCube));
                        StaticCube staticCube = newCube.GetComponent<StaticCube>();
                        SetCubeInfo(staticCube as _CubeBase, CubeLayers.cubeFull, CubeTypes.StaticCube);
                        staticCube.staticEnum = StaticEnums.Empty;
                        break;

                    case CubeTypes.MoveableCube:
                        newCube.AddComponent(typeof(MoveableCube));
                        MoveableCube moveableCube = newCube.GetComponent<MoveableCube>();
                        SetCubeInfo(moveableCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.MoveableCube);
                        break;

                    case CubeTypes.BaseVictoryCube:
                        newCube.AddComponent(typeof(BaseVictoryCube));
                        BaseVictoryCube baseVictoryCube = newCube.GetComponent<BaseVictoryCube>();
                        SetCubeInfo(baseVictoryCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.BaseVictoryCube);
                        break;

                    case CubeTypes.ConcreteVictoryCube:
                        newCube.AddComponent(typeof(ConcreteVictoryCube));
                        ConcreteVictoryCube concreteVictoryCube = newCube.GetComponent<ConcreteVictoryCube>();
                        SetCubeInfo(concreteVictoryCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.ConcreteVictoryCube);
                        break;

                    case CubeTypes.BombVictoryCube:
                        newCube.AddComponent(typeof(BombVictoryCube));
                        BombVictoryCube bombVictoryCube = newCube.GetComponent<BombVictoryCube>();
                        SetCubeInfo(bombVictoryCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.BombVictoryCube);
                        break;

                    case CubeTypes.SwitchVictoryCube:
                        newCube.AddComponent(typeof(SwitchVictoryCube));
                        SwitchVictoryCube switchVictoryCube = newCube.GetComponent<SwitchVictoryCube>();
                        SetCubeInfo(switchVictoryCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.SwitchVictoryCube);
                        break;

                    case CubeTypes.DeliveryCube:
                        newCube.AddComponent(typeof(DeliveryCube));
                        DeliveryCube deliveryCube = newCube.GetComponent<DeliveryCube>();
                        SetCubeInfo(deliveryCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.DeliveryCube);
                        break;

                    case CubeTypes.ElevatorCube:
                        newCube.AddComponent(typeof(ElevatorCube));
                        ElevatorCube elevatorCube = newCube.GetComponent<ElevatorCube>();
                        SetCubeInfo(elevatorCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.ElevatorCube);
                        break;

                    case CubeTypes.ConcreteCube:
                        newCube.AddComponent(typeof(ConcreteCube));
                        ConcreteCube concreteCube = newCube.GetComponent<ConcreteCube>();
                        SetCubeInfo(concreteCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.ConcreteCube);
                        break;

                    case CubeTypes.BombCube:
                        newCube.AddComponent(typeof(BombCube));
                        BombCube bombCube = newCube.GetComponent<BombCube>();
                        SetCubeInfo(bombCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.BombCube);
                        break;

                    case CubeTypes.TimerCube:
                        newCube.AddComponent(typeof(TimerCube));
                        TimerCube timerCube = newCube.GetComponent<TimerCube>();
                        SetCubeInfo(timerCube as _CubeBase, CubeLayers.cubeFull, CubeTypes.TimerCube);
                        break;

                    case CubeTypes.SwitchButton:
                        newCube.AddComponent(typeof(SwitchButton));
                        SwitchButton switchButton = newCube.GetComponent<SwitchButton>();
                        SetCubeInfo(switchButton as _CubeBase, CubeLayers.cubeFull, CubeTypes.SwitchButton);
                        break;

                    case CubeTypes.SwitchCube:
                        newCube.AddComponent(typeof(SwitchCube));
                        SwitchCube switchCube = newCube.GetComponent<SwitchCube>();
                        SetCubeInfo(switchCube as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.SwitchCube);
                        break;

                    case CubeTypes.RotatorRightTurner:
                        newCube.AddComponent(typeof(RotateRightCube));
                        RotateRightCube rotatorRightTurn = newCube.GetComponent<RotateRightCube>();
                        SetCubeInfo(rotatorRightTurn as _CubeBase, CubeLayers.cubeFull, CubeTypes.RotatorRightTurner);
                        break;

                    case CubeTypes.RotatorLeftTurner:
                        newCube.AddComponent(typeof(RotateLeftCube));
                        RotateLeftCube rotatorLeftTurn = newCube.GetComponent<RotateLeftCube>();
                        SetCubeInfo(rotatorLeftTurn as _CubeBase, CubeLayers.cubeFull, CubeTypes.RotatorLeftTurner);
                        break;

                    case CubeTypes.RotatorLocker:
                        newCube.AddComponent(typeof(RotatorLocker));
                        RotatorLocker rotatorLocker = newCube.GetComponent<RotatorLocker>();
                        SetCubeInfo(rotatorLocker as _CubeBase, CubeLayers.cubeFull, CubeTypes.RotatorLocker);
                        break;

                    case CubeTypes.ChaosBall:
                        newCube.AddComponent(typeof(ChaosBall));
                        ChaosBall chaosBall = newCube.GetComponent<ChaosBall>();
                        SetCubeInfo(chaosBall as _CubeBase, CubeLayers.cubeMoveable, CubeTypes.ChaosBall);
                        break;

                    default:
                        //set epmty cubes as cubeEmpty
                        grid.kuboGrid[recoveredNode.nodeIndex - 1].cubeOnPosition = null;
                        grid.kuboGrid[recoveredNode.nodeIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
                        grid.kuboGrid[recoveredNode.nodeIndex - 1].cubeType = CubeTypes.None;
                        break;
                }
            }

            Debug.Log("Level Loaded !");
        }

        void SetCubeInfo(_CubeBase cube, CubeLayers cubeLayers, CubeTypes cubeTypes)
        {
            cube.myIndex = currentNode.nodeIndex;
            cube.facingDirection = currentNode.facingDirection;
            grid.kuboGrid[cube.myIndex - 1].cubeLayers = cubeLayers;
            grid.kuboGrid[cube.myIndex - 1].cubeType = cubeTypes;
            cube.OnLoadSetTransform();
        }
    }
}
