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
            gameObject.GetComponent<Text>().text ="WAVE:"+EnemySpawner.getWaveNumber().ToString();
        }
        else
        {
            gameObject.GetComponent<Text>().text = GameManager.getScore().ToString();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
