using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static float enemySpawnFrequency;
    static int score,life;

    void Start()
    {
        resetScore();
        resetLife();
        setLevelProperties();
    }

    void setLevelProperties()
    {
        //Fast escape properties
        if(getLevelName()=="FAST_ESCAPE")
        {
            setLife(5);
        }
    }

    public static void resetScore()
    {
        score = 0;
    }

    public static void incrementScore()
    {
        score++;
    }

    public static int getScore()
    {
        return score;
    }

    public static void resetLife()
    {
        life = 0;
    }

    public static void decrementLife()
    {
        life--;
    }

    public static void setLife(int number)
    {
        life = number;
    }

    public static int getLife()
    {
        return life;
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
