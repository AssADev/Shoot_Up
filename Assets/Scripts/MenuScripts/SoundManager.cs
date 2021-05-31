using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    // Variables :
    public Slider volumeSlider;

    void Start() {
        if (!PlayerPrefs.HasKey("musicVolume")) {
            PlayerPrefs.SetFloat("musicVolume", 0.8f);
            LoadVolume();
        } else {
            LoadVolume();
        }
    }

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        SaveVolume();
    }

    private void LoadVolume() {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void SaveVolume() {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
