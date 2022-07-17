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
    private GameObject _player;

    private bool _isEnemyActive = false;

    private void Update()
    {
        if (_popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                _popUpIndex++;
        }
        else if (_popUpIndex == 1)
        {
            _popUps[0].SetActive(false);
            _popUps[1].SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftShift))
                _popUpIndex++;
        }
        else if (_popUpIndex == 2)
        {
            _popUps[1].SetActive(false);
            _popUps[2].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
                _popUpIndex++;
        }
        else if (_popUpIndex == 3)
        {
            if (_isEnemyActive)
                return;

            _popUps[2].SetActive(false);
            _popUps[3].SetActive(true);

            _enemyOnMap.SetActive(true);
            _enemyOnMap.GetComponent<Health>().OnDeath.AddListener(() => _popUpIndex++);
            _enemyOnMap.transform.position = new Vector3(0f, 0f);

            _isEnemyActive = true;
        }
        else if (_popUpIndex == 4)
        {
            _popUps[3].SetActive(false);
            _popUps[4].SetActive(true);

            StartCoroutine(LoadGame());
        }
    }
    private IEnumerator LoadGame() 
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
