using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour {
    // Variables :
    public GameObject gemDropObject, healthPotionObject;
    public int enemyHealth, enemyDamage, randomMaxGems;
    
    private GameObject player;
    private Animator enemyAnimator;
    private Collider2D enemyCollision;
    private ShootBullet bulletsAmountIndicator;

    void Start() {
        enemyAnimator = GetComponent<Animator>();
        enemyCollision = GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        bulletsAmountIndicator = GameObject.FindWithTag("Player").GetComponent<ShootBullet>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            EnemyTakeDamage();
        }   
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "PlayerFeet") {
            EnemyTakeDamage();

            // Reload :
            if (bulletsAmountIndicator.bulletNumber + 4 >= 8) {
                bulletsAmountIndicator.bulletNumber = 8;
            } else {
                bulletsAmountIndicator.bulletNumber += 4;
            }
            bulletsAmountIndicator.UpdateBulletsIndicator();
            bulletsAmountIndicator.UpdateBulletsHUD(bulletsAmountIndicator.bulletNumber - 1);
            CameraShakeController.instance.StartShake(0.125f, 0.125f);

            // Add a jump :
            Rigidbody2D playerRigidbody2D = player.GetComponent<Rigidbody2D>();
            playerRigidbody2D.velocity = Vector2.up * 10f;
        }
    }

    void EnemyTakeDamage () {
        enemyHealth--;
        if (enemyHealth <= 0) {
            EnemyDie();
        } else {
            enemyAnimator.SetTrigger("isHit");
            CameraShakeController.instance.StartShake(0.125f, 0.125f);
        }
    }

    void EnemyDie() {
        enemyCollision.enabled = false;
        enemyAnimator.SetTrigger("isDead");
        CameraShakeController.instance.StartShake(0.2f, 0.2f);

        // Drop Gems :
        int randomNumberGems = Random.Range(1, randomMaxGems);
        for (int i = 0; i < randomNumberGems; i++) {
            Instantiate(gemDropObject, transform.position, Quaternion.identity);
            CameraShakeController.instance.StartShake(0.075f, 0.075f);
        }

        // Drop Healt Potion :
        if (Random.Range(1, 100) <= 10) {
            Instantiate(healthPotionObject, transform.position, Quaternion.identity);
        }
    }

    public void DestroyEnemyGameObject() {
        Destroy(gameObject);
    }
}
