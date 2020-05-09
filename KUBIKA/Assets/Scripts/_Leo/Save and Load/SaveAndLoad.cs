using Kubika.Game;
using Kubika.LevelEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Kubika.Saving
{
    public class SaveAndLoad : MonoBehaviour
    {
        LevelEditor.Grid grid;

        //a list of the nodes in grid node that have cubes on them
        List<Node> activeNodes = new List<Node>();

        LevelEditorData levelData;

        // Start is called before the first frame update
        void Start()
        {
            grid = LevelEditor.Grid.instance;
            CreateEditorData();
        }

        private LevelEditorData CreateEditorData()
        {
            levelData = new LevelEditorData();
            levelData.nodesToSave = new List<Node>();
            return levelData;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SaveLevel(string levelName)
        {
            for (int i = 0; i < grid.kuboGrid.Length; i++)
            {
                if (grid.kuboGrid[i].cubeOnPosition != null) activeNodes.Add(grid.kuboGrid[i]);
            }

            foreach (Node node in activeNodes) levelData.nodesToSave.Add(node);

            string json = JsonUtility.ToJson(levelData);
            string folder = Application.dataPath + "/Scenes/Levels";
            string levelFile = "";

            if (levelName != "") levelFile = levelName + ".json";
            else levelFile = "New_Level.json";

            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            string path = Path.Combine(folder, levelFile);

            if (File.Exists(path)) File.Delete(path);

            File.WriteAllText(path, json);

            levelData.nodesToSave.Clear();
            activeNodes.Clear();

            Debug.Log("Level Saved !");
        }

        public void LoadLevel(string levelName)
        {
            Debug.Log("Level Loading !");
                
            string folder = Application.dataPath + "/Scenes/Levels";
            string levelFile = levelName + ".json";

            string path = Path.Combine(folder, levelFile);

            if(File.Exists(path))
            { 
                string json = File.ReadAllText(path);
                levelData = JsonUtility.FromJson<LevelEditorData>(json);

                ExtractAndRebuildLevel(levelData);
            }
        }

        private void ExtractAndRebuildLevel(LevelEditorData recoveredData)
        {
            foreach (Node recoveredNode in recoveredData.nodesToSave)
            {
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                
                Debug.Log(recoveredNode.cubeType);

                newCube.transform.position = recoveredNode.worldPosition;
                newCube.transform.parent = grid.gameObject.transform;

                switch (recoveredNode.cubeType)
                {
                    case CubeTypes.StaticCube:
                        newCube.AddComponent(typeof(CubeBase));
                        CubeBase staticCube = newCube.GetComponent<CubeBase>();

                        //set the cube's index so that you can assign its other variables (position, leyer, type, etc.)
                        staticCube.myIndex = recoveredNode.nodeIndex;

                        grid.kuboGrid[staticCube.myIndex - 1].cubeOnPosition = newCube;
                        grid.kuboGrid[staticCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        grid.kuboGrid[staticCube.myIndex - 1].cubeType = CubeTypes.StaticCube;
                        break;

                    case CubeTypes.MoveableCube:
                        newCube.AddComponent(typeof(_MoveableCube));
                        _MoveableCube moveableCube = newCube.GetComponent<_MoveableCube>();

                        moveableCube.myIndex = recoveredNode.nodeIndex;

                        grid.kuboGrid[moveableCube.myIndex - 1].cubeOnPosition = newCube;
                        grid.kuboGrid[moveableCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        grid.kuboGrid[moveableCube.myIndex - 1].cubeType = CubeTypes.MoveableCube;
                        break;

                    case CubeTypes.VictoryCube:
                        newCube.AddComponent(typeof(_VictoryCube));
                        _VictoryCube victoryCube = newCube.GetComponent<_VictoryCube>();

                        victoryCube.myIndex = recoveredNode.nodeIndex;

                        grid.kuboGrid[victoryCube.myIndex - 1].cubeOnPosition = newCube;
                        grid.kuboGrid[victoryCube.myIndex - 1].cubeLayers = CubeLayers.cubeMoveable;
                        grid.kuboGrid[victoryCube.myIndex - 1].cubeType = CubeTypes.VictoryCube;
                        break;

                    case CubeTypes.DeliveryCube:
                        newCube.AddComponent(typeof(_DeliveryCube));
                        _DeliveryCube deliveryCube = newCube.GetComponent<_DeliveryCube>();

                        deliveryCube.myIndex = recoveredNode.nodeIndex;

                        grid.kuboGrid[deliveryCube.myIndex - 1].cubeOnPosition = newCube;
                        grid.kuboGrid[deliveryCube.myIndex - 1].cubeLayers = CubeLayers.cubeFull;
                        grid.kuboGrid[deliveryCube.myIndex - 1].cubeType = CubeTypes.DeliveryCube;
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
                        break;
                }
            }

            Debug.Log("Level Loaded !");
        }
    }
}
