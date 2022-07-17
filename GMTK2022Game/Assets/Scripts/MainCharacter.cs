using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;

public class MainCharacter : MonoBehaviour
{
	[SerializeField]
	private List<Weapon> _weapons;

	public List<Weapon> WeaponList => _weapons;
	
	public AudioSource mainMusic;
	// Start is called before the first frame update
	void Start()
	{	
		mainMusic = JSAM.AudioManager.PlayMusic(Music.main_music);
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
