using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    float startDay, startMoment;
    public static int dayCount = 1;
	public static GameObject player;

    public static int MORNING = 0, DAY = 1, EVENING = 2, NIGHT = 3;
    public static int numMoments = 4;
    public static float lengthOfMoment = 60f;
    public static int moment;
    public static int MAX_HUNGER = 50, HUNGER_RATE = 200;

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

    }
    
    public static string MomentToText(int moment)
    {
        switch (moment)
        {
            case 0: return "MORNING"; break;
            case 1: return "DAY"; break;
            case 2: return "EVENING"; break;
            case 3: return "NIGHT"; break;
            default: return "UNDEFINED";
        }
    }

	public void NextDay()
	{
		moment = NIGHT;
		// Reset NPCs.
		foreach (GameObject g in GameObject.FindObjectsOfType<GameObject>()) {
			if (g.tag == "NPC" && g.activeInHierarchy) {
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

		if (moment == MORNING)
		{
			//Reset player disguise
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDisguise>().ResetDisguise();
			player.transform.position = GameObject.Find ("PlayerSpawn").transform.position;
			dayCount++;

			//Update UI day count
			GameObject.Find("DayCountText").GetComponent<TextMesh>().text = "Day: " + dayCount;

			//Reset fish
            for (int i = 0; i < player.GetComponent<FishScript>().fish.Length; i++)
            {
                player.GetComponent<FishScript>().fish[i].has = false;
            }
		}
	}
}
