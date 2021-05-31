using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
    // Variables :
    public Animator _animator;

    public IEnumerator LoadLevel(int sceneIndex) {
        _animator.SetTrigger("TransitionStart");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
