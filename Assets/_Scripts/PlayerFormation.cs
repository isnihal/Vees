using UnityEngine;
using System.Collections;

public class PlayerFormation : MonoBehaviour {

    public AudioClip destroyClip;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<EnemyFormation>())
        {
            AudioSource.PlayClipAtPoint(destroyClip, transform.position);
            Destroy(collider.gameObject);
        }
    }
}
