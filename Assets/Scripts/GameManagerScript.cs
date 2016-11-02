using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    float startDay, startMoment;

    public static int MORNING = 0, DAY = 1, EVENING = 2, NIGHT = 3;
    public static int numMoments = 4;
    public static float lengthOfMoment = 50f;
    public static int moment;

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
            //Move to next moment
            moment = (moment + 1) % 4;
            startMoment = Time.time;

            if (moment == MORNING)
            {
                //Reset player disguise
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDisguise>().disguise = PlayerDisguise.MAXDISGUISE;
            }
        }

        //switch ( moment)
        //{
        //    case 0: print("Morning " + elapsed); break;
        //    case 1: print("Day " + elapsed); break;
        //    case 2: print("Evening " + elapsed); break;
        //    case 3: print("Night " + elapsed); break;
        //}

    }
}
