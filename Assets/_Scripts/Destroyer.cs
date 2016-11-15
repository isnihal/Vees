using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
