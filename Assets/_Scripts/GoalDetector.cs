using UnityEngine;
using System.Collections;

public class GoalDetector : MonoBehaviour {

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
