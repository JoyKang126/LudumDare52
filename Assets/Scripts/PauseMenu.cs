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
    audioManager.Play("button");
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
  }

  public void Resume()
  {
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    audioManager.Play("button");
  }

  public void Quit(int sceneID)
  {
    Time.timeScale = 1f;
    audioManager.Play("button");
    SceneManager.LoadScene(sceneID);
  }
}
