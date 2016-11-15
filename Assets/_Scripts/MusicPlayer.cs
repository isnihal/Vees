using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

    [Tooltip("Music clips played through each levels")]
    public AudioClip[] gameMusicArray;
    AudioSource musicPlayer;
    int musicIndex;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        musicPlayer = GetComponent<AudioSource>();
	}
	
    void OnLevelWasLoaded(int level)
    {
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
            Debug.LogError("Clip not founded");
        }
    }
}
