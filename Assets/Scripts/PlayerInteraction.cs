﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {

	Text help_text;
    public Text[] options;
    public Text option1, option2, option3, option4;

    public int numOptions = 4;
    int interactions = 0;

	// Use this for initialization
	void Start () {
        help_text = GameObject.Find("Help Text").GetComponent<Text>();

        option1 = GameObject.Find("Option1").GetComponent<Text>();
        option2 = GameObject.Find("Option2").GetComponent<Text>();
        option3 = GameObject.Find("Option3").GetComponent<Text>();
        option4 = GameObject.Find("Option4").GetComponent<Text>();

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
        interactions = 0;
        help_text.text = "";
    }
}
