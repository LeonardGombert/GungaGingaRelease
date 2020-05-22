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

        // TOUCH
        Touch touch;
        Ray ray;
        RaycastHit hit;
        bool hasTouchedLevel = false;

        //SCROLL
        public float distance;
        protected Plane Plane;
        protected Vector3 BaseScroll;
        protected Vector3 CurrentScroll;

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
                            else
                            {
                                BaseScroll = hit.point;
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
                        hasTouchedLevel = false;
                        break;
                }
            }
        }

        void ScrollingSimple(float FingerPosition)
        {
            if (Physics.Raycast(ray, out hit))
            {
                CurrentScroll = hit.point;

                currentPivotPosition = pivotVCam.transform.position;

                distance = Vector3.Distance(LimitStart.position, LimitEnd.position);
                Debug.Log("Vector3.Distance(LimitStart.localPosition, hit.point) " + Vector3.Distance(LimitStart.position, hit.point) + " || T " + Vector3.Distance(LimitStart.position, hit.point) / distance);

                currentPivotPosition = Vector3.Lerp(LimitStart.position, LimitEnd.position, Vector3.Distance(LimitStart.position, hit.point) / distance);
                pivotVCam.transform.position = currentPivotPosition;
            }


        }

        protected Vector3 PlanePositionDelta(Touch touch)
        {
            //not moved
            if (touch.phase != TouchPhase.Moved)
                return Vector3.zero;

            //delta
            var rayBefore = _Planete.instance.MainCam.ScreenPointToRay(touch.position - touch.deltaPosition);
            var rayNow = _Planete.instance.MainCam.ScreenPointToRay(touch.position);
            if (Plane.Raycast(rayBefore, out var enterBefore) && Plane.Raycast(rayNow, out var enterNow))
                return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

            //not on plane
            return Vector3.zero;
        }
    }
}
