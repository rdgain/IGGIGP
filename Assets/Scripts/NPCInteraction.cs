using UnityEngine;
using System.Collections;

public class NPCInteraction : MonoBehaviour {

	public float bump_penalty = 1f;
	private PlayerDisguise disguise;

	// Use this for initialization
	void Start () {
		disguise = GameObject.Find ("Player").GetComponent<PlayerDisguise> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player") {
			if (c.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude > 0.1) {
				disguise.disguise -= bump_penalty * Time.deltaTime;
				disguise.DecreaseDisguise ();
			}
		}
	}
}
