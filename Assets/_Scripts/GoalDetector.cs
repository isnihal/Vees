using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour {

    //Increment score if player hits'em or decrease score if enemy hits'em

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerFormation>())
        {
            GameManager.incrementScore();
        }

        else if(collider.gameObject.GetComponent<EnemyFormation>())
        {
            GameManager.decrementLife();
        }
    }
}
