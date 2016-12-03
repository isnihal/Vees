using UnityEngine;
using System.Collections;

public class VolumeManager : MonoBehaviour {

    bool isMuted;

    void Start()
    {
        if (Application.loadedLevel == 0)
        {
            PlayerPrefsManager.setMute(1);
            isMuted = false;
        }
        else
        {
            isMuted = PlayerPrefsManager.isMuted();
            if (isMuted == null)
            {
                isMuted = false;
            }
        }
    }

    public void muteVolume()
    {
        if (!isMuted)
        {
            MusicPlayer.setVolume(0);
            PlayerPrefsManager.setMute(0);
            isMuted = PlayerPrefsManager.isMuted();
        }
        else
        {
            MusicPlayer.setVolume(1);
            PlayerPrefsManager.setMute(1);
            isMuted = PlayerPrefsManager.isMuted();
        }
    }
}
