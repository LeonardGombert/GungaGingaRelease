﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public class _StaticCube : CubeBase
    {
        // Start is called before the first frame update
        new void Start()
        {
            myCubeType = CubeTypes.StaticCube;
            myCubeLayer = CubeLayers.cubeFull;

            isStatic = true;

            base.Start();
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
        }
    }

}