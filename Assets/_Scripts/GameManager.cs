using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static float enemySpawnFrequency;

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

        return null;
    }
}
