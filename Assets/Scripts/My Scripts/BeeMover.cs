using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMover : MonoBehaviour {
    // private Rigidbody2D rb;
    public GameObject player;
	public float speed;
	Animator animator;
    // for circle:
    // float angle = 0;
    // float speed=(2*Mathf.PI)/5; //2*PI in degress is 360, so you get 5 seconds to complete a circle
    // float radius=2;

    void Start() {
        // rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update() {

		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
		// player.transform.position
		// ^^^


        // circle logic:
        // angle += speed*Time.deltaTime; //if you want to switch direction, use -= instead of +=
        // rb.velocity.x = Mathf.Cos(angle)*radius;
        // rb.velocity.y = Mathf.Sin(angle)*radius;

    }
	public void deleteThis () {
        Destroy(this.gameObject);
    }
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Player" || coll.collider.tag == "Bullet" || coll.collider.tag == "Scaffold") {
			animator.SetTrigger("BeeDies");
		}
	}

}
