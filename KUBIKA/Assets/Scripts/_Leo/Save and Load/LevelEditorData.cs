using Kubika.CustomLevelEditor;
using System.Collections.Generic;

namespace Kubika.Saving
{
    [System.Serializable]
    public class LevelEditorData
    {
        public string levelName;
        public int minimumMoves;
        public List<Node> nodesToSave;
    }
}
