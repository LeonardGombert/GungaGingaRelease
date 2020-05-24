using Kubika.CustomLevelEditor;
using System.Collections.Generic;

namespace Kubika.Saving
{
    [System.Serializable]
    public class LevelEditorData
    {
        public string levelName;
        public Biomes levelBiome;
        public int minimumMoves;
        public bool lockRotate;
        public string Kubicode;
        public List<Node> nodesToSave;
    }

    [System.Serializable]
    public class UserLevels
    {
        public int numberOfUserLevels;
        public List<string> levelNames  = new List<string>();
    }
}
