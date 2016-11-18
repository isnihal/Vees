using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

    public AudioClip destroyClip;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (GameManager.getLevelName() == "FAST_ESCAPE")
        {
            if (collider.gameObject.GetComponent<PlayerFormation>())
            {
                AudioSource.PlayClipAtPoint(destroyClip, transform.position, 1);
                Destroy(collider.gameObject);
            }
        }
    }
}
