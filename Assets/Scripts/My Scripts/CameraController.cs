using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame, but after everything else is processed
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
