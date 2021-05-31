using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAfterShoot : MonoBehaviour {
    // Variables :
    public GameObject bulletEffectExplosion;
    public GameObject bulletEffectOnGround;

    void OnCollisionEnter2D(Collision2D collision) {
        if ((collision.gameObject.tag != "Bullet") && (collision.gameObject.tag != "Player")) {
            // Custom animations (If ground and if ennemy) :
            if (collision.gameObject.layer == 8) {
                GameObject effect = Instantiate(bulletEffectExplosion, transform.position, Quaternion.identity);
                Destroy(effect, 0.6f);
            } else if (collision.gameObject.layer == 10) {
                GameObject effect = Instantiate(bulletEffectOnGround, transform.position, Quaternion.identity);
                Destroy(effect, 0.4f);
            }
            Destroy(gameObject);
        }
        
    }
}
