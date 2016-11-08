using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour {

	public float bump_penalty = 1f;
	public bool collision_enabled = true;
	private PlayerDisguise disguise;
	private NPCDefault movement;

	// Use this for initialization
	void Start () {
		disguise = GameObject.Find ("Player").GetComponent<PlayerDisguise> ();
		movement = GetComponentInParent<NPCDefault> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if (collision_enabled && c.gameObject.tag == "Player") {
			if (c.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > 0.1) {
				disguise.disguise -= bump_penalty * Time.deltaTime;
				disguise.DecreaseDisguise ();
			}
			if (movement.chasing) {
				// TODO Remove fish.
				// TODO Go to next day.
				GameObject.Find("GameManager").GetComponent<GameManagerScript>().NextDay();
			}
		}
	}
}
