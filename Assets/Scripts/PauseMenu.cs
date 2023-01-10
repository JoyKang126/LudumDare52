using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
  [SerializeField] GameObject pauseMenu;

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

  public void Quit()
  {
    Time.timeScale = 1f;
    FindObjectOfType<AudioManager>().Play("button");
    Destroy(FindObjectOfType<AudioManager>().gameObject);
    SceneManager.LoadScene("StartScene");
  }
}
