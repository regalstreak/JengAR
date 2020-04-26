using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableTouching : MonoBehaviour {
    public int piecesTouching = 0;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("JengaPiece")) {
            piecesTouching++;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("JengaPiece")) {
            piecesTouching--;
        }
    }

}
