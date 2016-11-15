using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyFormationPrefab;
    GameObject enemyFormation;
    public float minX,maxX;
    float enemySpawningPositionX;
    Vector3 enemySpawningPosition;
    float probability, spawnRate, formationVelocity;
    Rigidbody2D formationRigidBody;

	// Use this for initialization
	void Start () {
        spawnRate = 0.1f;
        formationVelocity = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        probability = spawnRate * Time.deltaTime;
        if(probability>Random.value)
        {
            enemySpawningPositionX = Random.Range(minX, maxX);
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
