using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour {

    float bombingFrequency,spawningX,bombVelocity,probability,minFrequency,maxFrequency;
    public GameObject bombPrefab,parent;
    public AudioClip bombClip;
    GameObject bombFormation;
    Vector3 spawningPosition;

	// Use this for initialization
	void Start () {
        
        if (GameManager.getLevelName() == "ARCADE")
        {
            minFrequency = 0.15f;
            maxFrequency = 0.5f;
            bombingFrequency = minFrequency;
            bombVelocity = -6;
        }
        else
        {
            minFrequency = 0.05f;
            maxFrequency = 0.5f;
            bombingFrequency = minFrequency;
            bombVelocity = -4.45f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        probability = bombingFrequency * Time.deltaTime;
        if (probability > Random.value)
        {
            spawningX = Random.RandomRange(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
            spawningPosition = new Vector3(spawningX, transform.position.y, 0);
            bombFormation = Instantiate(bombPrefab, spawningPosition, Quaternion.identity) as GameObject;
            bombFormation.GetComponent<Rigidbody2D>().velocity = new Vector3(0, bombVelocity, 0);
            bombFormation.transform.parent = parent.transform;
            if (!VolumeManager.getIsMuted())
            {
                AudioSource.PlayClipAtPoint(bombClip, bombFormation.transform.position, 1);
            }
            if (GameManager.getLevelName() != "TIME_LAPSE")
            {
                if (bombingFrequency <= maxFrequency)
                {
                    bombingFrequency += 0.05f;
                }
            }
            else
            {
                if (bombingFrequency <= maxFrequency)
                {
                    bombingFrequency += 0.005f;
                }
            }
        }
	}
}
