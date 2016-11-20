using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour {

    //Increment score if player hits'em or decrease score if enemy hits'em

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collidedObject=collider.gameObject;
        //Special condition for EQUALS
        if (GameManager.getLevelName() == "EQUALS")
        {
            Application.LoadLevel("GAME_OVER");
        }
        //For other levels
        else
        {
            if (collidedObject.GetComponent<PlayerFormation>())
            {
                GameManager.incrementScore();
            }

            else if (collidedObject.GetComponent<EnemyFormation>())
            {
                GameManager.decrementLife();
            }
        }
    }
}
