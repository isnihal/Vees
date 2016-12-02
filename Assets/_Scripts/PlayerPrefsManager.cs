using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    static string LANGUAGE_KEY,MUTE_KEY;

    void Start()
    {
        LANGUAGE_KEY = "LANGUAGE";
        MUTE_KEY = "MUTE";
    }

    public static void setLanguage(string selectedLanguage)
    {
        PlayerPrefs.SetString(LANGUAGE_KEY, selectedLanguage);
    }

    public static string getLanguage()
    {
        return (PlayerPrefs.GetString(LANGUAGE_KEY));
    }   
}
