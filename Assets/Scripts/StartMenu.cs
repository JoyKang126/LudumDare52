using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public void StartGame(string sceneName)
    {
        Time.timeScale = 1f;
        audioManager.Play("button");
        SceneManager.LoadScene(sceneName);
    }

    public void NextLevel()
    {
        audioManager.Play("button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
