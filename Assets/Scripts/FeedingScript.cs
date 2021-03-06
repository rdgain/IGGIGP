﻿using UnityEngine;
using System.Collections;

public class FeedingScript : MonoBehaviour
{
    public string FeedText;
    TextMesh t;
    GameObject player;
    public bool feedPlayer = false;

    public string who;

    PlayerInteraction playerInteraction;
    FishScript fishScript;
    InteractiveObject me;

    float startTime = 0f;
    public bool fed;
    public static float DURATION = 2f;

    public bool startedFeeding;

    // Use this for initialization
    void Start()
    {
        me = GetComponent<InteractiveObject>();
        t = GameObject.Find(FeedText).GetComponent<TextMesh>();
        player = GameObject.FindGameObjectWithTag("Player");
        fishScript = player.GetComponent<FishScript>();
        playerInteraction = player.GetComponent<PlayerInteraction>();
        fed = false;
        startedFeeding = false;
    }

    // Update is called once per frame
    void Update()
    {
        // loop until they feed the penguin (or cancel action)
        if (startedFeeding)
        {
            playerInteraction.HideHelpText();
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ReduceHunger(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ReduceHunger(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ReduceHunger(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ReduceHunger(3);
            }
            else if (Input.GetKeyDown(KeyCode.Escape)) // use Escape to cancel action
            {
                startedFeeding = false;
                ShowText("");
                playerInteraction.ForceHideHelpText();
                playerInteraction.HideOptionText();
            }
        }

        if (!startedFeeding && me.isActive && Input.GetKeyDown(KeyCode.Space))
        {
            Feed();
            startedFeeding = true;
            startTime = Time.time;
        }

        if (startedFeeding || fed && (Time.time - startTime > DURATION))
        {
            ShowText("");
            fed = false;
        }
    }

    void Feed()
    {
        // Check if player has fish in inventory
        if (fishScript.HowMany() > 0)
        {
            // Show options to choose which fish
            for (int i = 0; i < fishScript.fish.Length; i ++)
            {
                if (fishScript.fish[i].has)
                {
                    playerInteraction.ShowOptionText(i, "Choose " + GameManagerScript.MomentToText(i) + " fish");
                }
            }
        } else
        {
            startedFeeding = false;
        }
    }

    void ReduceHunger(int i)
    {
        // Reduce hunger by value of fish and display text
		if (!feedPlayer) {
			if (GetComponent<PlayerHunger> ().hunger + fishScript.fish [i].value < GameManagerScript.MAX_HUNGER)
				GetComponent<PlayerHunger> ().hunger += fishScript.fish [i].value;
			else
				GetComponent<PlayerHunger> ().hunger = GameManagerScript.MAX_HUNGER;
		} else {
			if (player.GetComponent<PlayerHunger> ().hunger + fishScript.fish [i].value < GameManagerScript.MAX_HUNGER)
				player.GetComponent<PlayerHunger> ().hunger += fishScript.fish [i].value;
			else
				player.GetComponent<PlayerHunger> ().hunger = GameManagerScript.MAX_HUNGER;
		}

        ShowText(who + ": Om nom nom");
        fed = true;
        playerInteraction.HideOptionText();
        startedFeeding = false;
        playerInteraction.HideHelpText();
        fishScript.RemoveFish(i);
    }

    void ShowText(string s)
    {
        t.text = s;
    }
}
