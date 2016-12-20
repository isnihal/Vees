﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    static int showRewardedAdAfter=6, showNoRewardAdAfter=3,numberOfGames=0,numberOfLapseGames=0;
    
    //Set scoreboard for gameOver level
    
	// Use this for initialization
	void Start () {
        
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
}