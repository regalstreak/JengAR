using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaPlacement : MonoBehaviour {
    public GameObject jengaToSpawn;

    private PlacementIndicator placementIndicator;

    void Start() {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            GameObject obj = Instantiate(jengaToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
    }
}
