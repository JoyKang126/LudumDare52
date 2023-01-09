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
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(sceneName);
    }

    public void NextLevel()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
