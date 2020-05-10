﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kubika.LevelEditor;

namespace Kubika.Game
{
    [CreateAssetMenu(fileName = "DataBank", menuName = "DataBank", order = 1)]
    public class _DataMatrixScriptable : ScriptableObject
    {
        public Node[] indexBank;
    }
}
