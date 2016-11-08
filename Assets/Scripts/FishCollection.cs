using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishCollection : MonoBehaviour {

	private InteractiveObject interaction;
	private GameObject player;

	public Slider slider;
	public float steal_time = 5f;

	private float stolen = 0f;

	// Use this for initialization
	void Start () {
		interaction = GetComponent<InteractiveObject> ();
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (interaction.isActive && Input.GetAxis ("Jump") > 0.1)
			Steal ();
		else {
			slider.value = 0;
			stolen = 0;
			player.GetComponent<PlatformerPlayerMovement> ().stealing = false;
			slider.gameObject.SetActive (false);
		}
	}

	void Steal()
	{
		slider.gameObject.SetActive (true);
		stolen += Time.deltaTime;
		slider.value = stolen / steal_time;
		player.GetComponent<PlatformerPlayerMovement> ().stealing = true;
		if (slider.value > 0.99) {
			player.GetComponent<FishScript> ().AddFish (GameManagerScript.moment);
			slider.value = 0;
			stolen = 0;
			player.GetComponent<PlatformerPlayerMovement> ().stealing = false;
			slider.gameObject.SetActive (false);
			GameObject.Find ("Help Text").GetComponent<Text> ().text = "";
			gameObject.SetActive (false);
		}
	}
}
