using UnityEngine;
using System.Collections;

public class FriendScript : MonoBehaviour {

	InteractiveObject me;
	public GameObject text;

	// Use this for initialization
	void Start () {
		me = GetComponent<InteractiveObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (me.isActive && Input.GetKeyDown (KeyCode.Space)) {
			ContinueConversation ();
		}
	}

	void ContinueConversation()
	{
		ShowText ("Hi!");
	}

	void ShowText(string s)
	{
		text.GetComponent<TextMesh> ().text = s;
	}
}
