using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField] GameObject pauseMenu;
  public AudioManager audioManager;

  public void Pause()
  {
    FindObjectOfType<AudioManager>().Play("button");
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
  }

  public void Resume()
  {
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    FindObjectOfType<AudioManager>().Play("button");
  }

  public void Quit(int sceneID)
  {
    Time.timeScale = 1f;
    FindObjectOfType<AudioManager>().Play("button");
    SceneManager.LoadScene(sceneID);
  }
}
