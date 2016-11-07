using UnityEngine;
using System.Collections;

public class PlayerDisguise : MonoBehaviour {

    public static int MAX_DISGUISE = 5, MAX_DELAY = 10;
    public int delay;
	public float disguise;
	public float loss_rate = 0.1f;

    public GameObject first, first_ui;
    public GameObject second, second_ui;
    public GameObject third, third_ui;
    public GameObject fourth, fourth_ui;
    public GameObject fifth, fifth_ui;

    GameObject[] objects, objects_ui;

    // Use this for initialization
    void Start () {

        disguise = MAX_DISGUISE;
        delay = MAX_DELAY;

        objects = new GameObject[MAX_DISGUISE];
        objects_ui = new GameObject[MAX_DISGUISE];

        objects[0] = first;
        objects[1] = second;
        objects[2] = third;
        objects[3] = fourth;
        objects[4] = fifth;

        objects_ui[0] = first_ui;
        objects_ui[1] = second_ui;
        objects_ui[2] = third_ui;
        objects_ui[3] = fourth_ui;
        objects_ui[4] = fifth_ui;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void DecreaseDisguise()
    {
        if (disguise > 0)
        {
			GameObject.Find ("Camera").GetComponent<CameraMovement> ().Shake ();
			disguise -= loss_rate*Time.deltaTime;

			int lost_part = Mathf.CeilToInt (disguise);
			if (lost_part < 5) {
				//remove disguise off player
				objects [lost_part].SetActive (false);

				//remove disguise off UI
				objects_ui [lost_part].SetActive (false);
			}
        }
    }

    public void ResetDisguise()
    {
        disguise = MAX_DISGUISE;

        foreach (GameObject g in objects)
        {
            g.SetActive(true);
        }

        foreach (GameObject o in objects_ui)
        {
            o.SetActive(true);
        }
    }
}
