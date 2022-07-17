using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class MainCharacter : MonoBehaviour
{
	[SerializeField]
	private List<Weapon> _weapons;

	public List<Weapon> WeaponList => _weapons;
	public static AudioSource mainMusic;
	
	// Start is called before the first frame update
	void Start()
	{
		if (JSAM.AudioManager.GetMusicVolume() != 0.0f)
			mainMusic = JSAM.AudioManager.PlayMusic(Music.main_music);
		else {
			JSAM.AudioManager.SetMusicVolume(1.0f);
			JSAM.AudioManager.SetMusicPlaybackPosition(0);
		}
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
