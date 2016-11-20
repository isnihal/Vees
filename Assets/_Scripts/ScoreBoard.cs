using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreBoard : MonoBehaviour {

    //Set scoreboard for gameOver level

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Text>().text = GameManager.getScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
