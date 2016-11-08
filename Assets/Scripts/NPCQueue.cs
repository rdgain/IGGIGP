using UnityEngine;
using System.Collections;

public class NPCQueue : MonoBehaviour {

	public bool in_queue = false;
	public GameObject queue_target;
	public QueueManager queue;

	private bool done_initial_setup = false;
	private NPCDefault movement;
	private NPCInteraction interaction;

	// Use this for initialization
	void Start () {
		movement = GetComponent<NPCDefault> ();
		interaction = GetComponentInChildren<NPCInteraction> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!done_initial_setup && GameManagerScript.moment == GameManagerScript.DAY) {
			// Choose which line to queue for.
			GameObject[] targets = GameObject.FindGameObjectsWithTag("QueueTarget");
			queue_target = targets [Random.Range (0, targets.Length)];
			queue = queue_target.GetComponent<QueueManager> ();
			queue.Add (gameObject);
			in_queue = true;
			done_initial_setup = true;
		}
		if (in_queue) {
			if (queue.Contains (gameObject)) {
				movement.override_destination = true;
				movement.destination = queue.GetPosition (gameObject);
			} else {
				LeaveQueue ();
			}
		}
		if (GameManagerScript.moment != GameManagerScript.DAY) {
			LeaveQueue();
		}
	}

	void LeaveQueue()
	{
		in_queue = false;
		movement.override_destination = false;
	}
}
