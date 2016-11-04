using UnityEngine;
using System.Collections;

public class FriendScript : MonoBehaviour {
	public GameObject text;
	public GameObject player;

	InteractiveObject me;

	// Use this for initialization
	void Start () {
		me = GetComponent<InteractiveObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (me.isActive && Input.GetKeyDown (KeyCode.Space)) {
			ContinueConversation ();
		}
		if (Vector3.Distance (transform.position, player.transform.position) > 5) {
			ShowText ("");
		}
	}

	void ContinueConversation()
	{
		if (GameManagerScript.moment == GameManagerScript.MORNING)
			ShowText ("Hi!\nCome back in the evening for fish.");
		if (GameManagerScript.moment == GameManagerScript.EVENING) {
			ShowText ("Welcome back! Have a fish.");
		}
	}

	void ShowText(string s)
	{
		text.GetComponent<TextMesh> ().text = s;
	}
}
