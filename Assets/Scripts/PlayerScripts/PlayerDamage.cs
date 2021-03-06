using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDamage : MonoBehaviour {
    // Variables :
    public float maxHealth = 8;
    public float currenthHealth;

    public Image healthBar;
    public TextMeshProUGUI healthIndicator;
    public bool readyToBeHit = true;

    // Song :
    private AudioSource audioSource;
    public AudioSource audioSourceCamera;
    public AudioClip dieSong, hitSong, healthPotionSong, dieMusicBackground;

    // Die Menu :
    public GameObject dieMenuUI;
    public GameObject player;
    public GemsCounter gemsCounter;
    public TextMeshProUGUI gemsScore;

    private Animator _animator;

    void Start() {
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currenthHealth = maxHealth;
        UpdateHealth();
        dieMenuUI.SetActive(false);
    }

    void UpdateHealth() {
        healthBar.fillAmount = currenthHealth / maxHealth;
        healthIndicator.SetText("{0}/{1}", currenthHealth, maxHealth);
    }

    // Health Potion :
    void OnCollisionEnter2D(Collision2D other) {
        if ((other.gameObject.tag == "HealthPotion") && (currenthHealth < maxHealth)) {
            Destroy(other.gameObject);
            currenthHealth++;

            audioSource.clip = healthPotionSong;
            audioSource.Play();
        } else if (other.gameObject.tag == "Enemy") {
            if (readyToBeHit) {
                currenthHealth--;
                 // Die :
                if (currenthHealth <= 0) {
                    readyToBeHit = false;
                    PauseMenu.GameCanBePaused = false;

                    audioSource.clip = dieSong;
                    audioSource.Play();

                    audioSourceCamera.clip = dieMusicBackground;
                    audioSourceCamera.Play();

                    _animator.SetTrigger("PlayerDead");
                    StartCoroutine(DieInterface());

                    // Désactiver les scripts :
                    player.GetComponent<PlayerController>().enabled = false;
                    player.GetComponent<ShootBullet>().enabled = false;
                    player.GetComponent<GemsCollector>().enabled = false;
                }

                // TakeDamage :
                if (currenthHealth > 0) {
                    audioSource.clip = hitSong;
                    audioSource.Play();

                    _animator.SetTrigger("PlayerHit");
                    StartCoroutine(UpdateReadyToBeHit());

                } 
            }
        }
        UpdateHealth();
    }

    IEnumerator UpdateReadyToBeHit() {
        readyToBeHit = false;
        yield return new WaitForSeconds(0.75f);
        readyToBeHit = true;
    }

    IEnumerator DieInterface() {
        yield return new WaitForSeconds(1f);
        gemsScore.SetText("SCORE : {0}", gemsCounter.gemsCounterScore);
        dieMenuUI.SetActive(true);

        // Désactiver le Player :
        player.SetActive(false);
    }
}
