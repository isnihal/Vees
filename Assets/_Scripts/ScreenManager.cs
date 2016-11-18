using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    static Vector3 leftBounday, rightBoundary,topBoundary,bottomBoundary;
    static float leftX, rightX,topY,bottomY,padding;

    // Use this for initialization
    void Start () {
        leftBounday = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        topBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        bottomBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        padding = 0.5f;
        leftX = leftBounday.x + padding;
        rightX = rightBoundary.x - padding;
        topY = topBoundary.y - padding;
        bottomY = bottomBoundary.y + padding;

    }

    public static float getLeftBoundary()
    {
        return leftX;
    }

    public static float getRightBoundary()
    {
        return rightX;
    }

    public static float getTopBoundary()
    {
        return topY;
    }

    public static float getBottomBoundary()
    {
        return bottomY;
    }
}
