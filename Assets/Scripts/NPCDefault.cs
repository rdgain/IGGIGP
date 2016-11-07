using UnityEngine;
using System.Collections;

public class NPCDefault : MonoBehaviour {

	public float destination;
	public bool override_destination = false;
	public float distance_threshold = 1f;
	public bool chasing = false;
	public float speed = 0.5f;
	public float run_speed = 1f;

	private bool facing_right = true;
	private GameObject player;
	private PlayerDisguise disguise;
	private Rigidbody2D body;
	private float reconsider_goal_time;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		disguise = player.GetComponent<PlayerDisguise> ();
		body = GetComponent<Rigidbody2D> ();
		reconsider_goal_time = Time.time + 10;
	}
	
	/** Update is called once per frame.
	 * First, check LoS on penguin for disguise purposes.
	 * Second, chase penguin if needed.
	 * Otherwise, wander randomly.
	 * Finally, update sprite and/or animation.
	 **/
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast (transform.position, facing_right ? Vector2.right : Vector2.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("Player"));
		if (hit.collider) {
			// We can see the player.
			PlatformerPlayerMovement player = hit.collider.GetComponent<PlatformerPlayerMovement>();
			if (player.sliding || !player.can_jump) {
				// Is that...?
				disguise.DecreaseDisguise();
			}
			chasing = disguise.disguise < 0;
		}

		if (chasing) {
			destination = player.transform.position.x;
			body.velocity = new Vector2 (destination > transform.position.x ? run_speed : -run_speed, 0);
		} else {
			if (Mathf.Abs (destination - transform.position.x) < distance_threshold || Time.time > reconsider_goal_time) {
				destination = transform.position.x + Random.value * 10 - 5;
				reconsider_goal_time = Time.time + 10;
			}
			else
				body.velocity = new Vector2 (destination > transform.position.x ? speed : -speed, 0);
		}

		if (body.velocity.x > 0)
			facing_right = true;
		if (body.velocity.x < 0)
			facing_right = false;

		GetComponentInChildren<SpriteRenderer> ().flipX = !facing_right;
	}
}
