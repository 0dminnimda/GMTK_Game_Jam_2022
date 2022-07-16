using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainCharacter;

    [SerializeField]
    private GameObject _currentCharacter;
    
    private GameObject _currentCharacterInstance;

    [SerializeField]
    private GameObject[] _characterPrefabs;

    [SerializeField]
    private Sprite[] _characterSprites;

    [SerializeField]
    private GameObject _changerMenu;

    [SerializeField]
    private Button[] _optionsButtons;

    private GameObject[] _currentCharacterPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        _currentCharacterInstance = Instantiate(_currentCharacter, _mainCharacter.transform);
        _currentCharacterPrefabs = new GameObject[3];
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
        List<GameObject> charactersWithoutCurrent = new List<GameObject>();
        List<Sprite> characterSpritesWithoutCurrent = new List<Sprite>();

        for (int i = 0; i < _characterPrefabs.Length; i++)
        {
            if (_characterPrefabs[i].gameObject != _currentCharacter.gameObject)
            {
                charactersWithoutCurrent.Add(_characterPrefabs[i]);
                characterSpritesWithoutCurrent.Add(_characterSprites[i]);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, charactersWithoutCurrent.Count);

            GameObject randomCharacter = charactersWithoutCurrent[randomIndex];
            Sprite randomCharacterSprite = characterSpritesWithoutCurrent[randomIndex];
            _optionsButtons[i].GetComponentInChildren<TMP_Text>().text = randomCharacter.name;
            _optionsButtons[i].GetComponentInChildren<RawImage>().texture = randomCharacterSprite.texture;

            _currentCharacterPrefabs[i] = randomCharacter;

            charactersWithoutCurrent.RemoveAt(randomIndex);
            characterSpritesWithoutCurrent.RemoveAt(randomIndex);
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
