using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    static string LANGUAGE_KEY,MUTE_KEY,FIRST_TIME_KEY;

    void Start()
    {
        LANGUAGE_KEY = "LANGUAGE";
        MUTE_KEY = "MUTE";
        FIRST_TIME_KEY = "FIRST_TIME";
    }

    public static void setLanguage(string selectedLanguage)
    {
        PlayerPrefs.SetString(LANGUAGE_KEY, selectedLanguage);
    }

    public static string getLanguage()
    {
        return (PlayerPrefs.GetString(LANGUAGE_KEY));
    }   

    public static void setMute(int volume)
    {
        PlayerPrefs.SetInt(MUTE_KEY, volume);
    }

    public static bool isMuted()
    {
        if(PlayerPrefs.GetInt(MUTE_KEY)==0)
        {
            return (true);
        }
        else
        {
            return (false);
        }
    }

    public static void setFirstTime()
    {
        PlayerPrefs.SetInt(FIRST_TIME_KEY, 1);
        PlayerPrefs.Save();
    }

    public static bool isFirstTime()
    {
        Debug.Log("First time:" + PlayerPrefs.GetInt(FIRST_TIME_KEY));
        if(PlayerPrefs.GetInt(FIRST_TIME_KEY)!=1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool hasFirstTimeKey()
    {
        if(PlayerPrefs.HasKey(FIRST_TIME_KEY))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
