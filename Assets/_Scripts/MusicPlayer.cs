using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    //Music player,Splash screen sound independent

    [Tooltip("Music clips played through each levels")]
    public AudioClip[] gameMusicArray;
    static AudioSource musicPlayer;
    int musicIndex;

	// Use this for initialization
	void Start () {
        //Persist music player through each level
        DontDestroyOnLoad(gameObject);
        musicPlayer = GetComponent<AudioSource>();
	}
	
    void OnLevelWasLoaded(int level)
    {
        //Executes on loading of each level;
        musicIndex = level - 1;
        musicPlayer.clip = gameMusicArray[musicIndex];
        if (musicPlayer.clip)
        {
            musicPlayer.loop = true;
            musicPlayer.playOnAwake = true;
            musicPlayer.Play();
        }
        else
        {
            Debug.LogError("Music Clip not found");
        }
    }

    public static void setVolume(float volume)
    {
        musicPlayer.volume = volume;
    }
}
