using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

    //When enemies have destruction permission,This class is effective

    public AudioClip destroyClip;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (GameManager.getLevelName() == "FAST_ESCAPE")
        {
            if (collider.gameObject.GetComponent<PlayerFormation>())
            {
                if (!PlayerPrefsManager.isMuted())
                {
                    AudioSource.PlayClipAtPoint(destroyClip, transform.position, 1);
                }
                Destroy(collider.gameObject);
                GameManager.decrementLife();
                Debug.Log("Lives remaining:" + GameManager.getLife());
            }
        }
    }
}
