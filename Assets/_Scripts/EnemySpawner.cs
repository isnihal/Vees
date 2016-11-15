using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyFormationPrefab;
    GameObject enemyFormation;
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
            enemyFormation=Instantiate(enemyFormationPrefab,transform.position,Quaternion.identity) as GameObject;
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
