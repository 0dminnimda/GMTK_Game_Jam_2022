using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class MainCharacter : MonoBehaviour
{
	public AudioSource mainMusic;
	// Start is called before the first frame update
	void Start()
	{
		mainMusic = JSAM.AudioManager.PlayMusic(Music.main_music);
	}
}
