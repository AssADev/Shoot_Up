using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSidePlateforme : MonoBehaviour {
    // Variables :
    private PlatformEffector2D effector2D;

    void Start() {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    void Update() {

        // Change the rotation of the PlatformEffector2D :
        if (Input.GetKey(KeyCode.DownArrow)) {
            effector2D.rotationalOffset = 180f;
        }

        // Reset the rotation of the PlatformEffector2D :
        if (Input.GetKey(KeyCode.UpArrow)) {
            effector2D.rotationalOffset = 0;
        }
    }
}
