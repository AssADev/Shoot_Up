using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeController : MonoBehaviour {
    // Variables :
    public static CameraShakeController instance;

    public float rotationMultiplier = 5f;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;

    void Start() {
        instance = this;
    }

    void LateUpdate() {
        // Decrease the shake :
        if (shakeTimeRemaining > 0) {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-0.5f, 0.5f) * shakePower;
            float yAmount = Random.Range(-0.1f, 0.1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);
        }
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-0.5f, 0.5f));
    }

    public void StartShake(float length, float power) {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
        shakeRotation = power * rotationMultiplier;
    }
}
