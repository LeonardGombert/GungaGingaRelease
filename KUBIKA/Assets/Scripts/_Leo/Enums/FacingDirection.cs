using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeFacingDirection : MonoBehaviour
{
    public static Vector3 CubeFacing(FacingDirection facingDirection)
    {
        Vector3 vectorResult = Vector3.up;

        switch (facingDirection)
        {
            case FacingDirection.up:
                vectorResult = new Vector3(0, 0, 0);
                break;
            case FacingDirection.down:
                vectorResult = new Vector3(180, 0, 0);
                break;
            case FacingDirection.forward:
                vectorResult = new Vector3(270, 0, 0);
                break;
            case FacingDirection.backward:
                vectorResult = new Vector3(90, 0, 0);
                break;
            case FacingDirection.right:
                vectorResult = new Vector3(0, 0, 270);
                break;
            case FacingDirection.left:
                vectorResult = new Vector3(0, 0, 90);
                break;

            default:
                break;
        }

        Debug.Log(vectorResult);
        return vectorResult;
    }
}
public enum FacingDirection
{
    up,
    down,

    forward,
    backward,

    right,
    left,
}