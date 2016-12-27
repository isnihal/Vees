using UnityEngine;
using UnityEngine.UI;
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
    static int score,life,lastLifeScore;
    //Time Lapse property
    float timeLeft;
    string language,defaultLanguage;

    public GameObject enemySpawner, playerSpawner, bombSpawner;
    public GameObject[] goalDetector;

    static bool isPaused,hasRestarted,lastLife;


    //Show AD button and No
    public GameObject gameOverPanel,pauseMenuPanel,pauseButton;

    public Sprite pauseImage, resumeImage;

    ToastManager toastManager;

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
        toastManager = FindObjectOfType<ToastManager>();
        lastLifeScore = 0;
        lastLife = false;
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

        if(life==1)
        {
            lastLife = true;
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
                if (!VolumeManager.getIsMuted())
                {
                    MusicPlayer.setVolume(0f);
                }
                pauseButton.active = false;
                gameOverPanel.active = true;
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
                        scoreBoard.text = "HITS:" + score;
                        break;
                case "CHINEESE":
                    scoreBoard.text = "杀死:" + score;
                    break;
            }
        }
    }

    void setLifeDisplay()
    {
        if (GameManager.getLevelName()=="FAST_ESCAPE" || GameManager.getLevelName()=="ONE_DIRECTION")
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
        if(lastLife)
        {
            lastLifeScore++;
        }
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
                EnemySpawner.decreaseSpawnedCount();
            }
        }
        if (!isGamePaused())
        {
            isPaused = true;
            pauseButton.GetComponent<Image>().sprite = resumeImage;
            if (life > 0)
            {
                pauseMenuPanel.active = true;
            }
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
            pauseButton.GetComponent<Image>().sprite = pauseImage;
            if (life > 0)
            {
                pauseMenuPanel.active = false;
            }
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
        if (getLevelName()=="ONE_DIRECTION")
        {
            Social.ReportProgress(GPGSIds.achievement_one_way_noob, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("One Direction noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });

            if (score == 200)
            {
                Social.ReportProgress(GPGSIds.achievement_one_way_pro, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (score == 500)
            {
                Social.ReportProgress(GPGSIds.achievement_one_way_master, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (score == 1000)
            {
                Social.ReportProgress(GPGSIds.achievement_one_way_legend, 100, (bool sucess) =>
                {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (lastLife && lastLifeScore==100)
            {
                Social.ReportProgress(GPGSIds.achievement_one_way_survivor, 100, (bool sucess) =>
                {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

        }

        else if (getLevelName()=="ARCADE")
        {
            Social.ReportProgress(GPGSIds.achievement_boom_noob, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("ARCADE noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });

            if (score == 200)
            {
                Social.ReportProgress(GPGSIds.achievement_bomb_diffuser, 100, (bool sucess) =>
                {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

        }

        else if (getLevelName()=="FAST_ESCAPE")
        {
            Social.ReportProgress(GPGSIds.achievement_escape_noob, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("Fast escape noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });

            if (score == 20)
            {
                Social.ReportProgress(GPGSIds.achievement_escape_pro, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (score == 50)
            {
                Social.ReportProgress(GPGSIds.achievement_escape_master, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (score == 100)
            {
                Social.ReportProgress(GPGSIds.achievement_escape_legend, 100, (bool sucess) =>
                {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if(lastLife && lastLifeScore==20)
            {
                Social.ReportProgress(GPGSIds.achievement_escape_survivor, 100, (bool sucess) =>
                {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

        }

        else if (getLevelName()=="EQUALS")
        {
            Social.ReportProgress(GPGSIds.achievement_equals_noob, 100, (bool sucess) => {
                if (sucess)
                {
                    Debug.Log("Equals noob unlocked");
                }
                else
                {
                    Debug.Log("Achievement failed");
                }
            });

            if(EnemySpawner.getWaveNumber()==20)
            {
                Social.ReportProgress(GPGSIds.achievement_equals_pro, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (EnemySpawner.getWaveNumber() == 50)
            {
                Social.ReportProgress(GPGSIds.achievement_equals_master, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement Success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }

            if (EnemySpawner.getWaveNumber() == 100)
            {
                Social.ReportProgress(GPGSIds.achievement_equals_legend, 100, (bool sucess) => {
                    if (sucess)
                    {
                        Debug.Log("Achievement success");
                    }
                    else
                    {
                        Debug.Log("Achievement failed");
                    }
                });
            }
        }

        else if (getLevelName()=="TIME_LAPSE")
        {
            Social.ReportProgress(GPGSIds.achievement_lapse_noob, 100, (bool sucess) => {
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
        Debug.Log("Has user Purchased:" + IAPManager.hasUserPurchasedVees());
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
    }

    void restartGame()
    {
        pauseGame();
        ElectricGun.setMaximumCharge();
        if (!VolumeManager.getIsMuted())
        {
            MusicPlayer.setVolume(0.5f);
        }
        pauseButton.active = true;
        gameOverPanel.active = false;
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
