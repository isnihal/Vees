using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerFormation;

    public void spawnPlayer(Vector3 spawningPosition)
    {
        Instantiate(playerFormation,spawningPosition, Quaternion.identity);
    }
}
