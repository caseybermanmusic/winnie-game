using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public float maxSpeed = 10f;
	public GameObject shot;
	public float speed;
	public Transform shotSpawn;

	public float fireRate;
	private float nextFire;

	public Vector2 touchPosition;
	public bool touchDown;

	public int Health = 5;
	public Text HealthText;
	public CharMoveUp charMoveUp;
	public Rigidbody2D rb;

	private Vector3 ProjectileLaunchOrigin;


	void Start() {
		SetHealth();
		rb = GetComponent<Rigidbody2D>();

	}
	void fireShot() {
		nextFire = Time.time + fireRate;

		// Vector3 myRotation = new Vector3(Random.rotation.x, Random.rotation.y, 0.0f);
		// shotSpawn.rotation

		Instantiate(shot, ProjectileLaunchOrigin, Quaternion.identity);
	}
	void Update() {
		if (Health > 0) {
			if (Input.GetButton("Fire1") && Time.time > nextFire && charMoveUp.moving == false) {
				fireShot();
			}
			if (Input.touchCount > 0 && Time.time > nextFire && charMoveUp.moving == false) {
				fireShot();
			}
			ProjectileLaunchOrigin = new Vector3(rb.transform.position.x + .4f, rb.transform.position.y + .9f, 0.0f);
		}


		// var mouse = Input.mousePosition;
		// var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		// var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		// var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		// transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	void OnCollisionEnter2D(Collision2D coll){
        if (coll.collider == true && coll.collider.tag != "Bullet" && coll.collider.tag !="Scaffold") {
			Health--;
			if (Health <= 0) {
				HealthText.text = "You Lose!";
			} else {
				SetHealth();
			}
        }
    }

	void SetHealth() {
		HealthText.text = "Lives: " + Health.ToString();
	}
}