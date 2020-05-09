using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kubika.Game
{

    public static class _DirectionCustom 
    {
        public static int rotationState { get; set; }
        public static int matrixLengthDirection { get; set; }

        public static int forward => rotationState == 0 ? (matrixLengthDirection * matrixLengthDirection) :
                                        (rotationState == 1 ? 1 :
                                        (rotationState == 2 ? -matrixLengthDirection : 0));
        public static int backward => -forward;
        public static int up => rotationState == 0 ? 1 :
                                        (rotationState == 1 ? matrixLengthDirection :
                                        (rotationState == 2 ? (matrixLengthDirection * matrixLengthDirection) : 0));
        public static int down => -up;
        public static int right => rotationState == 0 ? matrixLengthDirection :
                                        (rotationState == 1 ? -(matrixLengthDirection * matrixLengthDirection) :
                                        (rotationState == 2 ? -1 : 0));
        public static int left => -right;


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
