using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleWall : MonoBehaviour {
    // Variables :
    public Sprite[] breakStages;
    public int objectHealth;
    private int currentHealth;
    // int currentStage = 0;

    SpriteRenderer mySprite;

    void Start() {
        mySprite = GetComponent<SpriteRenderer>();
        currentHealth = objectHealth;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Bullet") {
            currentHealth--;
            mySprite.sprite = breakStages[currentHealth];

            if (currentHealth <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
