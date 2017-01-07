using UnityEngine;
using System.Collections;

public class PlayerFormation : MonoBehaviour {

    //Destroy enemies colliding with player except for levels FAST_ESCAPE

    public AudioClip destroyClip,blastClip;
    static int totalEnemiesKilledInThisWave;

    void Start()
    {
        totalEnemiesKilledInThisWave = 0;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        //Player Formation for arcade
        if(GameManager.getLevelName()=="ARCADE")
        {
            //Destroy enemy Vees and player vees
            if (collider.gameObject.GetComponent<EnemyFormation>())
            {
                DestroyEnemyVees(collider);
                Destroy(gameObject);
            }
        }

        //For Time Lapse and One Direction
        else if (GameManager.getLevelName() != "FAST_ESCAPE" && GameManager.getLevelName() != "EQUALS" && GameManager.getLevelName()!="TUTORIAL")
        {
            //Destroy enemy Vees only
            if (collider.gameObject.GetComponent<EnemyFormation>())
            {
                DestroyEnemyVees(collider);
            }

            if(collider.gameObject.GetComponent<GoldenVee>())
            {
                if (GameManager.getLevelName() == "ONE_DIRECTION")
                {
                    GameManager.incrementLife();
                }
                else
                {
                    GameManager.incrementScoreBy(49);
                }
                DestroyEnemyVees(collider);
            }
        }

        //For equals only
        else if (GameManager.getLevelName() == "EQUALS")
        {
            //Destroy player and enemey Vees
            if (collider.gameObject.GetComponent<EnemyFormation>())
            {
                totalEnemiesKilledInThisWave++;
                DestroyEnemyVees(collider);
                Destroy(gameObject);
                EnemySpawner.resumeTrigger = true;
            }
        }

        //For Tutorial
        else if(GameManager.getLevelName()=="TUTORIAL")
        {
            //Destroy enemy Vees only
            if (collider.gameObject.GetComponent<EnemyFormation>())
            {
                DestroyEnemyVees(collider);
                TutorialManager.veeHasCollided();
            }
        }


        //Handle Bomb collission
        if(collider.gameObject.GetComponent<BombFormation>())
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
            if (!VolumeManager.getIsMuted())
            {
                AudioSource.PlayClipAtPoint(blastClip, transform.position);
            }
            if (GameManager.getLevelName() == "ARCADE")
            {
                GameManager.setLife(0);
            }
            else if(GameManager.getLevelName()=="TIME_LAPSE")
            {
                GameManager.decrementScoreBy(5);
            }
        }
    }

    void DestroyEnemyVees(Collider2D collider)
    {
        if (!VolumeManager.getIsMuted())
        {
            AudioSource.PlayClipAtPoint(destroyClip, transform.position, 1);
        }
        GameManager.incrementScore();
        Destroy(collider.gameObject);
    }
}
