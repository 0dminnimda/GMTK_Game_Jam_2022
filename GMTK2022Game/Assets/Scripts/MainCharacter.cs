using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class MainCharacter : MonoBehaviour
{
	public AudioSource mainMusic;  // public static AudioSource mainMusic;

	void Start()
	{
		if (JSAM.AudioManager.GetMusicVolume() != 0.0f)
			mainMusic = JSAM.AudioManager.PlayMusic(Music.main_music);
		else {
			JSAM.AudioManager.SetMusicVolume(1.0f);
			JSAM.AudioManager.SetMusicPlaybackPosition(0);
		}
	}
}
