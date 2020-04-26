using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFriction : MonoBehaviour {

    // The minumum value for the friction
    [Range(0f, 1f)]
    public float minFriction;

    // The maximum value for the friction
    [Range(0f, 1f)]
    public float maxFriction;

    // Start is called before the first frame update
    void Start() {
        BoxCollider mainCollider = GetComponent<BoxCollider>();
        PhysicMaterial physicMaterial = mainCollider.material;
        physicMaterial.dynamicFriction = Random.Range(minFriction, maxFriction);
        physicMaterial.staticFriction = Random.Range(minFriction, maxFriction);
    }
}
