using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    //Critical Script
    //Spawns enemy according to each level properties
    //TODO:FUNCTIONAL REFACTORING

    public GameObject EnemyPrefab,parent;
    public AudioClip enemyClip;
    public Text waveNumberText;
    public static bool resumeTrigger;


    GameObject enemyFormation;
    float enemySpawningPositionX, enemySpawningPositionY;
    Vector3 enemySpawningPosition;
    float probability, spawnRate, formationVelocity;
    Rigidbody2D formationRigidBody;
    enum Position {Top,Bottom,Left,Right};



    //Properties applicable to EQUALS
    static int waveNumber, enemiesKilled, enemiesSpawned;

    //Properties applicable to TIME LAPSE


    // Use this for initialization
    void Start() {
        spawnRate = getSpawnFrequency();
        setFormationVelocity();
        //Properties applicable to equals
        if (GameManager.getLevelName() == "EQUALS")
        {
            waveNumber = 1;
            enemiesSpawned = 0;
            enemiesKilled = 0;
            setWaveNumberText();
            resumeTrigger = true;
        }

        if(GameManager.getLevelName()=="TUTORIAL")
        {
            enemiesSpawned = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        probability = spawnRate * Time.deltaTime;
        
        //Enemy Spawner for One Direction
        if (GameManager.getLevelName() == "ONE_DIRECTION")
        {
            if (probability > Random.value)
            {
                spawnEnemy(Position.Top);
                spawnRate += 0.006f;
                Mathf.Clamp(spawnRate, 1.5f, 8.5f);
            }
        }

        //Enemy Spawner for Arcade
        if (GameManager.getLevelName() == "ARCADE")
        {
            if (probability > Random.value)
            {
                int topOrBottom = Random.Range(0, 2);
                spawnRate += 0.006f;
                Mathf.Clamp(spawnRate, 1.5f, 8.5f);
                if (topOrBottom == 0)
                {
                    spawnEnemy(Position.Top);
                }

                else
                {
                    spawnEnemy(Position.Bottom);
                }
            }
        }


        //Enemy spawner of FAST ESCAPE
        if (GameManager.getLevelName() == "FAST_ESCAPE")
        {
            if (probability > Random.value)
            {
                spawnEnemy(Position.Left);
            }
        }

        //Enemy Spawner of EQUALS
        if (GameManager.getLevelName() == "EQUALS")
        {
            if ((enemiesSpawned < waveNumber)&&resumeTrigger)
            {
                if (probability > Random.value)
                {
                    enemiesSpawned++;
                    resumeTrigger = false;
                    int topOrBottom = Random.Range(0, 2);
                    if (topOrBottom == 0)
                    {
                        spawnEnemy(Position.Top);
                    }
                    else
                    {
                        spawnEnemy(Position.Bottom);
                    }
                }
            }
            else
            {
                if (resumeTrigger)//To ensure all enemies are killed in a wave before moving to next wave
                {
                    waveNumber++;
                    setWaveNumberText();
                    GameManager.resetScore();
                    enemiesSpawned = 0;
                }
            }
        }

        //Enemy Spawner for TIME LAPSE
        if (GameManager.getLevelName() == "TIME_LAPSE")
        {
            if (probability > Random.value)
            {
                //SPAWN RATE INCREASER 
                spawnRate += 0.06f;
                spawnRate = Mathf.Clamp(spawnRate, 0.4f, 8.5f);
                //SPAWN RATE INCREASER
                int topOrBottom = Random.Range(0, 2);
                if (topOrBottom == 0)
                {
                    spawnEnemy(Position.Top);
                }

                else
                {
                    spawnEnemy(Position.Bottom);
                }
            }
        }

        //Enemy Spawner for TUTORIAL
        if (GameManager.getLevelName() == "TUTORIAL")
        {
            if (TutorialManager.getCurentState() == 2 && enemiesSpawned<1)//BASIC_ENEMY
            {
                if (probability > Random.value)
                {
                    spawnEnemy(Position.Top);
                    enemiesSpawned++;
                }
            }
        }
    }

    void spawnEnemy(Position position)
    {
        switch(position)
        {
            case Position.Top:
                enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                enemySpawningPosition = new Vector3(enemySpawningPositionX, transform.position.y, 0);
                enemyFormation = Instantiate(EnemyPrefab, enemySpawningPosition, Quaternion.identity) as GameObject;
                formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                formationRigidBody.velocity = Vector3.down * formationVelocity;
                setEnemyFormationProperties();
                break;

            case Position.Bottom:
                enemySpawningPositionX = Random.Range(ScreenManager.getLeftBoundary(), ScreenManager.getRightBoundary());
                enemySpawningPosition = new Vector3(enemySpawningPositionX, -transform.position.y, 0);
                enemyFormation = Instantiate(EnemyPrefab, enemySpawningPosition, Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
                formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                formationRigidBody.velocity = Vector3.down * -formationVelocity;
                setEnemyFormationProperties();
                break;

            case Position.Left:
                enemySpawningPositionY = Random.Range(ScreenManager.getBottomBoundary(), ScreenManager.getTopBoundary());
                enemySpawningPosition = new Vector3(transform.position.x, enemySpawningPositionY, 0);
                enemyFormation = Instantiate(EnemyPrefab, enemySpawningPosition, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
                formationRigidBody = enemyFormation.GetComponent<Rigidbody2D>();
                formationRigidBody.velocity = Vector3.right * formationVelocity;
                setEnemyFormationProperties();
                break;
        }
    }

    void setEnemyFormationProperties()
    {
        enemyFormation.transform.parent = parent.transform;
        if (!VolumeManager.getIsMuted())
        {
            AudioSource.PlayClipAtPoint(enemyClip, enemyFormation.transform.position, 1);
        }
        
    }

    float getSpawnFrequency()
    {
        if (GameManager.getLevelName() == "ARCADE")
        {
            return 1.5f;
        }
        else if (GameManager.getLevelName() == "EQUALS")
        {
            return 0.8f;
        }

        else if(GameManager.getLevelName()=="TIME_LAPSE")
        {
            return 0.4f;
        }

        else if (GameManager.getLevelName() =="FAST_ESCAPE")
        {
            return 1.75f;
        }
        
        else
        {
            return 1.5f;
        }
    }

    void setFormationVelocity()
    {
        if (GameManager.getLevelName() == "ONE_DIRECTION" || GameManager.getLevelName() == "EQUALS" || GameManager.getLevelName() == "FAST_ESCAPE")
        {
            formationVelocity = 10.75f;
        }
        else if (GameManager.getLevelName() == "ARCADE")
        {
            formationVelocity = 12.85f;
        }

        else if(GameManager.getLevelName()=="TUTORIAL")
        {
            formationVelocity = 4f;
        }

        else if(GameManager.getLevelName()=="TIME_LAPSE")
        {
            formationVelocity = 13.85f;
        }
    }

    public static void decreaseSpawnedCount()
    {
        enemiesSpawned--;
        resumeTrigger = true;
    }

    void setWaveNumberText()
    {
        waveNumberText.text = "WAVE:" + waveNumber;
    }

    public static int getWaveNumber()
    {
        return waveNumber;
    }
}
