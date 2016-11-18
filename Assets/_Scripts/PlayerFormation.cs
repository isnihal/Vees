using UnityEngine;
using System.Collections;

public class PlayerFormation : MonoBehaviour {

    public AudioClip destroyClip;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (GameManager.getLevelName() != "FAST_ESCAPE")
        {
            if (collider.gameObject.GetComponent<EnemyFormation>())
            {
                AudioSource.PlayClipAtPoint(destroyClip, transform.position, 1);
                GameManager.incrementScore();
                Destroy(collider.gameObject);
            }
        }
    }
}
