﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeManager : MonoBehaviour {

    static bool isMuted=false;
    public Sprite muteIcon, unMuteIcon;
    public GameObject muteButton;
    

    void Start()
    {
        setMuteButtonSprite();
    }
    public void muteGame()
    {
        if(isMuted)
        {
            isMuted = false;
            muteButton.GetComponent<Image>().sprite = unMuteIcon;
            MusicPlayer.setVolume(0.5f);
        }

        else
        {
            isMuted = true;
            muteButton.GetComponent<Image>().sprite = muteIcon;
            MusicPlayer.setVolume(0f);
        }
    }

    public static bool getIsMuted()
    {
        return isMuted;
    }

    void setMuteButtonSprite()
    {
        if (isMuted)
        {
            muteButton.GetComponent<Image>().sprite = muteIcon;
        }

        else
        {
            muteButton.GetComponent<Image>().sprite = unMuteIcon;
        }
    }
    public static void setMusicPlayerOnIfSilent()
    {
        if (MusicPlayer.getVolume() == 0)
        {
            MusicPlayer.setVolume(0.5f);
        }
    }
}
