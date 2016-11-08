using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour {

    public Fish[] fish; // array of fish, one for each moment of day / location
    public static int FISH_VALUE = 10; // how many points of health the basic fish restores

	// Use this for initialization
	void Start () {
        // initialize fish
        fish = new Fish[GameManagerScript.numMoments];	
        for (int i = 0; i < fish.Length; i++)
        {
			fish[i] = new Fish(false, i * FISH_VALUE);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // adds a fish in inventory; need to specify moment / location where it was collected
    // returns how many fish in inventory
    public int AddFish(int moment)
    {
        fish[moment].has = true;
        return HowMany();
    }

    // removes a fish in inventory; need to specify corresponding moment / location
    // returns how many fish in invetory
    public int RemoveFish(int moment)
    {
        fish[moment].has = false;
        return HowMany();
    }

    // removes the most valuable fish from inventory
    public int RemoveLast()
    {
        if (HowMany() > 0)
        {
            for (int i = fish.Length - 1; i >= 0; i--)
            {
                if (fish[i].has)
                {
                    fish[i].has = false;
                    break;
                }
            }
        }
        return HowMany();
    }

    // removes all fish in inventory
    public void RemoveAll()
    {
        if (HowMany() > 0)
        {
            foreach (Fish f in fish)
            {
                f.has = false;
            }
        }
    }

    // returns the number of fish in inventory
    public int HowMany ()
    {
        int count = 0;
        foreach (Fish f in fish)
        {
            if (f.has) count++;
        }
        return count;
    }

    // container class for fish, has 2 values: a boolean to note if the player has the fish in inventory 
    // and the value of the fish
    public class Fish 
    {
        public bool has;
        public int value;

        public Fish(bool h, int v) { has = h; value = v; }
    }
}
