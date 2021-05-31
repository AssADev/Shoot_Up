using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    // Variables :
    public Transform targetToFollow;

    void Update() {
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);

        // Follow the player (Only down) :
        if (targetToFollow.position.y < transform.position.y) {
            Vector3 newPos = new Vector3(transform.position.x, targetToFollow.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
