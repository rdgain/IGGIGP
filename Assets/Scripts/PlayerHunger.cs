using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHunger : MonoBehaviour {

    public int hunger;
    public int rate;
    public Slider hunger_ui;

    public int hunger_penalty = 10;
    GameManagerScript gameManager;

	// Use this for initialization
	void Start () {
        rate = 0;
        hunger = 0;
        hunger_ui.maxValue = GameManagerScript.MAX_HUNGER;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }
	
	// Update is called once per frame
	void Update () {

        rate++;

        if (rate > GameManagerScript.HUNGER_RATE)
        {
            IncreaseHunger(1);
            rate = 0;
        }

	
	}

    public void HungerPenalty()
    {
        IncreaseHunger(hunger_penalty);
    }

    void IncreaseHunger(int amount)
    {
        if (hunger + amount <= GameManagerScript.MAX_HUNGER)
        {
            hunger += amount;

            // Update hunger UI
            hunger_ui.value = hunger;

        }
        else
        {
            Destroy(gameObject);

            if (gameObject.tag == "Player")
            {
                // Player died, end of game.
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }
            else
            {
                if (GameObject.FindGameObjectsWithTag("Penguin").Length == 0)
                {
                    //All family died, end of game.
                    SceneManager.LoadScene(2, LoadSceneMode.Single);
                } else
                {
                    gameManager.DisplayWarning(gameObject.name + " just died of hunger!");
                }
            }
        }
    }
}
