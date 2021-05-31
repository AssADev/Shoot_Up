using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsCollector : MonoBehaviour {
    // Variables :
    public GemsCounter gemsCounter;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Gem") {
            Destroy(other.gameObject);

            // Update the GemsCounter :
            gemsCounter.gemsCounterScore++;
            gemsCounter.UpdateGemsCounter();
        }
    }
}
