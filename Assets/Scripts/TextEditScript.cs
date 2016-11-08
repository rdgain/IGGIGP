using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class TextEditScript : MonoBehaviour {

    public string FileName; // This contains the name of the file. Don't add the ".txt"
                            // Assign in inspector
    private StreamWriter writer; // This is the writer that writes to the file

                                 // Use this for initialization
    void Start () {

        int day = GameManagerScript.dayCount;
        GetComponent<Text>().text += day;
        LoadSceneScript.AppendString(FileName, ": " + day.ToString() + Environment.NewLine);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
