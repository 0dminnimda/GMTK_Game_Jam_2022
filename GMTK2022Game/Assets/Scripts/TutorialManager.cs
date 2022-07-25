using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _popUps;
    private int _popUpIndex = 0;

    [SerializeField]
    private GameObject _enemyOnMap;

    [SerializeField]
    private InventoryManager _player;

    [SerializeField]
    private GameObject _tutorialBounds;

    [SerializeField]
    private GameObject _pickUps;

    [SerializeField]
    private GameObject _level;

    [SerializeField]
    private Animator _lightsAnim;

    [SerializeField]
    private float _waitTillStart = 1f;

    [SerializeField]
    private int _maxWeapons = 2;

    private void Update()
    {
        if (_popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                Progress();
        }
        else if (_popUpIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                Progress();
        }
        else if (_popUpIndex == 2)
        {
            _pickUps.SetActive(true);

            int left = _maxWeapons;

            foreach (var i in _player.Items)
                if (i != null)
                    left--;

            if (left <= 0)
                Progress();
        }
        else if (_popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Progress();
        }
        else if (_popUpIndex == 4)
        {
            if (_enemyOnMap != null)
                _enemyOnMap.SetActive(true);
            else
                Progress();
        }
        else if (_popUpIndex == 5)
        {
            _popUpIndex++;  // special last progress
            StartCoroutine(LoadGame(_popUpIndex - 1));
        }
    }

    void Progress()
    {
        _popUps[_popUpIndex].SetActive(false);
        _popUpIndex++;
        if (_popUpIndex < _popUps.Length)
            _popUps[_popUpIndex].SetActive(true);
    }

    private IEnumerator LoadGame(int index)
    {
        yield return new WaitForSeconds(_waitTillStart);
        _lightsAnim.Play("Tutorial Lights");
        _level.SetActive(true);
        _tutorialBounds.SetActive(false);
        gameObject.SetActive(false);
        _popUps[index].SetActive(false);
    }
}
