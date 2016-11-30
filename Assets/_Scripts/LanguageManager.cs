using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LanguageManager : MonoBehaviour {

    Text[] text;

    void Start()
    {
        text = FindObjectsOfType<Text>();
    }
    void Update()
    {
        setText();
    }

    void setText()
    {
        //Main Menu
        if (Application.loadedLevel == 1)
        {
            switch (PlayerPrefsManager.getLanguage())
            {
                case "ENGLISH":
                    text[0].text = "VEES";
                    text[1].text = "PLAY";
                    text[2].text = "EARN REWARDS";
                    break;
                case "CHINEESE":
                    text[0].text = "维丝";
                    text[1].text = "玩";
                    text[2].text = "获得奖励";
                    break;
            }
        }

        //Single Player
        else if (Application.loadedLevel==2)
        {
            switch (PlayerPrefsManager.getLanguage())
            {
                case "ENGLISH":
                    text[0].text = "ARCADE";
                    text[1].text = "ONE DIRECTION";
                    text[2].text = "FAST ESCAPE";
                    text[3].text = "EQUALS";
                    text[4].text = "TIME LAPSE";
                    break;
                case "CHINEESE":
                    text[0].text = "拱廊";
                    text[1].text = "一个方向";
                    text[2].text = "快速 逃逸";
                    text[3].text = "等于";
                    text[4].text = "时间间隔";
                    break;
            }
        }

        //Game over
        else if(Application.loadedLevel==8)
        {
            switch (PlayerPrefsManager.getLanguage())
            {
                case "ENGLISH":
                    text[0].text = "GAME OVER";
                    text[2].text = "MAIN MENU";
                    break;
                case "CHINEESE":
                    text[0].text = "游戏结束";
                    text[2].text = "主菜单";
                    break;
            }
        }
    }

    public void changeLanguage(string language)
    {
        PlayerPrefsManager.setLanguage(language);
    }
}

