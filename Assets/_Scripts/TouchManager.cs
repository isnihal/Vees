using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    static Vector3 startPositon, endPosition;
    static float startTime, endTime,xVelocity;
    PlayerSpawner playerSpawner;

    void DragStart()
    {
        startPositon = Input.mousePosition;
        startTime = Time.time;
        playerSpawner = FindObjectOfType<PlayerSpawner>();
    }

    void DragEnd()
    {
        endPosition = Input.mousePosition;
        endTime = Time.time;
        
        //Spawn a player formation start
        //Calculate X velocity
        xVelocity = (endPosition.x - startPositon.x) / (endTime - startTime);

        //ONE DIRECTION PROPERTIES
        if (GameManager.getLevelName() == "ONE_DIRECTION")
        {
            xVelocity = Mathf.Clamp(xVelocity, 10, 20);
        }

        //ARCADE PROPERTIES
       if(GameManager.getLevelName()=="ARCADE")
        {
            if(xVelocity < 0)
            {
                xVelocity = Mathf.Clamp(xVelocity, -20, -10);
            }

            else
            {
                xVelocity = Mathf.Clamp(xVelocity, 10, 20);
            }
        }

        //Convert start pos to world unit
        startPositon = Camera.main.ScreenToWorldPoint(startPositon);

        //Vector refactoring
        Vector3 playerPosition = new Vector3(0,Mathf.Clamp(startPositon.y,-5,5), 0);
        Vector3 playerVelocity = new Vector3(xVelocity, 0, 0);

        //Spawn player after swipe
        playerSpawner.spawnPlayer(playerPosition,playerVelocity);

        //Spawn a player formation end
    }
}
