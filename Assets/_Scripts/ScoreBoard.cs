using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    static int showRewardedAdAfter=6, showNoRewardAdAfter=7,numberOfGames=0,numberOfLapseGames=0;

    const string oneWayLeaderBoardID = "CgkIiY779uUNEAIQEw";
    const string equalsLeaderBoardID = "CgkIiY779uUNEAIQFA";
    const string escapeLeaderBoardID = "CgkIiY779uUNEAIQFQ";
    const string lapseLeaderBoardID = "CgkIiY779uUNEAIQFg";
    const string boomLeaderBoardID = "CgkIiY779uUNEAIQFw";

    //Set scoreboard for gameOver level

    // Use this for initialization
    void Start () {
        postScoreToLeaderBoard();
        if (!Advertisement.IsReady())
        {
            Advertisement.Initialize("1235581");
        }
        if (GameManager.hasGameBeenRestarted())
        {
            numberOfGames = 0;
        }
        else
        {
            numberOfGames++;
        }

        //Ad Script
        if (!IAPManager.hasUserPurchasedVees())
        {
            if (numberOfGames % showNoRewardAdAfter == 0 && !GameManager.hasGameBeenRestarted() && Advertisement.IsReady())//Show ad only if user didnt view ad for restarting
            {
                AdManager.showNoRewardedAd();
            }
        }

        if (!VolumeManager.getIsMuted())
        {
            VolumeManager.setMusicPlayerOnIfSilent();
        }

        string language = PlayerPrefsManager.getLanguage();
        if(language=="")
        {
            language = "ENGLISH";
        }
        if (GoalDetector.fromEquals)
        {
            //Wave number for level EQUALS
            switch (PlayerPrefsManager.getLanguage())
            {
                case "ENGLISH":
                    gameObject.GetComponent<Text>().text = "WAVE\n" + EnemySpawner.getWaveNumber().ToString();
                    break;
                case "CHINEESE":
                    gameObject.GetComponent<Text>().text = "波\n" + EnemySpawner.getWaveNumber().ToString();
                    break;
            }
            GoalDetector.fromEquals = false;
        }
        else
        {
            switch (language)
            {
                case "ENGLISH":
                    if (LevelManager.getFromLevel() != 5)
                    {
                        gameObject.GetComponent<Text>().text = "HITS\n" + GameManager.getScore().ToString();
                    }
                    else
                    {
                        gameObject.GetComponent<Text>().text = "ESCAPES\n" + GameManager.getScore().ToString();
                    }
                    break;
                case "CHINEESE":
                    gameObject.GetComponent<Text>().text = "得分了\n" + GameManager.getScore().ToString();
                    break;
            }
           
        }
	}


    void checkForAchievements()
    {
        if (LevelManager.getFromLevel() == 7)
        {
            numberOfLapseGames++;
        }
        else
        {
            numberOfLapseGames = 0;
        }
        if (numberOfLapseGames == 10)
        {
            Social.ReportProgress(GPGSIds.achievement_lapse_love, 100, (bool sucess) =>
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

    public void postScoreToLeaderBoard()
    {
        switch (LevelManager.getFromLevel())
        {
            case 3:
                Social.ReportScore(GameManager.getScore(), oneWayLeaderBoardID, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success in posting score to leaderboard");
                    }
                    else
                    {
                        Debug.Log("Failure in posting score to leaderboard");
                    }
                });

                break;
            case 6:
                Social.ReportScore(EnemySpawner.getWaveNumber(), equalsLeaderBoardID, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success in posting score to leaderboard");
                    }
                    else
                    {
                        Debug.Log("Failure in posting score to leaderboard");
                    }
                });
                break;
            case 5:
                Social.ReportScore(GameManager.getScore(), escapeLeaderBoardID, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success in posting score to leaderboard");
                    }
                    else
                    {
                        Debug.Log("Failure in posting score to leaderboard");
                    }
                });
                break;
            case 7:
                Social.ReportScore(GameManager.getScore(), lapseLeaderBoardID, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success in posting score to leaderboard");
                    }
                    else
                    {
                        Debug.Log("Failure in posting score to leaderboard");
                    }
                });
                break;
            case 4:
                Social.ReportScore(GameManager.getScore(), boomLeaderBoardID, (bool success) => {
                    if (success)
                    {
                        Debug.Log("Success in posting score to leaderboard");
                    }
                    else
                    {
                        Debug.Log("Failure in posting score to leaderboard");
                    }
                });
                break;
        }
    }
}