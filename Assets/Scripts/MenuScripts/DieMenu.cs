using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieMenu : MonoBehaviour {
    // Variables :
    public LevelLoader levelLoader;

    public void LoadMenu() {
        StartCoroutine(levelLoader.LoadLevel(0));
    }

    public void QuitGame() {
        Application.Quit();
    }
}
