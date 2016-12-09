using UnityEngine;
using GooglePlayGames;
using UnityEngine.SceneManagement;
using Google;
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
    bool doubleTapped;

    //Deals with splash screen and loading of other levels

    void Start()
    {
        //Load main menu if current level is splash screen
        //Load Tutorial if game is loaded for the first time
        if (isSplash())
        {
            loadMainMenu();
        }
        
        if(isMainMenu())
        {
            PlayGamesPlatform.Activate();
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    Debug.Log("Google sign in failed");
                }
                else
                {
                    Debug.Log("Google sign in success :D");
                }
            });
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        }

        doubleTapped = false;
        ButtonCooler = 2.5f;
        ButtonCount = 0;
    }

    void Update()
    {
        detectDoubleTap();
        detectSingleTap();
    }


    void detectSingleTap()
    {
        if (Input.GetKeyDown((KeyCode.Escape)) && !doubleTapped)
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
                default:
                    showToastOnUiThread("Press again to return to main menu");
                    break;
            }
        }
    }
    void detectDoubleTap()
    {
        
        if (Input.GetKeyDown((KeyCode.Escape)))
            {
                if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
                {
                //Has double tapped
                    doubleTapped = true;
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
                doubleTapped = false;
            }
   }


    public void loadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
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
       SceneManager.LoadScene(Application.loadedLevel + 1);
    }

    void loadTutorial()
    {
        Invoke("showTutorial", 5f); 
    }

    void showTutorial()
    {
        SceneManager.LoadScene(10);
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


    bool isMainMenu()
    {
        //Level 1->MAIN MENU
        if (Application.loadedLevel == 1)
        {
            return true;
        }

        return false;
    }

    public void replayGame()
    {
        SceneManager.LoadScene(fromLevel);
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
