using UnityEngine;
using System.Collections;

public class FriendScript : MonoBehaviour {
	public GameObject text;
	public GameObject player;

	InteractiveObject me;
    public static int DISTANCE = 5;

	// Use this for initialization
	void Start () {
		me = GetComponent<InteractiveObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (me.isActive && Input.GetKeyDown (KeyCode.Space)) {
			ContinueConversation ();
		}
		if (Vector3.Distance (transform.position, player.transform.position) > DISTANCE) {
			ShowText ("");
		}
	}

	void ContinueConversation()
	{
		if (GameManagerScript.moment == GameManagerScript.MORNING)
			ShowText ("Hi!\nCome back at night for fish.");
		if (GameManagerScript.moment == GameManagerScript.NIGHT) {
			ShowText ("Welcome back! Have a fish.");
            player.GetComponent<FishScript>().fish[GameManagerScript.NIGHT].has = true;
		}
	}

	void ShowText(string s)
	{
		text.GetComponent<TextMesh> ().text = s;
	}
}
