using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    //Destroy anything that comes its way
    //Just a code to organize gamespace

    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
