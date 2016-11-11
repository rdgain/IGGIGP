using UnityEngine;
using System.Collections;

public class FriendScript : MonoBehaviour {
	GameObject text;
	GameObject player;

    public string friendText = "FriendText";

	InteractiveObject me;
    public static int DISTANCE = 5;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GameObject.Find(friendText);
		me = GetComponent<InteractiveObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (me.isActive && Input.GetKeyDown (KeyCode.Space)) {
			ContinueConversation ();
		}
		if (Vector3.Distance (transform.position, player.transform.position) > DISTANCE) {
			HideText();

        }
	}

	void ContinueConversation()
	{
		if (GameManagerScript.moment == GameManagerScript.MORNING)
			ShowText ("FRIEND: Hi!\nFRIEND: I hear they are just unloading fish at the docks!");
        if (GameManagerScript.moment == GameManagerScript.DAY)
            ShowText("FRIEND: You look beautiful, friend.\nFRIEND: Can you get me a hat from the market?");
        if (GameManagerScript.moment == GameManagerScript.EVENING)
            ShowText("FRIEND: Did you see that cool restaurant they \njust opened near the docks?");
        if (GameManagerScript.moment == GameManagerScript.NIGHT) {
			ShowText ("FRIEND: Welcome, friend! Have a fish.");
            player.GetComponent<FishScript>().fish[GameManagerScript.NIGHT].has = true;
		}
	}

	void ShowText(string s)
	{
		text.GetComponent<TextMesh> ().text = s;

    }

    void HideText()
    {
        text.GetComponent<TextMesh>().text = "";
    }
}
