using UnityEngine;
using System.Collections;

public class PlayerDisguise : MonoBehaviour {

    public static int MAX_DISGUISE = 5, MAX_DELAY = 10;
    public int delay;
	public float disguise;
	public float loss_rate = 0.1f;

    public string disguise_name1, disguise_name2, disguise_name3, disguise_name4, disguise_name5;

    GameObject first, first_ui, first_ui1;
    GameObject second, second_ui, second_ui1;
    GameObject third, third_ui, third_ui1;
    GameObject fourth, fourth_ui, fourth_ui1;
    GameObject fifth, fifth_ui, fifth_ui1;

    GameObject[] objects, objects_ui, objects_ui1;

    // Use this for initialization
    void Start () {

        disguise = MAX_DISGUISE;
        delay = MAX_DELAY;

        objects = new GameObject[MAX_DISGUISE];
        objects_ui = new GameObject[MAX_DISGUISE];
        objects_ui1 = new GameObject[MAX_DISGUISE];

        first = GameObject.Find(disguise_name1);
        second = GameObject.Find(disguise_name2);
        third = GameObject.Find(disguise_name3);
        fourth = GameObject.Find(disguise_name4);
        fifth = GameObject.Find(disguise_name5);

        first_ui = GameObject.Find(disguise_name1 + "_ui");
        second_ui = GameObject.Find(disguise_name2 + "_ui");
        third_ui = GameObject.Find(disguise_name3 + "_ui");
        fourth_ui = GameObject.Find(disguise_name4 + "_ui");
        fifth_ui = GameObject.Find(disguise_name5 + "_ui");

        first_ui1 = GameObject.Find(disguise_name1 + "_ui1");
        second_ui1 = GameObject.Find(disguise_name2 + "_ui1");
        third_ui1 = GameObject.Find(disguise_name3 + "_ui1");
        fourth_ui1 = GameObject.Find(disguise_name4 + "_ui1");
        fifth_ui1 = GameObject.Find(disguise_name5 + "_ui1");

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

        objects_ui1[0] = first_ui1;
        objects_ui1[1] = second_ui1;
        objects_ui1[2] = third_ui1;
        objects_ui1[3] = fourth_ui1;
        objects_ui1[4] = fifth_ui1;
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
                objects_ui1[lost_part].SetActive(false);
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

        foreach (GameObject o in objects_ui1)
        {
            o.SetActive(true);
        }
    }
}
