using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    private Rigidbody2D rb;
	public int speed;
	public Vector2 touchPosition;
	private Vector2 offset;
	private GameController gameController;
	
	void Start() {
		gameController = Object.FindObjectOfType<GameController>();
		rb = GetComponent<Rigidbody2D>();
		
		var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch(0).position;
			offset = new Vector2(touch.x - screenPoint.x, touch.y - screenPoint.y);
		} else {
			var mouse = Input.mousePosition;
			offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		}

		float[] offsetSquared = new float[] {Mathf.Pow(offset.x, 2), Mathf.Pow(offset.y, 2)};
		var hypotenuse = offsetSquared[0] + offsetSquared[1];
		hypotenuse = Mathf.Pow(hypotenuse, 0.5f);
		offset.x = offset.x/hypotenuse;
		offset.y = offset.y/hypotenuse;
		if (gameController.moving == true) {
			speed += 10;
		}
		offset.x = offset.x * speed;
		offset.y = offset.y * speed;
		rb.AddForce(offset);

	}
	// var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
}