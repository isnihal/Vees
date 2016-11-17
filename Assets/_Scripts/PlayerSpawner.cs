using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerFormation;
    GameObject spawnedPlayer;
    Rigidbody2D spawnedPlayerRigidBody;
    public AudioClip playerClip;

    public void spawnPlayer(Vector3 spawningPosition,Vector3 playerVelocity)
    {
        spawningPosition.x = transform.position.x;
        spawnedPlayer = Instantiate(playerFormation, spawningPosition, Quaternion.Euler(new Vector3(0,0,90))) as GameObject;
        spawnedPlayer.transform.parent = transform;
        spawnedPlayerRigidBody = spawnedPlayer.GetComponent<Rigidbody2D>();
        spawnedPlayerRigidBody.velocity = playerVelocity;
        AudioSource.PlayClipAtPoint(playerClip, spawnedPlayer.transform.position,1);
    }
}
