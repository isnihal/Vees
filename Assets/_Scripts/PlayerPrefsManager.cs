using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

    static string LANGUAGE_KEY,MUTE_KEY,FIRST_TIME_KEY,EQUALS_HIGH,ONE_HIGH,ESCAPE_HIGH,LAPSE_HIGH;

    void Start()
    {
        LANGUAGE_KEY = "LANGUAGE";
        MUTE_KEY = "MUTE";
        FIRST_TIME_KEY = "FIRST_TIME";
        ONE_HIGH = "ONE_HIGH";
        EQUALS_HIGH = "EQUALS_HIGH";
        ESCAPE_HIGH = "ESCAPE_HIGH";
        LAPSE_HIGH = "LAPSE_HIGH";
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

    /*public static int getHighScore(int fromLevel)
    {
        switch(fromLevel)
        {
            //One Way
            case 3:
                if (PlayerPrefs.HasKey(ONE_HIGH))
                {
                    return (PlayerPrefs.GetInt(ONE_HIGH));
                }
                else
                {
                    return 99;
                }
            //Escape
            case 5:
                if (PlayerPrefs.HasKey(ESCAPE_HIGH))
                {
                    return (PlayerPrefs.GetInt(ESCAPE_HIGH));
                }
                else
                {
                    return 99;
                }
            //Equals
            case 6: if (PlayerPrefs.HasKey(EQUALS_HIGH))
                {
                    return (PlayerPrefs.GetInt(EQUALS_HIGH));
                }
                else
                {
                    return 99;
                }
            //Lapse
            case 7: if (PlayerPrefs.HasKey(LAPSE_HIGH))
                {
                    return (PlayerPrefs.GetInt(LAPSE_HIGH));
                }
                else
                {
                    return 99;
                }

            default:return (999);
        }
    }


    public static void saveHighScore(int score,int fromLevel)
    {
        switch (fromLevel)
        {
            //One Way
            case 3:
                if(score>PlayerPrefs.GetInt(ONE_HIGH))
                {
                    PlayerPrefs.SetInt(ONE_HIGH, score);
                }
                break; 
            //Escape
            case 5:
                if (score > PlayerPrefs.GetInt(ESCAPE_HIGH))
                {
                    PlayerPrefs.SetInt(ESCAPE_HIGH, score);
                }
                break;
            //Equals
            case 6:
                if (score > PlayerPrefs.GetInt(EQUALS_HIGH))
                {
                    PlayerPrefs.SetInt(EQUALS_HIGH, score);
                }
                break;
            //Lapse
            case 7:
                if (score > PlayerPrefs.GetInt(LAPSE_HIGH))
                {
                    PlayerPrefs.SetInt(LAPSE_HIGH, score);
                }
                break;
        }
        PlayerPrefs.Save();
    }

    public static bool isHighScore(int score, int fromLevel)
    {
        switch (fromLevel)
        {
            //One Way
            case 3:
                if (score > PlayerPrefs.GetInt(ONE_HIGH))
                {
                    return true;
                }
                break;
            //Escape
            case 5:
                if (score > PlayerPrefs.GetInt(ESCAPE_HIGH))
                {
                    return true;
                }
                break;
            //Equals
            case 6:
                if (score > PlayerPrefs.GetInt(EQUALS_HIGH))
                {
                    return true;
                }
                break;
            //Lapse
            case 7:
                if (score > PlayerPrefs.GetInt(LAPSE_HIGH))
                {
                    return true;
                }
                break;
            default:return false;
        }
        return false;
    }*/
}
