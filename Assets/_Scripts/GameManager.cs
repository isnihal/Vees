using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using System;
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


    public Text scoreBoard, timerText,highScoreText;
    public GameObject[] lifeArray;
    static float enemySpawnFrequency;
    static int score, life, lastLifeScore;
    //Time Lapse property
    float timeLeft;
    string language, defaultLanguage;

    public GameObject enemySpawner, playerSpawner, bombSpawner;
    public GameObject[] goalDetector;
    public AudioClip lifeGrantedMusic;

    static bool isPaused, hasRestarted, lastLife,delayTrigger;


    //Show AD button and No
    public GameObject gameOverPanel, pauseMenuPanel, pauseButton,headerPanel,gameOverScoreBoard;
    ToastManager toastManager;

    bool startNoResponseCounter;
    float noResponseTime;
    public Text responseTimer;

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
        else if (getLevelName() == "GAME_OVER")
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
        toastManager = FindObjectOfType<ToastManager>();
        lastLifeScore = 0;
        lastLife = false;
        delayTrigger = false;

        /*if (!Advertisement.IsReady())
        {
            Advertisement.Initialize("1215854");
        }*/

        startNoResponseCounter = false;
        noResponseTime = 7f;
    }

    void Update()
    {
        setScoreBoardDisplay();
        setLifeDisplay();
        checkForAchievements();
        isGameOver();
        if (GameManager.getLevelName() == "TIME_LAPSE")
        {
            setTimer();
        }

        if (life == 1)
        {
            lastLife = true;
        }
        else
        {
            lastLife = false;
        }

        setPerformanceBonus();
        setNoResponseCounter();
    }

    void setLevelProperties()
    {
        //Set life to all levels
        setLife(5);

        //Game Over properties
        if (getLevelName() == "GAME_OVER")
        {
            FindObjectOfType<ScoreBoard>().GetComponent<Text>().text = getScore().ToString();
        }
    }

    void isGameOver()
    {
        /*if (!Advertisement.IsReady())
        {
            Advertisement.Initialize("1215854");
        }*/

        if (life <= 0 && life != -99)//-99 as a flag
        {
            if (!hasRestarted && Advertisement.IsReady())//Play more by viewing ad (Only once)
            {
                //Show the UI Buttons
                pauseGame();
                setHighScoreDisplay();
                if (!VolumeManager.getIsMuted())
                {
                    MusicPlayer.setVolume(0f);
                }
                pauseButton.active = false;
                headerPanel.active = false;
                if (getLevelName() == "EQUALS")
                {
                    gameOverScoreBoard.GetComponent<Text>().text = EnemySpawner.getWaveNumber() + "";
                }
                else
                {
                    gameOverScoreBoard.GetComponent<Text>().text = score + "";
                }
                gameOverPanel.active = true;
                life = -99;//To avoid a bug
                startNoResponseCounter = true;
            }
            else
            {
                SceneManager.LoadScene("GAME_OVER");
            }
        }
    }

    void setNoResponseCounter()
    {
        if (startNoResponseCounter)
        {
            noResponseTime -= Time.deltaTime;
            int timeInSeconds = Mathf.RoundToInt(noResponseTime);
            responseTimer.text = timeInSeconds + "";
        }

        if (noResponseTime <= 0)
        {
            SceneManager.LoadScene("GAME_OVER");
        }
    }

    void setScoreBoardDisplay()
    {
        if (getLevelName() == "EQUALS")
        {
            scoreBoard.text = "HITS:" + score;   
        }
        else
        {
            scoreBoard.text = score + "";
        }
    }

    void setLifeDisplay()
    {
        if (GameManager.getLevelName() == "FAST_ESCAPE" || GameManager.getLevelName() == "ONE_DIRECTION")
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
        if (!isGamePaused() && timeLeft <= 1000)
        {
            timeLeft -= Time.deltaTime;
            int timeInSeconds = Mathf.RoundToInt(timeLeft);
            timerText.text = "00:" + timeInSeconds.ToString("00");
        }

        if (timeLeft <= 0)
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
        if (lastLife)
        {
            lastLifeScore++;
        }
    }

    public static void incrementScoreBy(int howMuch)
    {
        score += howMuch;
    }

    public static int getScore()
    {
        return score;
    }

    public static void decrementScoreBy(int number)
    {
        if (score - number >= 0)
        {
            score -= number;
        }
        else
        {
            score = 0;
        }
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

    public static void incrementLife()
    {
        life++;  
    }

    public static int getLife()
    {
        return life;
    }

    public void pauseGame()
    {
        if (GameManager.getLevelName() == "EQUALS")
        {
            EnemyFormation enemiesOnScreen = FindObjectOfType<EnemyFormation>();
            if (enemiesOnScreen != null)
            {
                EnemySpawner.decreaseSpawnedCount();
            }
        }
        if (!isGamePaused())
        {
            isPaused = true;
            MusicPlayer.setVolume(0);
            if (life > 0)
            {
                pauseMenuPanel.active = true;
            }
            pauseButton.active = false;
            playerSpawner.active = false;
            enemySpawner.active = false;
            if (goalDetector != null)
            {
                for (int i = 0; i < goalDetector.Length; i++)
                {
                    goalDetector[i].active = false;
                }
            }
            if (getLevelName() == "ARCADE" || getLevelName() == "TIME_LAPSE")
            {
                bombSpawner.active = false;
            }
        }
        else if (isGamePaused())
        {
            isPaused = false;
            if (!VolumeManager.getIsMuted())
            {
                MusicPlayer.setVolume(0.5f);
            }
            if (life > 0)
            {
                pauseMenuPanel.active = false;
            }
            pauseButton.active = true;
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
        if (Advertisement.IsReady())
        {
            if (getLevelName() == "ONE_DIRECTION")
            {
                Social.ReportProgress(GPGSIds.achievement_one_way_noob, 100, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });

                if (score >= 200)
                {
                    Social.ReportProgress(GPGSIds.achievement_one_way_pro, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (score >= 500)
                {
                    Social.ReportProgress(GPGSIds.achievement_one_way_master, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (score >= 1000)
                {
                    Social.ReportProgress(GPGSIds.achievement_one_way_legend, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (lastLife && lastLifeScore >= 100)
                {
                    Social.ReportProgress(GPGSIds.achievement_one_way_survivor, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

            }

            else if (getLevelName() == "FAST_ESCAPE")
            {
                Social.ReportProgress(GPGSIds.achievement_escape_noob, 100, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });

                if (score >= 20)
                {
                    Social.ReportProgress(GPGSIds.achievement_escape_pro, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (score >= 50)
                {
                    Social.ReportProgress(GPGSIds.achievement_escape_master, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (score >= 100)
                {
                    Social.ReportProgress(GPGSIds.achievement_escape_legend, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (lastLife && lastLifeScore >= 20)
                {
                    Social.ReportProgress(GPGSIds.achievement_escape_survivor, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

            }

            else if (getLevelName() == "EQUALS")
            {
                Social.ReportProgress(GPGSIds.achievement_equals_noob, 100, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });

                if (EnemySpawner.getWaveNumber() >= 20)
                {
                    Social.ReportProgress(GPGSIds.achievement_equals_pro, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (EnemySpawner.getWaveNumber() >= 50)
                {
                    Social.ReportProgress(GPGSIds.achievement_equals_master, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }

                if (EnemySpawner.getWaveNumber() >= 100)
                {
                    Social.ReportProgress(GPGSIds.achievement_equals_legend, 100, (bool sucess) =>
                    {
                        if (sucess)
                        {

                        }
                        else
                        {

                        }
                    });
                }
            }

            else if (getLevelName() == "TIME_LAPSE")
            {
                Social.ReportProgress(GPGSIds.achievement_lapse_noob, 100, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
            }
        }
    }

    public void continueGameWithAd()
    {
        
        //Unlock veeplay achievement
        Social.ReportProgress(GPGSIds.achievement_veeplay, 100, (bool sucess) => {
            if (sucess)
            {
              
            }
            else
            {
                
            }
        });
        if (!IAPManager.hasUserPurchasedVees())
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video", new ShowOptions() { resultCallback = handleAdResult });
                hasRestarted = true;
            }
            else
            {
                toastManager.showToastOnUiThread("Check Your Internet Connection");
            }
        }
        else
        {
            hasRestarted = true;
            Invoke("restartGame", 0.9f);
            life = 4;
            if (GameManager.getLevelName() == "TIME_LAPSE")
            {
                timeLeft = 20f;
            }
        }
    }

    void restartGame()
    {
        startNoResponseCounter = false;
        pauseGame();//Unpause in this context
        ElectricGun.setMaximumCharge();
        if (!VolumeManager.getIsMuted())
        {
            MusicPlayer.setVolume(0.5f);
        }
        pauseButton.active = true;
        headerPanel.active = true;
        gameOverPanel.active = false;
    }

    public static string getLevelName()
    {
        if (Application.loadedLevel == 3)
        {

            return ("ONE_DIRECTION");
        }

        else if (Application.loadedLevel == 4)
        {

            return ("HOW_TO");
        }

        else if (Application.loadedLevel == 5)
        {

            return ("FAST_ESCAPE");
        }

        else if (Application.loadedLevel == 6)
        {

            return ("EQUALS");
        }

        else if (Application.loadedLevel == 7)
        {

            return ("TIME_LAPSE");
        }

        else if (Application.loadedLevel == 8)
        {
            return ("GAME_OVER");
        }

        else if (Application.loadedLevel == 9)
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

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            isPaused = false;
            pauseGame();
        }
    }

    void setPerformanceBonus()
    {
        switch (getLevelName())
        {
            case "ONE_DIRECTION":
                if (score % 100 == 0 && !delayTrigger && score!=0)
                {
                    delayTrigger = true;
                    if (!VolumeManager.getIsMuted())
                    {
                        AudioSource.PlayClipAtPoint(lifeGrantedMusic, Vector3.zero);
                    }
                    if (life < 5)
                    {
                        incrementLife();
                    }
                }
                else if (score % 100 != 0)
                {
                    delayTrigger = false;
                }
                break;
            case "FAST_ESCAPE":
                if (score % 10 == 0 && !delayTrigger && score != 0)
                {
                    delayTrigger = true;
                    if (!VolumeManager.getIsMuted())
                    {
                        AudioSource.PlayClipAtPoint(lifeGrantedMusic, Vector3.zero);
                    }
                    if (life < 5)
                    {
                        incrementLife();
                    }
                }
                else if (score % 10 != 0)
                {
                    delayTrigger = false;
                }
                break;
        }
    }

    void setHighScoreDisplay()
    {
        switch(getLevelName())
        {
            case "ONE_DIRECTION":
                highScoreText.text=""+PlayerPrefsManager.getHighScore(3);
                break;
            case "FAST_ESCAPE":
                highScoreText.text = "" + PlayerPrefsManager.getHighScore(5);
                break;
            case "EQUALS":
                highScoreText.text = "" + PlayerPrefsManager.getHighScore(6);
                break;
            case "TIME_LAPSE":
                highScoreText.text = "" + PlayerPrefsManager.getHighScore(7);
                break;
        }
    }
}
