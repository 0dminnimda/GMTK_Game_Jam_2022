using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class CharacterOption
{
	public GameObject Prefab;
	public Texture2D Texture;
}

public class CharacterChanger : MonoBehaviour
{
	[SerializeField]
	private GameObject _mainCharacter;

	[SerializeField]
	private GameObject _currentCharacter;

	private GameObject _currentCharacterInstance;

	[SerializeField]
	private CharacterOption[] _characterOptions;

	[SerializeField]
	private GameObject _changerMenu;

	[SerializeField]
	private Button[] _optionsButtons;

	private GameObject[] _currentCharacterPrefabs;

	// Start is called before the first frame update
	void Start()
	{
		_currentCharacterInstance = Instantiate(_currentCharacter, _mainCharacter.transform);
		_currentCharacterPrefabs = new GameObject[_optionsButtons.Length];
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			RefreshOptions();

			Time.timeScale = 0f;
			_changerMenu.SetActive(true);
		}
	}

	private void RefreshOptions() {
		List<CharacterOption> withoutCurrent = new List<CharacterOption>();

		for (int i = 0; i < _characterOptions.Length; i++)
		{
			if (_characterOptions[i].Prefab != _currentCharacter.gameObject)
			{
				withoutCurrent.Add(_characterOptions[i]);
			}
		}

		for (int i = 0; i < _optionsButtons.Length; i++)
		{
			int randomIndex = Random.Range(0, withoutCurrent.Count);

			CharacterOption randomCharacter = withoutCurrent[randomIndex];
			_optionsButtons[i].GetComponentInChildren<TMP_Text>().text = randomCharacter.Prefab.name;
			_optionsButtons[i].GetComponentInChildren<RawImage>().texture = randomCharacter.Texture;

			_currentCharacterPrefabs[i] = randomCharacter.Prefab;

			withoutCurrent.RemoveAt(randomIndex);
		}
	}

	public void ChooseAnOption(int index) {
		_currentCharacter = _currentCharacterPrefabs[index];

		Destroy(_currentCharacterInstance.gameObject);
		_currentCharacterInstance = Instantiate(_currentCharacter, _mainCharacter.transform);
		
		

		Time.timeScale = 1f;
		_changerMenu.SetActive(false);
	}
}
