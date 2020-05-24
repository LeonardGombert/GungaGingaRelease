﻿using Kubika.CustomLevelEditor;
using System.Collections;
using UnityEngine;

namespace Kubika.Game
{
    public class MoveableCube : _CubeMove
    {
        public override void Start()
        {
            //call base.start AFTER assigning the cube's layers
            base.Start();

            isStatic = false;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}