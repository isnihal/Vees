using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreBoard : MonoBehaviour
{

    public Text typeBoard;

    static int showRewardedAdAfter = 6, showNoRewardAdAfter = 7, numberOfGames = 0, numberOfLapseGames = 0, numberOfEqualsGames;

    const string oneWayLeaderBoardID = "CgkIiY779uUNEAIQEw";
    const string equalsLeaderBoardID = "CgkIiY779uUNEAIQFA";
    const string escapeLeaderBoardID = "CgkIiY779uUNEAIQFQ";
    const string lapseLeaderBoardID = "CgkIiY779uUNEAIQFg";
    const string boomLeaderBoardID = "CgkIiY779uUNEAIQFw";

    static string filePath = Application.persistentDataPath + "/csd.dat";

    //Set scoreboard for gameOver level

    void Start()
    {
        postScoreToLeaderBoard();
        checkForAchievements();
        loadHighScore();
        //checkIfHighScore();
        if (GameManager.hasGameBeenRestarted())
        {
            numberOfGames = 0;
        }
        else
        {
            numberOfGames++;
        }

        //Ad Script
        if (!IAPManager.hasUserPurchasedVees())
        {
            if (numberOfGames % showNoRewardAdAfter == 0 && !GameManager.hasGameBeenRestarted() && Advertisement.IsReady())//Show ad only if user didnt view ad for restarting
            {
                AdManager.showNoRewardedAd();
            }
        }

        if (!VolumeManager.getIsMuted())
        {
            VolumeManager.setMusicPlayerOnIfSilent();
        }

        if (LevelManager.getFromLevel() == 6)
        {
            //Wave number for level EQUALS
            gameObject.GetComponent<Text>().text = EnemySpawner.getWaveNumber().ToString();
            //PlayerPrefsManager.saveHighScore(EnemySpawner.getWaveNumber(), LevelManager.getFromLevel());
            GoalDetector.fromEquals = false;
            typeBoard.text = "WAVE";
        }
        else if (LevelManager.getFromLevel() == 5)
        {
            typeBoard.text = "ESCAPES";
            //PlayerPrefsManager.saveHighScore(GameManager.getScore(), LevelManager.getFromLevel());
            gameObject.GetComponent<Text>().text = GameManager.getScore().ToString();
        }

        else
        {
            typeBoard.text = "HITS";
            //PlayerPrefsManager.saveHighScore(GameManager.getScore(), LevelManager.getFromLevel());
            gameObject.GetComponent<Text>().text = GameManager.getScore().ToString();
        }
    }

    void Update()
    {
        if (LevelManager.getFromLevel() == 6)
        {
            //Wave number for level EQUALS
            gameObject.GetComponent<Text>().text = EnemySpawner.getWaveNumber().ToString();
            GoalDetector.fromEquals = false;
        }
        else
        {
            if (LevelManager.getFromLevel() != 5)
            {
                gameObject.GetComponent<Text>().text = GameManager.getScore().ToString();
            }
            else
            {
                gameObject.GetComponent<Text>().text = GameManager.getScore().ToString();
            }
        }
    }

    void checkForAchievements()
    {
        if (Advertisement.IsReady())
        {
            if (LevelManager.getFromLevel() == 7)
            {
                numberOfLapseGames++;
            }
            else
            {
                numberOfLapseGames = 0;
            }
            if (numberOfLapseGames == 10)
            {
                Social.ReportProgress(GPGSIds.achievement_lapse_love, 100, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });

            }



            if (LevelManager.getFromLevel() == 6)
            {
                numberOfEqualsGames++;
            }
            else
            {
                numberOfEqualsGames = 0;
            }
            if (numberOfEqualsGames == 10)
            {
                Social.ReportProgress(GPGSIds.achievement_equals_love, 100, (bool sucess) =>
                {
                    if (sucess)
                    {

                    }
                    else
                    {

                    }
                });
            }
        }
    }

    public void postScoreToLeaderBoard()
    {
        switch (LevelManager.getFromLevel())
        {
            case 3:
                Social.ReportScore(GameManager.getScore(), oneWayLeaderBoardID, (bool success) =>
                {
                    if (success)
                    {

                    }
                    else
                    {

                    }
                });

                break;
            case 6:
                Social.ReportScore(EnemySpawner.getWaveNumber(), equalsLeaderBoardID, (bool success) =>
                {
                    if (success)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case 5:
                Social.ReportScore(GameManager.getScore(), escapeLeaderBoardID, (bool success) =>
                {
                    if (success)
                    {

                    }
                    else
                    {
                    }
                });
                break;
            case 7:
                Social.ReportScore(GameManager.getScore(), lapseLeaderBoardID, (bool success) =>
                {
                    if (success)
                    {

                    }
                    else
                    {

                    }
                });
                break;
            case 4:
                Social.ReportScore(GameManager.getScore(), boomLeaderBoardID, (bool success) =>
                {
                    if (success)
                    {

                    }
                    else
                    {

                    }
                });
                break;
        }
    }

    static float nihalEncryption(float input)
    {
        return (Mathf.Log10(input));
    }

    static float nihalDecryption(float input)
    {
        return (Mathf.Pow(10, input));
    }

    static void saveOneWayHigh(float score,float escape,float equals,float lapse)
    {
        BinaryFormatter myBinaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        veesData obj = new veesData();
        obj.sys = nihalEncryption(score);
        obj.cache = nihalEncryption(escape);
        obj.config = nihalEncryption(equals);
        obj.tmp = nihalEncryption(lapse);
        myBinaryFormatter.Serialize(file, obj);
        file.Close();
    }

    static void saveEscapeHigh(float score,float one,float equals,float lapse)
    {
        BinaryFormatter myBinaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        veesData obj = new veesData();
        obj.sys = nihalEncryption(one);
        obj.cache = nihalEncryption(score);
        obj.config = nihalEncryption(equals);
        obj.tmp = nihalEncryption(lapse);
        myBinaryFormatter.Serialize(file, obj);
        file.Close();
    }

    static void saveEqualsHigh(int score, float one, float escape, float lapse)
    {
        BinaryFormatter myBinaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        veesData obj = new veesData();
        obj.sys = nihalEncryption(one);
        obj.cache = nihalEncryption(escape);
        obj.config = nihalEncryption(score);
        obj.tmp = nihalEncryption(lapse);
        myBinaryFormatter.Serialize(file, obj);
        file.Close();
    }

    static void saveLapseHigh(int score, float one, float escape, float equals)
    {
        BinaryFormatter myBinaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(filePath);

        veesData obj = new veesData();
        obj.sys = nihalEncryption(one);
        obj.cache = nihalEncryption(escape);
        obj.config = nihalEncryption(equals);
        obj.tmp = nihalEncryption(score);
        myBinaryFormatter.Serialize(file, obj);
        file.Close();
    }

    public static void loadHighScore()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter myBinaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
            veesData obj = (veesData)myBinaryFormatter.Deserialize(file);
            file.Close();

            float oneWayHighScore = nihalDecryption(obj.sys);
            float escapeHighScore = nihalDecryption(obj.cache);
            float equalsHighScore = nihalDecryption(obj.config);
            float lapseHighScore =  nihalDecryption(obj.tmp);

            //If previousScore>HighScore without errors BINGO! commit here
            switch (LevelManager.getFromLevel())
            {
                case 3:
                    if (GameManager.getScore() > oneWayHighScore)
                    {
                        saveOneWayHigh(GameManager.getScore(),escapeHighScore,equalsHighScore,lapseHighScore);
                    }
                break;

                case 5:
                    if (GameManager.getScore() > escapeHighScore)
                    {
                        saveEscapeHigh(GameManager.getScore(),oneWayHighScore, equalsHighScore, lapseHighScore);
                    }
                    break;

                case 6:
                    if (EnemySpawner.getWaveNumber() > equalsHighScore)
                    {
                        saveEqualsHigh(EnemySpawner.getWaveNumber(),oneWayHighScore,escapeHighScore,lapseHighScore);
                    }
                    break;

                case 7:
                    if (GameManager.getScore() > lapseHighScore)
                    {
                        saveLapseHigh(GameManager.getScore(),oneWayHighScore,escapeHighScore,equalsHighScore);
                    }
                    break;
            }
        }
        else
        {
            switch(LevelManager.getFromLevel())
            {
                case 3:
                    saveOneWayHigh(GameManager.getScore(), 0, 0, 0);
                    break;

                case 5:
                    saveEscapeHigh(GameManager.getScore(), 0, 0, 0);
                    break;

                case 6:
                    saveEqualsHigh(EnemySpawner.getWaveNumber(),0,0,0);
                    break;

                case 7:
                    saveLapseHigh(GameManager.getScore(),0,0,0);
                    break;
            }
        }
    }

    public static float setHighScoreDisplay()
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter myBinaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.OpenOrCreate);
            veesData obj = (veesData)myBinaryFormatter.Deserialize(file);
            file.Close();
            switch (LevelManager.getFromLevel())
            {
                case 3: return (nihalDecryption(obj.sys));
                case 5: return (nihalDecryption(obj.cache));
                case 6: return (nihalDecryption(obj.config));
                case 7: return (nihalDecryption(obj.tmp));
            }
        }
        return 0;
    }
}

[Serializable]
public class veesData
{
    public float sys;//One Way HighScore
    public float cache;//Escape HighScore
    public float config;//Equals HighScore
    public float tmp;//Lapse HighScore
}