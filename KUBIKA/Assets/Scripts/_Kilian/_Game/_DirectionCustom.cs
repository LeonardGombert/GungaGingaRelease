using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{
    public static class _DirectionCustom 
    {
        public static int rotationState;// { get; set; }

        public static int matrixLengthDirection;

        /// WORLD MODE
        public static int forward => (matrixLengthDirection * matrixLengthDirection);
        
        public static int backward => -forward;

        public static int up => 1;
        public static int down => -up;

        public static int right => matrixLengthDirection;
        public static int left => -right;



        /// LOCAL MODE
        public static int fixedForward => rotationState == 0 ? (matrixLengthDirection * matrixLengthDirection) :
                                (rotationState == 1 ? 1 :
                                (rotationState == 2 ? -matrixLengthDirection : 0));
        public static int fixedBackward => -forward;
        public static int fixedUp => rotationState == 0 ? 1 :
                                        (rotationState == 1 ? matrixLengthDirection :
                                        (rotationState == 2 ? (matrixLengthDirection * matrixLengthDirection) : 0));
        public static int fixedDown => -up;
        public static int fixedRight => rotationState == 0 ? matrixLengthDirection :
                                        (rotationState == 1 ? -(matrixLengthDirection * matrixLengthDirection) :
                                        (rotationState == 2 ? -1 : 0));
        public static int fixedLeft => -right;

        public static int ScannerSet(Vector3 localDirection, Transform transform)
        {
            if (localDirection == transform.TransformDirection(Vector3.up))
            {
                return 1;
            }
            else if (localDirection == transform.TransformDirection(Vector3.down))
            {
                return 2;
            }
            else if (localDirection == transform.TransformDirection(Vector3.forward))
            {
                return 4; // FIX DE MERDE
            }
            else if (localDirection == transform.TransformDirection(Vector3.back))
            {
                return 3; // FIX DE MERDE
            }
            else if (localDirection == transform.TransformDirection(Vector3.right))
            {
                return 6; // FIX DE MERDE
            }
            else if (localDirection == transform.TransformDirection(Vector3.left))
            {
                return 5; // FIX DE MERDE
            }
            else
            {
                return 69;
            }
        }

        public static int LocalScanner(int localDirection)
        {
            if (localDirection == 1)
            {
                return fixedUp;
            }
            else if (localDirection == 2)
            {
                return fixedDown;
            }
            else if (localDirection == 3)
            {
                return fixedForward;
            }
            else if (localDirection == 4)
            {
                return fixedBackward;
            }
            else if (localDirection == 5)
            {
                return fixedRight;
            }
            else if (localDirection == 6)
            {
                return fixedLeft;
            }
            else
            {
                return 69;
            }
        }

        /// LOCAL VECTOR
        public static Vector3 vectorForward => rotationState == 0 ? Vector3.forward :
                                (rotationState == 1 ? Vector3.up :
                                (rotationState == 2 ? Vector3.right : Vector3.zero));

        public static Vector3 vectorBack => -vectorForward;
        public static Vector3 vectorLeft => rotationState == 0 ? Vector3.left :
                                        (rotationState == 1 ? Vector3.back :
                                        (rotationState == 2 ? Vector3.up : Vector3.zero));
        public static Vector3 vectorRight => -vectorLeft;
        public static Vector3 vectorUp => rotationState == 0 ? Vector3.up :
                                        (rotationState == 1 ? Vector3.right :
                                        (rotationState == 2 ? Vector3.forward : Vector3.zero));
        public static Vector3 vectorDown => -vectorUp;
        


    }
}
