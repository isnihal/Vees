using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject EnemyPrefab;
    TopEnemySpawner topSpawner;
    BottomEnemySpawner bottomSpawner;
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
        bottomSpawner = FindObjectOfType<BottomEnemySpawner>();
        topSpawner = FindObjectOfType<TopEnemySpawner>();
    }
	
	// Update is called once per frame
	void Update () {
        probability = spawnRate * Time.deltaTime;

        //Enemy Spawner for One Direction
        if (GameManager.getLevelName() == "ONE_DIRECTION")
        { 
            if (probability > Random.value)
            {
                enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                enemySpawningPosition = new Vector3(enemySpawningPositionX, transform.position.y, 0);
                enemyFormation = Instantiate(EnemyPrefab, enemySpawningPosition, Quaternion.identity) as GameObject;
                enemyFormation.transform.parent = transform;
                formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                formationRigidBody.velocity = Vector3.down * formationVelocity;
                AudioSource.PlayClipAtPoint(enemyClip, enemyFormation.transform.position, 1);
            }
        }

        //Enemy Spawner for Arcade
        if(GameManager.getLevelName()=="ARCADE")
        {
            if(probability>Random.value)
            {
                enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                int topOrBottom = Random.Range(0,2);
                if(topOrBottom == 0)
                {
                    //Spawn enemy at top
                    Debug.Log("Enemy Spawned at top");
                    enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                    enemySpawningPosition = new Vector3(enemySpawningPositionX, topSpawner.transform.position.y, 0);
                    enemyFormation = Instantiate(EnemyPrefab, enemySpawningPosition, Quaternion.identity) as GameObject;
                    enemyFormation.transform.parent = transform;
                    formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                    formationRigidBody.velocity = Vector3.down * formationVelocity;
                    AudioSource.PlayClipAtPoint(enemyClip, enemyFormation.transform.position, 1);
                }

                else
                {
                    //Spawn enemy at bottom
                    Debug.Log("Enemy spawned at bottom");
                    enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                    enemySpawningPosition = new Vector3(enemySpawningPositionX, bottomSpawner.transform.position.y, 0);
                    enemyFormation = Instantiate(EnemyPrefab, enemySpawningPosition, Quaternion.Euler(new Vector3(0,0,180))) as GameObject;
                    enemyFormation.transform.parent = transform;
                    formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                    formationRigidBody.velocity = Vector3.down * -formationVelocity;
                    AudioSource.PlayClipAtPoint(enemyClip, enemyFormation.transform.position, 1);
                }
            }
        }
	}
}
