using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    static int showRewardedAdAfter=6, showNoRewardAdAfter=3,numberOfGames=0;

    //Set scoreboard for gameOver level

	// Use this for initialization
	void Start () {
        numberOfGames++;
        //Ad Script
        if(numberOfGames%showRewardedAdAfter==0)
        {
            AdManager.showRewardedAd();
        }
        else if(numberOfGames%showNoRewardAdAfter==0)
        {
            AdManager.showNoRewardedAd();
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
                    gameObject.GetComponent<Text>().text = "Wave\n" + EnemySpawner.getWaveNumber().ToString();
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
                    gameObject.GetComponent<Text>().text = "Score\n" + GameManager.getScore().ToString();
                    break;
                case "CHINEESE":
                    gameObject.GetComponent<Text>().text = "得分了\n" + GameManager.getScore().ToString();
                    break;
            }
           
        }
	}
	
}
