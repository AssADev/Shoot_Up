using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCollector : MonoBehaviour {
    // Variables :
    public GemsCounter gemsCounter;

    // Song :
    private AudioSource audioSource;
    public AudioClip gemCollectedSong;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Gem") {
            Destroy(other.gameObject);
            audioSource.clip = gemCollectedSong;
            audioSource.Play();

            // Update the GemsCounter :
            gemsCounter.gemsCounterScore++;
            gemsCounter.UpdateGemsCounter();
        }
    }
}
