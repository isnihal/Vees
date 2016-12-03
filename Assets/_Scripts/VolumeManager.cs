using UnityEngine;
using System.Collections;

public class VolumeManager : MonoBehaviour {

    bool isMuted;

    void Start()
    {
        isMuted = false;
    }

    public void muteVolume()
    {
        if (!isMuted)
        {
            MusicPlayer.setVolume(0);
            isMuted = true;
        }
        else
        {
            MusicPlayer.setVolume(1);
            isMuted = false;
        }
    }
}
