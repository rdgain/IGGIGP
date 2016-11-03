using UnityEngine;
using System.Collections;

public class PlatformerPlayerMovement : MonoBehaviour {

	public float move_speed;
	public float step_length;
	public float slide_speed;
	public float fall_speed;

	public float gravity = 1;

	private Vector2 velocity = new Vector2();
	private bool stepping = false;
	private float to_step;
	private bool can_jump = false;

	// Use this for initialization
	void Start () {
		to_step = step_length;
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw ("Horizontal");
		if ((h > 0.1f || h < -0.1f) && !stepping && !CannotMove(h)) {
			stepping = true;
			velocity.x = h*move_speed;
		}
		GetComponent<Animator> ().SetBool ("Step", stepping);

		if (!can_jump) {
			velocity.y = -fall_speed;
		} else {
			velocity.y = 0f;
		}

		// Update position according to velocity.
		float move_x = velocity.x * Time.deltaTime;
		transform.Translate(new Vector3(move_x, velocity.y * Time.deltaTime, 0));

		to_step -= Mathf.Abs(move_x);
		if (to_step < 0f) {
			to_step = step_length;
			stepping = false;
			velocity.x = 0f;
		}
	}

	bool CannotMove(float h)
	{
		RaycastHit2D hit = Physics2D.Linecast (new Vector2(transform.position.x, transform.position.y), new Vector2 (transform.position.x + h * step_length, 0), 1 << LayerMask.NameToLayer ("Geometry"));
		print(hit.collider);
		return hit.collider != null;
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if (c.gameObject.tag == "Floor") {
			can_jump = true;
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		if (c.gameObject.tag == "Floor") {
			can_jump = false;
		}
	}
}
