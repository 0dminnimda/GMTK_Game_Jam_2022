using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class EndGameManager : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private GameObject endGameScreen;

    [SerializeField]
    private GameObject pauseGameScreen;

    [SerializeField]
    private GameObject wonGameScreen;

    [SerializeField]
    private bool loadingLevel;

    void Awake()
    {
        loadingLevel = false;
    }

    void Update()
    {
        if (loadingLevel) return;

        if (player == null) {
            Time.timeScale = 0;
            endGameScreen.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1) {
                PauseGame();
            } else {
                ResumeGame();
            }
        }
    }

    public void RestartGame() {
        JSAM.AudioManager.SetMusicVolume(0.0f);
        Time.timeScale = 1;
        loadingLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu() {
        JSAM.AudioManager.SetMusicVolume(0.0f);
        Time.timeScale = 1;
        loadingLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PauseGame() {
        Time.timeScale = 0;
        pauseGameScreen.SetActive(true);
    }
    public void ResumeGame() {
        Time.timeScale = 1;
        pauseGameScreen.SetActive(false);
    }
}
