using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour {

    static Vector3 leftBounday, rightBoundary;
    static float leftX, rightX, padding;

    // Use this for initialization
    void Start () {
        leftBounday = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        padding = 0.5f;
        leftX = leftBounday.x + padding;
        rightX = rightBoundary.x - padding;
    }

    public static float getLeftBoundary()
    {
        return leftX;
    }

    public static float getRightBoundary()
    {
        return rightX;
    }
}
