using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaPlacement : Singleton<JengaPlacement> {
    public GameObject tableToSpawn;

    [HideInInspector]
    public bool placedTable = false;
    public Vector3 tablePosition;
    public Quaternion tableRotation;

    private PlacementIndicator placementIndicator;

    void Awake() {
        placementIndicator = FindObjectOfType<PlacementIndicator>();
    }

    void Update() {
        if (!placedTable && Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            Debug.Log("testing");
            GameObject table = Instantiate(tableToSpawn, placementIndicator.transform.position, placementIndicator.transform.rotation);
            if (table) {
                tablePosition = placementIndicator.transform.position;
                tableRotation = placementIndicator.transform.rotation;
                // tableRotation.y += 0.0313f;
                placedTable = true;
                placementIndicator.gameObject.SetActive(false);
            }
        }
    }
}
