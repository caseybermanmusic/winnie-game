using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour {
    private Rigidbody2D rb;
    public Vector2 throwVector;
    private GameObject scaffold;
    private GameController gameController;
    // public Rigidbody2D scaffold2;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        gameController = Object.FindObjectOfType<GameController>();
        // scaffold = GameObject.FindWithTag("Scaffold");
        // Debug.Log(scaffold);
        if (rb.position.x > 0) {
            throwVector.x = -throwVector.x;
        }
        rb.AddForce(throwVector);
    }

    void Update() {
        if (gameController.moving == true) {
            rb.isKinematic = true;
        } else {
            rb.isKinematic = false;
        }
    }
}