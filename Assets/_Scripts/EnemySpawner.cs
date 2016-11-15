using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyFormationPrefab;
    GameObject enemyFormation;
    float enemySpawningPositionX;
    Vector3 enemySpawningPosition;
    float probability, spawnRate, formationVelocity;
    Rigidbody2D formationRigidBody;
    //Boundary variables
    Vector3 leftBounday, rightBoundary;
    float leftX, rightX,padding;


    // Use this for initialization
    void Start () {
        spawnRate = 0.5f;
        formationVelocity = 10f;
        leftBounday = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        padding = 0.5f;
        leftX = leftBounday.x+padding;
        rightX = rightBoundary.x-padding;
    }
	
	// Update is called once per frame
	void Update () {
        probability = spawnRate * Time.deltaTime;
        if(probability>Random.value)
        {
            Debug.Log("Enemy Spawned");
            enemySpawningPositionX = Random.Range(leftX, rightX);
            enemySpawningPosition = new Vector3(enemySpawningPositionX, transform.position.y, 0);
            enemyFormation=Instantiate(enemyFormationPrefab,enemySpawningPosition,Quaternion.identity) as GameObject;
            formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
            formationRigidBody.velocity = Vector3.down*formationVelocity;
        }
	}

    void OnDrawGizmos()
    {
        Debug.Log("Draw gizoms");
        Gizmos.DrawWireCube(transform.position,Vector3.one);
    }
}
