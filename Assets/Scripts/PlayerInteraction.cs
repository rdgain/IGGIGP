using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {

	public Text help_text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Interactive") {
			InteractiveObject o = c.GetComponent<InteractiveObject> ();
			o.isActive = true;
			ShowHelpText (o.text);
		}
	}

	void OnTriggerExit2D (Collider2D c)
	{
		if (c.tag == "Interactive") {
			InteractiveObject o = c.GetComponent<InteractiveObject> ();
			o.isActive = false;
			HideHelpText ();
		}
	}

	void ShowHelpText(string text)
	{
		help_text.text = "[space] " + text;
	}

	void HideHelpText()
	{
		help_text.text = "";
	}
}
