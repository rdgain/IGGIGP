﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour {

    public int hunger;
    public int rate;
    public Slider hunger_ui;

	// Use this for initialization
	void Start () {
        rate = 0;
        hunger = 0;
	}
	
	// Update is called once per frame
	void Update () {

        rate++;

        if (rate > GameManagerScript.HUNGER_RATE)
        {
            if (hunger < GameManagerScript.MAX_HUNGER)
            {
                hunger++;

                // Update hunger UI
                hunger_ui.value = hunger;
                
            } else
            {
                // Player died, end of game.
                Destroy(gameObject);
            }
            rate = 0;
        }

	
	}
}
