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


    void Update()
    {
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
        if (!pauseGameScreen.active && player != null)
            Time.timeScale = 1;
    }

    public void RestartGame() {
        JSAM.AudioManager.SetMusicVolume(0.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu() {
        JSAM.AudioManager.SetMusicVolume(0.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void PauseGame() {
        Time.timeScale = 0;
        pauseGameScreen.SetActive(true);
    }
    public void ResumeGame() {
        pauseGameScreen.SetActive(false);
    }
}
