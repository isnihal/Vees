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
        if (xVelocity < 10)
            xVelocity = 10;
        xVelocity=Mathf.Clamp(xVelocity, 10,20);

        //Vector refactoring
        Vector3 playerPosition = new Vector3(0, 0, 0);
        Debug.Log("Y pos" + startPositon.y);
        Vector3 playerVelocity = new Vector3(xVelocity, 0, 0);
        //Spawn player after swipe

        playerSpawner.spawnPlayer(playerPosition,playerVelocity);
        //Spawn a player formation end
    }
}
