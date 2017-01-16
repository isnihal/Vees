using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

    //Script that deals with TouchInput
    //Critical Script
    /*
     Functions
     1)Calculate swipeVelocity
     2)Check for a minimum swipe distance
     3)Set drag properties to each level
    */

    static Vector3 startPositon, endPosition;
    static float startTime, endTime,xVelocity,yVelocity;
    PlayerSpawner playerSpawner;
    Vector3 playerPosition, playerVelocity;
    float dragLength, dragDuration, dragHeight,minimumDragLength,minimumDragHeight;

    void Start()
    {
        minimumDragLength = 10;
        minimumDragHeight = 10;
    }

    public void DragStart()
    {
        startPositon = Input.mousePosition;
        startTime = Time.time;
        playerSpawner = FindObjectOfType<PlayerSpawner>();
    }

    public void DragEnd()
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
                if (xVelocity < 0)
                {
                    xVelocity = Mathf.Clamp(xVelocity, -20, -10);
                }

                else
                {
                    xVelocity = Mathf.Clamp(xVelocity, 10, 20);
                }
            }

            //ARCADE & EQUALS PROPERTIES
            if (GameManager.getLevelName() == "ARCADE" || GameManager.getLevelName()== "EQUALS" || GameManager.getLevelName()=="TIME_LAPSE")
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

                yVelocity = Mathf.Clamp(yVelocity, 18, 20);
            }

            //Tutorial properties
            if (GameManager.getLevelName() == "TUTORIAL")
            {
                if (TutorialManager.getCurentState() == 1)//Basic 
                {
                    if (xVelocity > 0)
                    {
                        xVelocity = Mathf.Clamp(xVelocity,9,12);
                    }
                }

                if (TutorialManager.getCurentState() == 2)//Basic ENEMY
                {
                    if (xVelocity > 0)
                    {
                        xVelocity = Mathf.Clamp(xVelocity, 9, 12);
                    }
                }
            }

                //Convert start pos to world unit
                startPositon = Camera.main.ScreenToWorldPoint(startPositon);

            //Vector refactoring
            if (GameManager.getLevelName()!="FAST_ESCAPE")
            {
                //X Velocity for all other levels
                playerPosition = new Vector3(0, Mathf.Clamp(startPositon.y,ScreenManager.getBottomBoundary(),ScreenManager.getTopBoundary()), 0);
                playerVelocity = new Vector3(xVelocity, 0, 0);
            }
            else
            {
                //FAST ESCAPE PROPERTIES
                playerPosition = new Vector3(startPositon.x, 0, 0);
                playerVelocity = new Vector3(0, yVelocity, 0);
            }

            //Spawn player after swipe
            if (!GameManager.isGamePaused())
            {
                playerSpawner.spawnPlayer(playerPosition, playerVelocity);
            }

            //Spawn a player formation end
        }
    }
}
