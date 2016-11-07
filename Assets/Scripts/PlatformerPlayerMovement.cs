using UnityEngine;
using System.Collections;

public class PlatformerPlayerMovement : MonoBehaviour {

	public float move_speed;
	public float step_length;
	public float slide_speed;
	public float turn_speed;
	public float jump_speed;

	public float gravity = 1;

	private static int LEFT = -1;
	private static int RIGHT = 1;

	private Vector2 velocity = new Vector2();
	private bool stepping = false;
	private float to_step;
	public bool can_jump = false;
	public bool sliding = false;
	private bool righting = false;
	private int slide_direction = RIGHT;

	private GameObject sprite;
	private Rigidbody2D body;

	// Use this for initialization
	void Start () {
		to_step = step_length;
		sprite = transform.FindChild ("PlayerSprite").gameObject;
		body = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		velocity.y = GetComponent<Rigidbody2D> ().velocity.y;
		GetComponent<Animator> ().SetBool ("Step", stepping);
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		if (sliding) {
			if (v > 0.1f) {
				righting = true;
				body.isKinematic = true;
			}
			if (righting) {
				/*if (body.rotation > 0) {
					body.rotation -= turn_speed * Time.deltaTime;
				} else {
					body.rotation += turn_speed * Time.deltaTime;
				}*/
				if (body.rotation < turn_speed && body.rotation > -turn_speed) {
					righting = false;
					sliding = false;
					body.isKinematic = false;
					body.freezeRotation = true;
				}
				body.rotation = 0;
			} else if ((h > 0.1 && CanSlide(RIGHT)) ||
				(h < -0.1 && CanSlide(LEFT))) {
				body.AddForce (new Vector2 (h * slide_speed, 0));
			}
		} else {
			if (v < -0.1 && Mathf.Abs(h) > 0.1) {
				sliding = true;
				stepping = false;
				body.freezeRotation = false;
				body.AddTorque (-h*100);
				slide_direction = h < -0.1 ? LEFT : RIGHT;
				return;
			}
			if ((h > 0.1f || h < -0.1f) && !stepping) {
				stepping = true;
				velocity.x = h * move_speed;
				FlipSprite (velocity.x < 0.1f);
			}
			if (v > 0.1f && can_jump) {
				velocity.y = jump_speed;
			}

			// Update position according to velocity.
			float move_x = velocity.x * Time.deltaTime;
			GetComponent<Rigidbody2D> ().velocity = velocity;

			to_step -= Mathf.Abs (move_x);
			if (to_step < 0f) {
				to_step = step_length;
				stepping = false;
				velocity.x = 0f;
			}
		}
	}

	/** Can we slide in the given direction? i.e. is the slide direction correct and are
	 * we lying on our front?
	 **/
	bool CanSlide(int direction)
	{
		if (slide_direction != direction)
			return false;
		return (slide_direction == RIGHT && Util.ConstrainAngle (body.rotation) > -150 && Util.ConstrainAngle(body.rotation) < -45)
			|| (slide_direction == LEFT && Util.ConstrainAngle (body.rotation) < 150 && Util.ConstrainAngle(body.rotation) > 45);
	}

	void FlipSprite(bool flip)
	{
		sprite.GetComponent<SpriteRenderer> ().flipX = flip;
		foreach (SpriteRenderer child in sprite.GetComponentsInChildren<SpriteRenderer>()) {
			child.flipX = flip;
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
