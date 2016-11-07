using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class LoadSceneScript : MonoBehaviour {

    public Text nameText;

    public string FileName; // This contains the name of the file. Don't add the ".txt"
                            // Assign in inspector
    private TextAsset asset; // Gets assigned through code. Reads the file.
    private StreamWriter writer; // This is the writer that writes to the file

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadScene()
    {
        if (!nameText.text.Equals(""))
        {
            // Save name to file
            AppendString(nameText.text);
            
            // Load game
            SceneManager.LoadScene(1);
        }
    }

    void AppendString(string appendString)
    {
        asset = Resources.Load(FileName + ".txt") as TextAsset;
        writer = File.AppendText("Assets/Resources/" + FileName + ".txt");
        writer.WriteLine(appendString);
        writer.Close();
    }
}
