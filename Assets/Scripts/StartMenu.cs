using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play("button");
        SceneManager.LoadScene(sceneName);
    }

    public void NextLevel()
    {

        FindObjectOfType<AudioManager>().Play("button");
        if (SceneManager.GetActiveScene().name == "level6")
        {
            FindObjectOfType<AudioManager>().StopPlay("bgm");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().Play("button");
        Destroy(FindObjectOfType<AudioManager>().gameObject);
        SceneManager.LoadScene("StartScene");
    }
}