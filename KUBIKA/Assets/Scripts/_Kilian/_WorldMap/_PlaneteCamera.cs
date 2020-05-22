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
        Vector3 currentPivotPosition;
        public Transform LimitStart;
        public Transform LimitEnd;

        [Space]
        public bool isActive = false;
        public float ScrollPower = 0.1f;
        public float DEBUG_ACTUAL_SCROOL;

        // TOUCH
        Touch touch;
        Ray ray;
        RaycastHit hit;
        bool hasTouchedLevel = false;

        public void ActivatePSFB()
        {
            PS.Play();
        }

        void Update()
        {
            if(isActive == true)
            {
                touch = Input.GetTouch(0);
                ray = Camera.main.ScreenPointToRay(touch.position);


                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log("Touch Hit " + hit.collider.gameObject.name);

                            if (hit.collider.gameObject.GetComponent<_ScriptMatFaceCube>())
                            {
                                Debug.Log("ClickedOnLevel");
                                hasTouchedLevel = true;
                            }
                        }
                        break;
                    case TouchPhase.Moved:
                        if (hasTouchedLevel == false)
                        {
                            ScrollingSimple(touch.deltaPosition.y);
                        }
                        break;
                    case TouchPhase.Ended:
                        break;
                }
            }
        }

        void ScrollingSimple(float YPosition)
        {
            currentPivotPosition = pivotVCam.transform.localPosition;
            DEBUG_ACTUAL_SCROOL = (YPosition * 0.01f) * ScrollPower;
            currentPivotPosition = Vector3.Lerp(LimitStart.localPosition, LimitEnd.localPosition, DEBUG_ACTUAL_SCROOL);
            pivotVCam.transform.localPosition = currentPivotPosition;
        }
    }
}
