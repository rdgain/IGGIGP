using UnityEngine;
using System.Collections;

public class Crane : MonoBehaviour {

	public float stage_time;
	public float pause_time;

	public FishScript player_fish;
	public GameObject my_fish;

	public GameObject fish_crate;
	private GameObject my_crate;

	private GameObject start;
	private GameObject intermediate;
	private GameObject end;

	private bool forwards = true;
	private int stage = 0; // 0: lifting, 1: travelling, 2: lowering.
	private Vector2 start_pos;
	private Vector2 above_start_pos;
	private Vector2 above_end_pos;
	private Vector2 end_pos;

	private Vector2 source;
	private Vector2 target;
	private float start_time;
	private bool unloaded = false;

	// Use this for initialization
	void Start () {
		start = GameObject.Find ("CraneStart");
		intermediate = GameObject.Find ("CraneIntermediate");
		end = GameObject.Find ("CraneEnd");

		start_pos = start.transform.position;
		above_start_pos = new Vector2 (start_pos.x, intermediate.transform.position.y);
		end_pos = end.transform.position;
		above_end_pos = intermediate.transform.position;

		transform.position = start_pos;
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManagerScript.moment == GameManagerScript.MORNING && forwards
		    && !player_fish.fish [GameManagerScript.MORNING].has
			&& (stage < 2 || Time.time < start_time + stage_time)) {
			my_fish.SetActive (true);
		} else {
			my_fish.SetActive (false);
		}
		if (Time.time > start_time + stage_time && !unloaded) {
			if (stage == 2 && forwards) {
				my_crate = Instantiate (fish_crate);
				unloaded = true;
			}
		}
		if (Time.time > start_time + stage_time + pause_time) {
				stage++;
			unloaded = false;
			Destroy (my_crate);
			stage = stage % 3;
			start_time = Time.time;
			if (stage == 0)
				forwards = !forwards;
		}
		if (forwards) {
			switch (stage) {
			case 0:
				source = start_pos;
				target = above_start_pos;
				break;
			case 1:
				source = above_start_pos;
				target = above_end_pos;
				break;
			case 2:
				source = above_end_pos;
				target = end_pos;
				break;
			}
		} else {
			switch (stage) {
			case 0:
				source = end_pos;
				target = above_end_pos;
				break;
			case 1:
				source = above_end_pos;
				target = above_start_pos;
				break;
			case 2:
				source = above_start_pos;
				target = start_pos;
				break;
			}
		}
		transform.position = Vector2.Lerp (source, target, (Time.time - start_time)/(stage_time));
	}
}
