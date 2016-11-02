using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 0.2f;
	public float maxVelocity = 10f;

	private static int LEFT = -1;
	private static int NONE = 0;
	private static int RIGHT = 1;

	private int slide_direction = NONE;

	private bool can_jump = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "Floor") {
			can_jump = true;
		}
	}

	void FixedUpdate() {

		float x = Input.GetAxis ("Horizontal");
		float y = Input.GetAxis ("Vertical");

		Rigidbody2D body = GetComponent<Rigidbody2D> ();

		if (body.velocity.magnitude < maxVelocity || slide_direction != NONE) {
			body.AddForce (new Vector2 (x * speed, y * speed));
		}

		if (body.velocity.x < -0.1) {
			transform.parent.gameObject.GetComponent<SpriteRenderer> ().flipX = true;
		}

		if (body.velocity.x > 0.1) {
			transform.parent.gameObject.GetComponent<SpriteRenderer> ().flipX = false;
		}

		if (body.rotation > 45) {
			slide_direction = LEFT;
		} else if (body.rotation < -45) {
			slide_direction = RIGHT;
		} else {
			slide_direction = NONE;
		}

		if (y > 0 && slide_direction != NONE) {
			// Get up
			body.position += new Vector2(0, 1);
			body.rotation = 0;
			transform.parent.GetComponent<Rigidbody2D> ().rotation = 0;
		}
	}
}
