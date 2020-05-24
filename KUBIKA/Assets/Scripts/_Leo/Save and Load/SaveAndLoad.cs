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

        LevelEditorData levelData;

        public GameObject cubePrefab;

        public string currentOpenLevelName;
        public string currentKubicode;
        public bool currentLevelLockRotate;
        public int currentMinimumMoves;

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
            // start by resetting the grid's nodes to their base states
            _Grid.instance.ResetIndexGrid();

            foreach (Node recoveredNode in recoveredData.nodesToSave)
            {
                GameObject newCube = Instantiate(cubePrefab);

                _Grid.instance.placedObjects.Add(newCube);

                Debug.Log(recoveredNode.cubeType);
                Debug.Log(recoveredNode.worldRotation);

                newCube.transform.position = _Grid.instance.kuboGrid[recoveredNode.nodeIndex - 1].worldPosition;
                newCube.transform.rotation = Quaternion.Euler(recoveredNode.worldRotation);
                newCube.transform.parent = _Grid.instance.gameObject.transform;

                switch (recoveredNode.cubeType)
                {
                    case CubeTypes.StaticCube:
                        newCube.AddComponent(typeof(StaticCube));
                        StaticCube staticCube = newCube.GetComponent<StaticCube>();

                        //set the cube's index so that you can assign its other variables (position, leyer, type, etc.)
                        staticCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].cubeType = CubeTypes.StaticCube;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[staticCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;

                        staticCube.staticEnum = StaticEnums.Empty;
                        break;

                    case CubeTypes.MoveableCube:
                        newCube.AddComponent(typeof(MoveableCube));
                        MoveableCube moveableCube = newCube.GetComponent<MoveableCube>();

                        moveableCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].cubeType = CubeTypes.MoveableCube;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[moveableCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.BaseVictoryCube:
                        newCube.AddComponent(typeof(BaseVictoryCube));
                        BaseVictoryCube victoryCube = newCube.GetComponent<BaseVictoryCube>();

                        victoryCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].cubeType = CubeTypes.BaseVictoryCube;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[victoryCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.ConcreteVictoryCube:
                        newCube.AddComponent(typeof(ConcreteVictoryCube));
                        ConcreteVictoryCube concreteVictoryCube = newCube.GetComponent<ConcreteVictoryCube>();

                        concreteVictoryCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].cubeType = CubeTypes.ConcreteVictoryCube;
                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[concreteVictoryCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.BombVictoryCube:
                        newCube.AddComponent(typeof(BombVictoryCube));
                        BombVictoryCube bombVictoryCube = newCube.GetComponent<BombVictoryCube>();

                        bombVictoryCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].cubeType = CubeTypes.BombVictoryCube;
                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[bombVictoryCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.SwitchVictoryCube:
                        newCube.AddComponent(typeof(SwitchVictoryCube));
                        SwitchVictoryCube switchVictoryCube = newCube.GetComponent<SwitchVictoryCube>();

                        switchVictoryCube.myIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].cubeType = CubeTypes.SwitchVictoryCube;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[switchVictoryCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.DeliveryCube:
                        newCube.AddComponent(typeof(DeliveryCube));
                        DeliveryCube deliveryCube = newCube.GetComponent<DeliveryCube>();

                        deliveryCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].cubeType = CubeTypes.DeliveryCube;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[deliveryCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.ElevatorCube:
                        newCube.AddComponent(typeof(ElevatorCube));
                        ElevatorCube elevatorCube = newCube.GetComponent<ElevatorCube>();

                        elevatorCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].cubeType = CubeTypes.ElevatorCube;
                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[elevatorCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.ConcreteCube:
                        newCube.AddComponent(typeof(ConcreteCube));
                        ConcreteCube concreteCube = newCube.GetComponent<ConcreteCube>();

                        concreteCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].cubeType = CubeTypes.ConcreteCube;
                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[concreteCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.BombCube:
                        newCube.AddComponent(typeof(BombCube));
                        BombCube bombCube = newCube.GetComponent<BombCube>();

                        bombCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].cubeType = CubeTypes.BombCube;
                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[bombCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.TimerCube:
                        newCube.AddComponent(typeof(TimerCube));
                        TimerCube timerCube = newCube.GetComponent<TimerCube>();

                        timerCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].cubeType = CubeTypes.TimerCube;
                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[timerCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.SwitchButton:
                        newCube.AddComponent(typeof(SwitchButton));
                        SwitchButton switchButton = newCube.GetComponent<SwitchButton>();

                        switchButton.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].cubeType = CubeTypes.SwitchButton;
                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[switchButton.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.SwitchCube:
                        newCube.AddComponent(typeof(SwitchCube));
                        SwitchCube switchCube = newCube.GetComponent<SwitchCube>();

                        switchCube.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].cubeType = CubeTypes.SwitchCube;
                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[switchCube.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.RotatorRightTurner:
                        newCube.AddComponent(typeof(RotateRightCube));
                        RotateRightCube rotatorRightTurn = newCube.GetComponent<RotateRightCube>();

                        rotatorRightTurn.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].cubeType = CubeTypes.RotatorRightTurner;
                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[rotatorRightTurn.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.RotatorLeftTurner:
                        newCube.AddComponent(typeof(RotateLeftCube));
                        RotateLeftCube rotatorLeftTurn = newCube.GetComponent<RotateLeftCube>();

                        rotatorLeftTurn.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].cubeType = CubeTypes.RotatorLeftTurner;
                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[rotatorLeftTurn.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.RotatorLocker:
                        newCube.AddComponent(typeof(RotatorLocker));
                        RotatorLocker rotatorLocker = newCube.GetComponent<RotatorLocker>();

                        rotatorLocker.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].cubeType = CubeTypes.RotatorLocker;
                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[rotatorLocker.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    case CubeTypes.ChaosBall:
                        newCube.AddComponent(typeof(ChaosBall));
                        ChaosBall chaosBall = newCube.GetComponent<ChaosBall>();

                        chaosBall.myIndex = recoveredNode.nodeIndex;

                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].cubeOnPosition = newCube;
                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].cubeType = CubeTypes.ChaosBall;
                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].worldPosition = recoveredNode.worldPosition;
                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].worldRotation = recoveredNode.worldRotation;
                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
                        _Grid.instance.kuboGrid[chaosBall.myIndex - 1].facingDirection = recoveredNode.facingDirection;
                        break;

                    default:
                        //set epmty cubes as cubeEmpty
                        _Grid.instance.kuboGrid[recoveredNode.nodeIndex - 1].cubeLayers = CubeLayers.cubeEmpty;
                        _Grid.instance.kuboGrid[recoveredNode.nodeIndex - 1].cubeType = CubeTypes.None;
                        break;
                }
            }

            Debug.Log("Level Loaded !");
        }

        /*static void Foo<T>() where T : <_CubeBase> (T x)
        {
            _Grid.instance.kuboGrid[T.myIndex - 1].cubeOnPosition = newCube;
            _Grid.instance.kuboGrid[T.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
            _Grid.instance.kuboGrid[T.myIndex - 1].cubeType = CubeTypes.ElevatorCube;
            _Grid.instance.kuboGrid[T.myIndex - 1].worldPosition = recoveredNode.worldPosition;
            _Grid.instance.kuboGrid[T.myIndex - 1].worldRotation = recoveredNode.worldRotation;
            _Grid.instance.kuboGrid[T.myIndex - 1].nodeIndex = recoveredNode.nodeIndex;
        }*/
    }
}
