using UnityEngine;
using System.Collections;

public class PlayerDisguise : MonoBehaviour {

    public static int MAXDISGUISE = 5;
    public int disguise;

	// Use this for initialization
	void Start () {

        disguise = MAXDISGUISE;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DecreaseDisguise()
    {
        if (disguise > 0) disguise--;
        else
        {
            // Move player to home and reset disguise
            transform.position = GameObject.Find("PlayerSpawn").transform.position;
            disguise = MAXDISGUISE;
        }
    }
}
