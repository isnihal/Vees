using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    static Vector3 startPositon, endPosition;
    static float startTime, endTime,xVelocity;

    void DragStart()
    {
        startPositon = Input.mousePosition;
        startTime = Time.time;
    }

    void DragEnd()
    {
        endPosition = Input.mousePosition;
        endTime = Time.time;

        xVelocity = (endPosition.x - startPositon.x) / (endTime - startTime);
    }

    public static float getXvelocity()
    {
        return xVelocity;
    }

    public static float getYpos()
    {
        return (startPositon.y);
    }
}
