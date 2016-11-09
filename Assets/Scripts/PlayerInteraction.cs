using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {

	public Text help_text;
    public Text[] options;
    public Text option1, option2, option3, option4;

    public int numOptions = 4;
    int interactions = 0;

	// Use this for initialization
	void Start () {
        options = new Text[numOptions];
        options[0] = option1;
        options[1] = option2;
        options[2] = option3;
        options[3] = option4;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D c)
	{
		if (c.tag == "Interactive" || c.tag == "Penguin") {
			InteractiveObject o = c.GetComponent<InteractiveObject>();
			o.isActive = true;
            interactions++;
            ShowHelpText (o.text);
		}
	}

	void OnTriggerExit2D (Collider2D c)
	{
		if (c.tag == "Interactive" || c.tag == "Penguin") {
			InteractiveObject o = c.GetComponent<InteractiveObject> ();
			o.isActive = false;
            interactions--;
			HideHelpText ();
		}
	}

	public void ShowHelpText(string text)
	{
		help_text.text = "[space] " + text;
	}

    public void ShowOptionText(int option, string text)
    {
        options[option].text = "[" + (option + 1) + "] " + text;
    }

    public void HideOptionText()
    {
        foreach (Text o in options)
        {
            o.text = "";
        }
    }

	public void HideHelpText()
	{
        if (interactions == 0)
        {
            help_text.text = "";
        }
	}

    public void ForceHideHelpText()
    {
        help_text.text = "";
    }
}
