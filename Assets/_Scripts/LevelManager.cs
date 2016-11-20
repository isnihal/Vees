using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    //Deals with splash screen and loading of other levels

    void Start()
    {
        //Load main menu if current level is splash screen
        if(isSplash())
        {
            loadMainMenu();
        }
    }

    public void loadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }


    void loadMainMenu()
    {
        Invoke("loadNextLevel", 5f);
    }

    void loadNextLevel()
    {
        
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    bool isSplash()
    {
        //Level 0->00_SPLASH
        if (Application.loadedLevel == 0)
        {
            return true;
        }

        return false;
    }
}
