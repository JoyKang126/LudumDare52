using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame(int sceneID)
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(sceneID);
    }

    public void NextLevel()
    {
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
