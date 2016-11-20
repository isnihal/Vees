using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    //Most Critical script

   /*Game Manager Functions
    1)Score reseting,incrementing,fetching
    2)Life reseting,decrementing,fetching & Set life to an integer
    3)Set life to 5(for all levels),If life is zero gameOver gets triggered()
    4)Set score
    5)Manage gameOver Level,like setting score
    6)Check for game over
    7)Manage Life Display
    8)Get level name
   */


    public Text scoreBoard;
    public Image[] lifeArray;
    static float enemySpawnFrequency;
    static int score,life;

    void Start()
    {
        if (getLevelName() != "GAME_OVER")//Properties of equals set in enemy spawner
        {
            resetScore();
            resetLife();
            setLevelProperties();

            //scoreBoard = FindObjectOfType<Text>();
        }
        else if(getLevelName() =="GAME_OVER")
        {
            scoreBoard = FindObjectOfType<ScoreBoard>().GetComponent<Text>();
        }
    }

    void Update()
    {
        scoreBoard.text ="KILLS:"+score;
        setLifeDisplay();
        isGameOver();
    }

    void setLevelProperties()
    {
        //Set life to all levels
        setLife(5);

        //Game Over properties
        if(getLevelName()=="GAME_OVER")
        {
            FindObjectOfType<ScoreBoard>().GetComponent<Text>().text = getScore().ToString();
        }
    }

    void isGameOver()
    {
        if (life <= 0)
        {
            Application.LoadLevel("GAME_OVER");
        }
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

        else if(Application.loadedLevel==6)
        {
            return ("EQUALS");
        }

        else if(Application.loadedLevel==7)
        {
            return ("TIME_LAPSE");
        }

        else if(Application.loadedLevel==8)
        {
            return ("GAME_OVER");
        }
        return null;
    }
}
