using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyFormationPrefab;
    GameObject enemyFormation;
    float enemySpawningPositionX;
    Vector3 enemySpawningPosition;
    float probability, spawnRate, formationVelocity;
    Rigidbody2D formationRigidBody;
    public AudioClip enemyClip;


    // Use this for initialization
    void Start () {
        spawnRate = 0.5f;
        formationVelocity = 10f;
    }
	
	// Update is called once per frame
	void Update () {


        //Enemy Spawner for One Direction
        if (GameManager.getLevelName() == "ONE_DIRECTION")
        {
            probability = spawnRate * Time.deltaTime;
            if (probability > Random.value)
            {
                Debug.Log("Enemy Spawned");
                enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                enemySpawningPosition = new Vector3(enemySpawningPositionX, transform.position.y, 0);
                enemyFormation = Instantiate(enemyFormationPrefab, enemySpawningPosition, Quaternion.identity) as GameObject;
                enemyFormation.transform.parent = transform;
                formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                formationRigidBody.velocity = Vector3.down * formationVelocity;
                AudioSource.PlayClipAtPoint(enemyClip, enemyFormation.transform.position, 1);
            }
        }

        //Enemy Spawner for Arcade

	}

    void OnDrawGizmos()
    {
        Debug.Log("Draw gizoms");
        Gizmos.DrawWireCube(transform.position,Vector3.one);
    }
}
