using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameCanBePaused = true;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public LevelLoader levelLoader;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && GameCanBePaused) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        StartCoroutine(levelLoader.LoadLevel(0));
    }

    public void QuitGame() {
        Application.Quit();
    }
}
