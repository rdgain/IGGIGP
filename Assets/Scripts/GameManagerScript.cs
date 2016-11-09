using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    float startDay, startMoment;
    public static int dayCount = 1;
	public GameObject player;
	public GameObject NPC;
    public GameObject playerSpawn;
    public TextMesh dayCountText;

	public GameObject day_fish;

    public const int MORNING = 0, DAY = 1, EVENING = 2, NIGHT = 3;
    public static int numMoments = 4;
    public static float lengthOfMoment = 20f;
    public static int moment;

	public static float time; // range: 0 to lengthOfMoment
    public static int MAX_HUNGER = 100, HUNGER_RATE = 200;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        moment = MORNING;
        startDay = Time.time;
        startMoment = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        float elapsed = Time.time - startMoment;

        if (elapsed > lengthOfMoment)
        {
			NextMoment ();
        }

        //switch ( moment)
        //{
        //    case 0: print("Morning " + elapsed); break;
        //    case 1: print("Day " + elapsed); break;
        //    case 2: print("Evening " + elapsed); break;
        //    case 3: print("Night " + elapsed); break;
        //}

		time = moment * lengthOfMoment + elapsed;
    }
    
    public static string MomentToText(int moment)
    {
        switch (moment)
        {
            case 0: return "MORNING";
            case 1: return "DAY";
            case 2: return "EVENING";
            case 3: return "NIGHT";
            default: return "UNDEFINED";
        }
    }

	public void NextDay()
	{
		moment = NIGHT;
		// Reset NPCs.
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("NPC")) {
			if (g.activeSelf) {
				Destroy (g);
			}
		}
		// TODO adjust hunger
		NextMoment ();
	}

	public void NextMoment()
	{
		//Move to next moment
		moment = (moment + 1) % 4;
		startMoment = Time.time;

		day_fish.SetActive (false);

		switch (moment)
		{
		case MORNING:
            //Reset player disguise
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerDisguise> ().ResetDisguise ();
			player.transform.position = playerSpawn.transform.position;
			dayCount++;

			//Update UI day count
			GameObject.Find("DayCountText").GetComponent<TextMesh>().text = "Day: " + dayCount;

			//Reset fish
			for (int i = 0; i < player.GetComponent<FishScript> ().fish.Length; i++) {
				player.GetComponent<FishScript> ().fish [i].has = false;
			}
			break;
		case DAY:
			// Day things:
			// Spawn NPCs near the market, and add queueing scripts.
			GameObject market_spawn = GetSpawn ("MarketSpawn");
			for (int i = 0; i < 10; i++) {
				GameObject npc = Instantiate (NPC);
				npc.transform.position = market_spawn.transform.position;
			}
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("NPC")) {
				if (g.transform.parent == null)
					g.AddComponent<NPCQueue> ();
			}
			day_fish.SetActive (true);
			break;
		}
	}

	GameObject GetSpawn(string tag)
	{
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		GameObject[] possible_spawns = GameObject.FindGameObjectsWithTag (tag);
		GameObject spawn = null;
		float max_distance = 0f;
		foreach (GameObject g in possible_spawns) {
			float distance = Vector2.Distance (player.transform.position, g.transform.position);
			if (distance > max_distance) {
				max_distance = distance;
				spawn = g;
			}
		}
		return spawn;
	}
}
