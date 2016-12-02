using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    //Set scoreboard for gameOver level

	// Use this for initialization
	void Start () {
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
            switch (PlayerPrefsManager.getLanguage())
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
