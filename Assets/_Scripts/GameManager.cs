using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static float enemySpawnFrequency;
    static float arcadeFrequency, oneDirectionFrequency, fastEscapeFrequency;

    void Start()
    {
    }

    public static string getLevelName()
    {
        if(Application.loadedLevel==3)
        {
            return ("ONE_DIRECTION");
        }

        else if(Application.loadedLevel==4)
        {
            return ("ARCADE");
        }

        else if(Application.loadedLevel==5)
        {
            return ("FAST_ESCAPE");
        }

        return null;
    }
}
