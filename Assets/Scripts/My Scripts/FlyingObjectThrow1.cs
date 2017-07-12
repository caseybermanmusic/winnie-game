using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObjectThrow1 : MonoBehaviour {
    private Rigidbody2D rb;
    public Vector2 throwVector;
    public float thrust;
    private GameObject scaffold;
    public float HowMuchDrag;

    public GameObject Bee;

    public float TimeBeforeExplode;
    private bool Exploding = false;

    Animator animator;
    public PlayerController playerController;
    public CharMoveUp charMoveUp;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerController = Object.FindObjectOfType<PlayerController>();
        // gameController = Object.FindObjectOfType<GameController>();
        scaffold = GameObject.FindWithTag("Scaffold");

        if (rb.position.x > 0) {
            throwVector.x = -throwVector.x;
        }
        animator = GetComponent<Animator>();
        rb.AddForce(throwVector);
        charMoveUp = Object.FindObjectOfType<CharMoveUp>();

    }

    void OnCollisionEnter2D(Collision2D coll) {



        // || coll.collider.tag == "Scaffold" 
        // ^^^^ look into why it's detecting collision on the way up!



        if (coll.collider.tag == "Bullet") {
            animator.SetTrigger("HitGround");
        }
        if (coll.collider.tag == "Scaffold" && charMoveUp.moving == true) {
            animator.SetTrigger("HitGround");
        }
    }

    public void deleteThis () {
        Destroy(this.gameObject);
    }

    public void SpawnBeeTrigger() {
        Exploding = true;
        animator.SetTrigger("SpawnTheBee");
    }

    public void SpawnBee() {
        Instantiate(Bee, rb.position, Quaternion.identity);
        deleteThis();
    }

    void Update() {
        if (TimeBeforeExplode > 0) {
            TimeBeforeExplode -= Time.deltaTime;
        }
        if (TimeBeforeExplode <= 0.0f && Exploding == false) {
            SpawnBeeTrigger();
        }
        if (rb.position.y > scaffold.transform.position.y && rb.velocity.y < .01f) {
            rb.drag = HowMuchDrag;
        }
    }
}