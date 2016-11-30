﻿using UnityEngine;
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


    public Text scoreBoard,timerText;
    public Image[] lifeArray;
    static float enemySpawnFrequency;
    static int score,life;

    //Time Lapse property
    float timeLeft;

    void Start()
    {
        if (getLevelName() != "GAME_OVER")//Properties of equals set in enemy spawner
        {
            resetScore();
            resetLife();
            setLevelProperties();
            //Only for time lapse
            timeLeft = 60f;
        }
        else if(getLevelName() =="GAME_OVER")
        {
            scoreBoard = FindObjectOfType<ScoreBoard>().GetComponent<Text>();
        }
    }

    void Update()
    {
        setScoreBoardDisplay();
        setLifeDisplay();
        isGameOver();
        if(GameManager.getLevelName()=="TIME_LAPSE")
        {
            setTimer();
        }
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

    void setScoreBoardDisplay()
    {
        if (GameManager.getLevelName() == "FAST_ESCAPE")
        {
            switch (PlayerPrefsManager.getLanguage())
            {
                case "ENGLISH":
                    scoreBoard.text = "ESCAPES:" + score;
                    break;
                case "CHINEESE":
                    scoreBoard.text = "逃脱:" + score;
                    break;
            }
            
        }
        else
        {
            switch(PlayerPrefsManager.getLanguage())
            {
                case "ENGLISH":
                        scoreBoard.text = "KILLS:" + score;
                        break;
                case "CHINEESE":
                    scoreBoard.text = "杀死:" + score;
                    break;
            }
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

    void setTimer()
    {
        timeLeft -= Time.deltaTime;
        int timeInSeconds = Mathf.RoundToInt(timeLeft);
        timerText.text = "TIME:00:" + timeInSeconds.ToString("00");
        if(timeLeft<=0)
        {
            Application.LoadLevel("GAME_OVER");
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
