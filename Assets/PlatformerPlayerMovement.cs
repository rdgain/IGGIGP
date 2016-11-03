using UnityEngine;
using System.Collections;

public class PlatformerPlayerMovement : MonoBehaviour {

	public float move_speed;
	public float step_length;
	public float slide_speed;
	public float fall_speed;
	public float jump_speed;

	public float gravity = 1;

	private Vector2 velocity = new Vector2();
	private bool stepping = false;
	private float to_step;
	private bool can_jump = false;

	private GameObject sprite;

	// Use this for initialization
	void Start () {
		to_step = step_length;
		sprite = transform.FindChild ("PlayerSprite").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		velocity.y = GetComponent<Rigidbody2D> ().velocity.y;
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		if ((h > 0.1f || h < -0.1f) && !stepping) {
			stepping = true;
			velocity.x = h*move_speed;
			sprite.GetComponent<SpriteRenderer> ().flipX = velocity.x < 0.1f;
		}
		if (v > 0.1f && can_jump) {
			velocity.y = jump_speed;
		}
		GetComponent<Animator> ().SetBool ("Step", stepping);

		// Update position according to velocity.
		float move_x = velocity.x * Time.deltaTime;
		GetComponent<Rigidbody2D> ().velocity = velocity;

		to_step -= Mathf.Abs(move_x);
		if (to_step < 0f) {
			to_step = step_length;
			stepping = false;
			velocity.x = 0f;
		}
	}

	void OnCollisionStay2D(Collision2D c)
	{
		if (c.gameObject.tag == "Floor") {
			can_jump = true;
		}
	}

	void OnCollisionExit2D(Collision2D c)
	{
		if (c.gameObject.tag == "Floor") {
			can_jump = false;
		}
	}
}
