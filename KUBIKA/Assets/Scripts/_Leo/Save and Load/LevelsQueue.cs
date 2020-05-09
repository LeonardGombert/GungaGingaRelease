using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsQueue : MonoBehaviour
{
    public List<LevelFileInfo> listOfLevels;
    public Queue<LevelFileInfo> levels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class LevelFileInfo
{
    public TextAsset level;
}


