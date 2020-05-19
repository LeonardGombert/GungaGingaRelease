using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Kubika.Game
{
    public class _PlaneteCamera : MonoBehaviour
    {
        public CinemachineVirtualCamera vCam;
        public int index;
        public ParticleSystem PS;

        public void ActivatePSFB()
        {
            PS.Play();
        }
    }
}
