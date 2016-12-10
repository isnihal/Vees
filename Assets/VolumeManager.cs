using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeManager : MonoBehaviour {

    static bool isMuted=false;
    public Sprite muteIcon, unMuteIcon;
    public GameObject muteButton;
    

    void Start()
    {
        if(isMuted)
        {
            muteButton.GetComponent<Image>().sprite = muteIcon;
        }

        else
        {
            muteButton.GetComponent<Image>().sprite = unMuteIcon;
        }
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
}
