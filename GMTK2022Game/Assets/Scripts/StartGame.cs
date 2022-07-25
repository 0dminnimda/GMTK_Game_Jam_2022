using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using JSAM;

public class StartGame : MonoBehaviour
{
    void Start()
    {
        // JSAM.AudioManager.SetMusicVolume(0.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
