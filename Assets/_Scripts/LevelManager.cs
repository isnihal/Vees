using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    //Replay Setting
    static int fromLevel;

    string toastString;
    string input;
    AndroidJavaObject currentActivity;
    AndroidJavaClass UnityPlayer;
    AndroidJavaObject context;
    bool startTimer,firstTap;
    float ButtonCooler; //Time before reset
    int ButtonCount;

    //Deals with splash screen and loading of other levels

    void Start()
    {
        //Load main menu if current level is splash screen
        if(isSplash())
        {
            loadMainMenu();
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }

        firstTap = false;
        ButtonCooler = 2.5f;
        ButtonCount = 0;
    }

    void Update()
    {
        detectDoubleTap();
    }

    void detectDoubleTap()
    {
        
        if (Input.GetKeyDown((KeyCode.Escape)))
            {

                if(ButtonCooler>0 && ButtonCount==0)
                {
                switch (Application.loadedLevel)
                {
                    case 1:
                        showToastOnUiThread("Press again to quit");
                        break;
                    case 2:
                        Application.LoadLevel(1);//Load main menu
                        break;
                    case 9:
                        Application.LoadLevel(1);//Load main menu
                        break;
                    default: showToastOnUiThread("Press again to return to main menu");
                        break;
                }
                }
                if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
                {
                //Has double tapped
                    switch (Application.loadedLevel)
                    {
                        case 1:
                        Application.Quit();
                        break;
                        default: Application.LoadLevel(1);
                        break;
                }
                }
                else
                {
                    ButtonCooler = 2.5f;
                    ButtonCount += 1;
                }
            }

            if (ButtonCooler > 0)
            {

                ButtonCooler -= 1 * Time.deltaTime;

            }
            else
            {
                ButtonCount = 0;
            }
   }


    public void loadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    public void setLevelIndex(int levelIndex)
    {
        fromLevel = levelIndex;
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

    public void replayGame()
    {
        Application.LoadLevel(fromLevel);
    }


    public void showToastOnUiThread(string toastString)
    {
        this.toastString = toastString;
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(showToast));
    }

    void showToast()
    {
        Debug.Log(this + ": Running on UI thread");

        AndroidJavaClass Toast = new AndroidJavaClass("android.widget.Toast");
        AndroidJavaObject javaString = new AndroidJavaObject("java.lang.String", toastString);
        AndroidJavaObject toast = Toast.CallStatic<AndroidJavaObject>("makeText", context, javaString, Toast.GetStatic<int>("LENGTH_SHORT"));
        toast.Call("show");
    }
}
