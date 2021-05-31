using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootBullet : MonoBehaviour {
    // Variables :
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject playerForJump;
    public TextMeshProUGUI bulletsAmountIndicator;

    public float bulletForce = 20f;
    public int bulletNumber = 8;
    public bool readyToShoot = true;
    public GameObject[] bulletsArray;

    void Start() {
        for (int i = 0; i < bulletNumber; i++) {
            bulletsArray[i].gameObject.SetActive(true);
        }
    }

    void Update() {
        if ((Input.GetKey(KeyCode.UpArrow)) && (bulletNumber > 0) && readyToShoot) {
            bulletNumber--;
            // Add a jump :
            Rigidbody2D playerRigidbody2D = playerForJump.GetComponent<Rigidbody2D>();
            playerRigidbody2D.velocity = Vector2.up * 8f;

            StartCoroutine(Shoot());
            UpdateBulletsIndicator();
            UpdateBulletsHUD(bulletNumber);
        }
    }

    // Shoot Function :
    IEnumerator Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D _rigidbody = bullet.GetComponent<Rigidbody2D>();
        _rigidbody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        CameraShakeController.instance.StartShake(0.175f, 0.175f);

        // Wait before shoot :
        readyToShoot = false;
        yield return new WaitForSeconds(0.1f);
        readyToShoot = true;
    }

    // Update BulletsAmountIndicator :
    public void UpdateBulletsIndicator() {
        bulletsAmountIndicator.SetText("{0}", bulletNumber);
    }

    // Update Bullets HUD :
    public void UpdateBulletsHUD(int bulletIndex) {
        bulletsArray[bulletIndex].gameObject.SetActive(false);

        for (int i = 0; i < bulletNumber; i++) {
            bulletsArray[i].gameObject.SetActive(true);
        }
    }
}
