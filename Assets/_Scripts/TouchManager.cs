using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    static Vector3 startPositon, endPosition;
    static float startTime, endTime,xVelocity,yVelocity;
    PlayerSpawner playerSpawner;
    Vector3 playerPosition, playerVelocity;
    float dragLength, dragDuration, dragHeight,minimumDragLength=40,minimumDragHeight=25;

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
        dragLength=(endPosition.x - startPositon.x);
        dragHeight = (endPosition.y - startPositon.y);
        dragDuration = endTime - startTime;
        if (endTime - startTime != 0 && ((Mathf.Abs(dragLength)>minimumDragLength)||(Mathf.Abs(dragHeight)>minimumDragHeight)))
        {
            xVelocity = dragLength / dragDuration;
            yVelocity = dragLength / dragDuration;


            //ONE DIRECTION PROPERTIES
            if (GameManager.getLevelName() == "ONE_DIRECTION")
            {
                xVelocity = Mathf.Clamp(xVelocity, 10, 20);
            }

            //ARCADE PROPERTIES
            if (GameManager.getLevelName() == "ARCADE")
            {
                if (xVelocity < 0)
                {
                    xVelocity = Mathf.Clamp(xVelocity, -20, -10);
                }

                else
                {
                    xVelocity = Mathf.Clamp(xVelocity, 10, 20);
                }
            }

            //FAST ESCAPE PROPERTIES
            if (GameManager.getLevelName() == "FAST_ESCAPE")
            {

                yVelocity = Mathf.Clamp(yVelocity, 10, 20);
            }

            //Convert start pos to world unit
            startPositon = Camera.main.ScreenToWorldPoint(startPositon);

            //Vector refactoring
            if (GameManager.getLevelName() == "ARCADE" || GameManager.getLevelName() == "ONE_DIRECTION")
            {
                playerPosition = new Vector3(0, Mathf.Clamp(startPositon.y, -5, 5), 0);
                playerVelocity = new Vector3(xVelocity, 0, 0);
            }
            else
            {
                playerPosition = new Vector3(startPositon.x, 0, 0);
                playerVelocity = new Vector3(0, yVelocity, 0);
            }

            //Spawn player after swipe
            playerSpawner.spawnPlayer(playerPosition, playerVelocity);

            //Spawn a player formation end
        }
    }
}
