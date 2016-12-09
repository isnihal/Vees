using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using Google;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
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
    9)Pause game
   */


    public Text scoreBoard,timerText;
    public GameObject[] lifeArray;
    static float enemySpawnFrequency;
    static int score,life;
    //Time Lapse property
    float timeLeft;
    string language,defaultLanguage;

    public GameObject enemySpawner, playerSpawner, bombSpawner;
    public GameObject[] goalDetector;

    static bool isPaused,hasRestarted;


    //Show AD button and No
    public GameObject showAdButton, noButton,pauseButton;

    //Achievement Keys
    static string ONE_DIRECTION_NOOB = "CgkIu73IgfAIEAIQBA";
    static string ARCADE_NOOB = "CgkIu73IgfAIEAIQAw";
    static string FAST_ESCAPE_NOOB = "CgkIu73IgfAIEAIQBQ";
    static string EQUALS_NOOB = "CgkIu73IgfAIEAIQBg";
    static string TIME_LAPSE_NOOB = "CgkIu73IgfAIEAIQBw";

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

        //Some kind of bug to fix
        defaultLanguage = "ENGLISH";
        language = PlayerPrefsManager.getLanguage();
        if (language == "")
        {
            language = defaultLanguage;
            PlayerPrefsManager.setLanguage(language);
        }

        isPaused = false;
        hasRestarted = false;

        checkForAchievements();
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
        if(life<=0 && life!=-99)//-99 as a flag
        {
            if (!hasRestarted)//Play more by viewing ad (Only once)
            {
                //Show the UI Buttons
                pauseGame();
                pauseButton.active = false;
                showAdButton.active = true;
                noButton.active = true;
                life = -99;//To avoid a bug
            }
            else
            {
                SceneManager.LoadScene("GAME_OVER");
            }
        }
    }

    void setScoreBoardDisplay()
    {
        if (GameManager.getLevelName() == "FAST_ESCAPE")
        {
            switch (language)
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
            switch(language)
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
        if (GameManager.getLevelName() != "ARCADE" && GameManager.getLevelName()!="TIME_LAPSE")
        {
            for (int i = 0; i < lifeArray.Length; i++)
            {
                if (i < life)
                {
                    lifeArray[i].active = true;
                }
                else
                {
                    lifeArray[i].active = false;
                }
            }
        }
    }

    void setTimer()
    {
        if (!isGamePaused() && timeLeft<= 1000)
        {
            timeLeft -= Time.deltaTime;
            int timeInSeconds = Mathf.RoundToInt(timeLeft);
            timerText.text = "TIME:00:" + timeInSeconds.ToString("00");
        }

        if(timeLeft<=0)
        {
            life = 0;
            timeLeft = 10000000;
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

    public void pauseGame()
    {
        if(GameManager.getLevelName()=="EQUALS")
        {
            EnemyFormation enemiesOnScreen = FindObjectOfType<EnemyFormation>();
            if(enemiesOnScreen!=null)
            {
              
            }
        }
        if (!isGamePaused())
        {
            isPaused = true;
            playerSpawner.active = false;
            enemySpawner.active = false;
            if(goalDetector!=null)
            {
                for (int i = 0; i < goalDetector.Length; i++)
                {
                    goalDetector[i].active = false;
                }
            }
            if (getLevelName() == "ARCADE")
            {
                bombSpawner.active = false;
            }
        }
        else if (isGamePaused())
        {
            isPaused = false;
            playerSpawner.active = true;
            enemySpawner.active = true;
            if (goalDetector != null)
            {
                for (int i = 0; i < goalDetector.Length; i++)
                {
                    goalDetector[i].active = true;
                }
            }
            if (getLevelName() == "ARCADE")
            {
                bombSpawner.active = true;
            }
        }
    }

    public static bool isGamePaused()
    {
        return isPaused;
    }

    void checkForAchievements()
    {
        if (Application.loadedLevel == 3)
        {
            Social.ReportProgress(ONE_DIRECTION_NOOB, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("One Direction noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });
        }

        else if (Application.loadedLevel == 4)
        {
            Social.ReportProgress(ARCADE_NOOB, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("ARCADE noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });
        }

        else if (Application.loadedLevel == 5)
        {
            Social.ReportProgress(FAST_ESCAPE_NOOB, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("Fast escape noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });
        }

        else if (Application.loadedLevel == 6)
        {
            Social.ReportProgress(EQUALS_NOOB, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("Equals noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });
        }

        else if (Application.loadedLevel == 7)
        {
            Social.ReportProgress(TIME_LAPSE_NOOB, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("Time lapse noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });
        }
    }

    public void continueGameWithAd()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = handleAdResult });
            hasRestarted = true;
        }
        
    }

    void restartGame()
    {
        pauseGame();
        pauseButton.active = true;
        showAdButton.active = false;
        noButton.active = false; 
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

        else if(Application.loadedLevel==10)
        {
            return ("TUTORIAL");
        }
        return null;
    }

    public static bool hasGameBeenRestarted()
    {
        return hasRestarted;
    }

    void handleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                restartGame();
                life = 4;
                if (GameManager.getLevelName() == "TIME_LAPSE")
                {
                    timeLeft = 20f;
                }
                break;
            case ShowResult.Skipped:
                restartGame();
                life = 1;
                if (GameManager.getLevelName() == "TIME_LAPSE")
                {
                    timeLeft = 10f;
                }
                break;
            case ShowResult.Failed:
                SceneManager.LoadScene("GAME_OVER");
                break;
        }
    }
}
