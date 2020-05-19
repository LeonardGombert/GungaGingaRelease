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

        [Space]
        [Header("CamLimit")]
        public Transform pivotVCam;
        public float VectorXLimitStart;
        public float VectorXLimitEnd;

        [Space]
        public bool isActive = false;

        public void ActivatePSFB()
        {
            PS.Play();
        }

        void Update()
        {
            if(isActive == true)
            {

            }
        }
    }
}
