using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMoveUp : MonoBehaviour {
    private GameObject gameObj;
    private Rigidbody2D rb;
    public float thrust;
    public float nextPlatform;
    public bool moving;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        moving = false;
    }

    void Update() {
        if(rb.transform.position.y < nextPlatform) {
            transform.Translate(Vector3.up * thrust);
            moving = true;
        }
        else {
            moving = false;
        }
    }
}