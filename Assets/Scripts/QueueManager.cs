using UnityEngine;
using System.Collections;

public class QueueManager : MonoBehaviour {

	public float queue_distance = 0.1f; /** Distance between members of the queue **/
	public Vector2 queue_direction = Vector2.right;
	public float queue_time = 2f;
	private ArrayList queue = new ArrayList();
	private float next_time;

	// Use this for initialization
	void Start () {
		next_time = Time.time + queue_time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > next_time) {
			Next ();
		}
	}

	public float GetPosition(GameObject g)
	{
		int index = queue.IndexOf (g);
		return transform.position.x + queue_distance * (queue_direction == Vector2.right ? index : -index);
	}

	public void Add(GameObject g)
	{
		queue.Add (g);
	}

	public void Next()
	{
		if (queue.Count > 0)
			queue.RemoveAt (0);
		next_time = Time.time + queue_time;
	}

	public bool Contains (GameObject g)
	{
		return queue.Contains (g);
	}
}
