using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    static string LANGUAGE_KEY,MUTE_KEY;

    public static void setLanguage(string selectedLanguage)
    {
        PlayerPrefs.SetString(LANGUAGE_KEY, selectedLanguage);
    }

    public static string getLanguage()
    {
        return (PlayerPrefs.GetString(LANGUAGE_KEY));
    }   

    public static void setMute(int value)
    {
        PlayerPrefs.SetInt(MUTE_KEY, value);
    }

    public static bool isMuted()
    {
        if(PlayerPrefs.GetInt(MUTE_KEY)==1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
