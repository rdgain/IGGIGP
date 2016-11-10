using UnityEngine;
using System.Collections;

public class FishActivationScript : MonoBehaviour {

    SpriteRenderer sr;

    public FishScript fishScript;
    public int ID;

	// Use this for initialization
	void Start () {
		fishScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<FishScript>();
        sr = GetComponent<SpriteRenderer>();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (fishScript.fish[ID].has)
            sr.enabled = true;
        else sr.enabled = false;	
	}
}
