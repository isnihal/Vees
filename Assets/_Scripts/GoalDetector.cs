using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour {

    public static bool fromEquals=false;

    //Increment score if player hits'em or decrease life if enemy hits'em

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject collidedObject=collider.gameObject;
        //Special condition for EQUALS
        if (GameManager.getLevelName() == "EQUALS")
        {
            fromEquals = true;
            GameManager.setLife(0);
        }

        else if(GameManager.getLevelName()=="TUTORIAL")
        {
            if (collider.gameObject.GetComponent<EnemyFormation>())
            {
                EnemySpawner.decreaseSpawnedCount();
            }
            else if(collider.gameObject.GetComponent<PlayerFormation>())
            {
                PlayerSpawner.decreaseSpawnedVees();
            }
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
