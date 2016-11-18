using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    Text scoreBoard;
    public Image[] lifeArray;
    static float enemySpawnFrequency;
    static int score,life;

    void Start()
    {
        resetScore();
        resetLife();
        setLevelProperties();

        scoreBoard = FindObjectOfType<Text>();
    }

    void Update()
    {
        scoreBoard.text = score.ToString();
        setLifeDisplay();
        if(isGameOver())
        {
            Debug.Log("Game over");
        }
    }

    void setLevelProperties()
    {

        //Arcade properites
        if(getLevelName()=="ARCADE")
        {
            setLife(5);
        }
        
        //ONE DIRECTION properties
        if(getLevelName()=="ONE_DIRECTION")
        {
            setLife(5);
        }

        //Fast escape properties
        if(getLevelName()=="FAST_ESCAPE")
        {
            setLife(5);
        }
    }

    bool isGameOver()
    {
        if (life == 0)
            return true;
        return false;
    }

    void setLifeDisplay()
    {
        int count = 0;
        foreach(Image lifeImage in lifeArray)
        {
            count++;
            if(count==life+1)
            {
                Destroy(lifeImage);
            }
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
