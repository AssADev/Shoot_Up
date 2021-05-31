using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemsCounter : MonoBehaviour {
    // Variables :
    public int gemsCounterScore = 0;
    public TextMeshProUGUI gemsCounterIndicator;

    void Start() {
        UpdateGemsCounter();
    }

    public void UpdateGemsCounter() {
        gemsCounterIndicator.SetText("{0}", gemsCounterScore);
    }
}
