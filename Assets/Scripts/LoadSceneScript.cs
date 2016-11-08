using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;

public class LoadSceneScript : MonoBehaviour {

    public GameObject leaderboard;

    public Text scoreText;
    public Text scoreNameText;
    public Image highlight;
    
    int highlight_default_y = 300;
    int interval = 12;

    public Text nameText;

    public string FileName; // This contains the name of the file. Don't add the ".txt"
                            // Assign in inspector
    private TextAsset asset; // Gets assigned through code. Reads the file.

    public static int NO_DISPLAY = 20;

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
            AppendString(FileName, nameText.text);
            
            // Load game
            SceneManager.LoadScene(1);
        }
    }

    public static void AppendString(string FileName, string appendString)
    {
        StreamWriter writer;
        writer = File.AppendText("Assets/Resources/" + FileName + ".txt");
        writer.Write(appendString);
        writer.Close();
    }

    public void OpenLeaderboard()
    {
        leaderboard.SetActive(true);
        highlight.transform.position = new Vector3(highlight.transform.position.x, highlight_default_y, highlight.transform.position.z);

        // Set ScoreText variable to the values in the file
        asset = Resources.Load(FileName) as TextAsset;
        string leaderText = asset.text;
        string[] scores = leaderText.Split("\n" [0]);
        List<Player> scoreList = new List<Player>();
        
        foreach (string s in scores)
        {
            string[] temp = s.Split(":"[0]);
            if (temp.Length != 2) continue;
            string name = temp[0];
            int score = int.Parse(temp[1]);
            scoreList.Add(new Player(name, score));
        }

        scoreList[scoreList.Count-1].highlight = true;

        scoreList.Sort();

        string nameTextString = "";
        string scoreTextString = "";

        int i = 1;
        foreach (Player p in scoreList)
        {
            if (i > NO_DISPLAY) break;
            nameTextString += i + ". " + p.name + Environment.NewLine;
            scoreTextString += p.score + Environment.NewLine;
            if (p.highlight)
            {
                highlight.transform.Translate(new Vector2(0, -i * interval));
            }
            i++;
        }

        scoreNameText.text = nameTextString;
        scoreText.text = scoreTextString;

    }

    public void CloseLeaderboard()
    {
        leaderboard.SetActive(false);
    }

    public class Player : IComparable<Player>
    {
        public string name;
        public int score;
        public bool highlight;

        public Player(string n, int s)
        {
            name = n;
            score = s;
            highlight = false;
        }

        public int CompareTo(Player other)
        {
            if (other == null)
            {
                return 1;
            }

            //Return the difference in scores.
            return other.score - score;
        }
    }
}
