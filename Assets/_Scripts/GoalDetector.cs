using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameManager.incrementScore();
        Debug.Log("Score:" + GameManager.getScore());
    }
}
