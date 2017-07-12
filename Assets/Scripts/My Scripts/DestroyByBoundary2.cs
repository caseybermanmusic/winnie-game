using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary2 : MonoBehaviour {

	Animator animator;

	void Start() {
		animator = GetComponent<Animator>();
	}

	// public float destroyWait;
    public void deleteThis () {
        Destroy(this.gameObject);
    }

	// cheap: when something goes off screen it will get deleted
	void OnBecameInvisible() {
		Destroy(this.gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (this.gameObject.tag == "Bullet") {
			if (coll.collider.tag != "Bullet") {
				animator.SetTrigger("HitGround");
			}
		} else {
			if (coll.collider.tag == "Bullet" || coll.collider.tag == "Scaffold" || coll.collider.tag == "Player") {
				animator.SetTrigger("HitGround");
			}
		}

    }
}
