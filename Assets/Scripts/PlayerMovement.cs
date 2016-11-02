using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 0.2f;
	public float maxVelocity = 10f;

	private float LEFT = 0f;
	private float RIGHT = 180f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		if (GetComponent<Rigidbody2D> ().velocity.magnitude < maxVelocity) {
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (x * speed, y * speed));
		}

	}
}
