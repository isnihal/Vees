using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    //Important script
    //Responsible for generating player vees
    //Spawn vees for each level
    //TODO:Functional refactoring

    public GameObject playerFormation,parent;
    GameObject spawnedPlayer;
    Rigidbody2D spawnedPlayerRigidBody;
    public AudioClip playerClip;
    enum Position {Top,Bottom,Left,Right}
    static int spawnedVees;

    void Start()
    {
        spawnedVees = 0;
    }

    public void spawnPlayer(Vector3 spawningPosition, Vector3 playerVelocity)
    {
        //Check if electric gun has charge
        if (ElectricGun.getCharge() > 0)
        {
            ElectricGun.decrementCharge();
            //Spawn Player for ONE_DIRECTION
            if (GameManager.getLevelName() == "ONE_DIRECTION")
            {
                if (playerVelocity.x > 0)
                {
                    spawnPlayerFormation(Position.Left, spawningPosition, playerVelocity);
                }
            }

            //Spawn player for ARCADE
            if (GameManager.getLevelName() == "ARCADE")
            {
                if (playerVelocity.x > 0)
                {
                    spawnPlayerFormation(Position.Left, spawningPosition, playerVelocity);
                }

                else
                {
                    spawnPlayerFormation(Position.Right, spawningPosition, playerVelocity);
                }
            }


            //Spawn player for FAST ESCAPE
            if (GameManager.getLevelName() == "FAST_ESCAPE")
            {
                spawnPlayerFormation(Position.Bottom, spawningPosition, playerVelocity);
            }

            //Spawn player for EQUALS
            if (GameManager.getLevelName() == "EQUALS")
            {
                if (playerVelocity.x > 0)
                {
                    spawnPlayerFormation(Position.Left, spawningPosition, playerVelocity);
                }

                else
                {
                    //Spawn from right
                    spawnPlayerFormation(Position.Right, spawningPosition, playerVelocity);
                }
            }

            //Spawn player for TIME LAPSE
            if (GameManager.getLevelName() == "TIME_LAPSE")
            {
                if (playerVelocity.x > 0)
                {
                    spawnPlayerFormation(Position.Left, spawningPosition, playerVelocity);
                }

                else
                {
                    spawnPlayerFormation(Position.Right, spawningPosition, playerVelocity);
                }
            }

            //Spawn player for tutorial
            if (GameManager.getLevelName() == "TUTORIAL")
            {
                if (TutorialManager.getCurentState()==1)//Basic
                {
                    if (playerVelocity.x > 0 && spawnedVees<1)
                    {
                        spawnPlayerFormation(Position.Left, spawningPosition, playerVelocity);
                        spawnedVees++;
                        TutorialManager.veeHasSpawned();
                    }
                }

                if (TutorialManager.getCurentState() == 2)//Basic_Enemy
                {
                    if (playerVelocity.x > 0 && spawnedVees < 2)
                    {
                        spawnPlayerFormation(Position.Left, spawningPosition, playerVelocity);
                        spawnedVees++;
                        TutorialManager.veeHasSpawned();
                    }
                }
            }
        }
    }

    void spawnPlayerFormation(Position position, Vector3 spawningPosition, Vector3 playerVelocity)
    {
        switch(position)
        {
            case Position.Left:
                spawningPosition.x = transform.position.x;
                spawnedPlayer = Instantiate(playerFormation, spawningPosition, Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;
                setPlayerFormationProperties(playerVelocity);
                break;
            case Position.Right:
                spawningPosition.x = -transform.position.x;
                spawnedPlayer = Instantiate(playerFormation, spawningPosition, Quaternion.Euler(new Vector3(0, 0, -90))) as GameObject;
                setPlayerFormationProperties(playerVelocity);
                break;
            case Position.Bottom:
                spawningPosition.y = transform.position.y;
                spawnedPlayer = Instantiate(playerFormation, spawningPosition, Quaternion.Euler(new Vector3(0, 0, 180))) as GameObject;
                setPlayerFormationProperties(playerVelocity);
                break;

        }
    }

    void setPlayerFormationProperties(Vector3 playerVelocity)
    {
        spawnedPlayer.transform.parent = parent.transform;
        spawnedPlayerRigidBody = spawnedPlayer.GetComponent<Rigidbody2D>();
        spawnedPlayerRigidBody.velocity = playerVelocity;
        if (!VolumeManager.getIsMuted())
        {
            AudioSource.PlayClipAtPoint(playerClip, spawnedPlayer.transform.position, 1);
        }
    }

    public static int getVeesSpawned()
    {
        return spawnedVees;
    }

    public static void decreaseSpawnedVees()
    {
        spawnedVees--;
    }
}
