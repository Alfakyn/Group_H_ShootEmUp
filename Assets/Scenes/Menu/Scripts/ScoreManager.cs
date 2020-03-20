using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public List<Score> scoreboard = new List<Score>();
    static JsonWrapper wrapper;
    public Text leaderboard;
    public Text inputText;
    static Transform canvasTransform;
    public static ScoreManager scoreManager;
    static int playerScore = 0;

    void Awake()
    {
        scoreManager = this;    //instantiates a static scoreManager so that non static variables can be accessed
        DontDestroyOnLoad(transform.gameObject);    //Makes it so the ScoreManager stays alive even after the scene is changed
    }

    void Start()
    {
        canvasTransform = transform.GetChild(0);
        canvasTransform.gameObject.SetActive(false);
    }

    public void SetScore(int score)
    {
        playerScore = score;
        Debug.Log("Score: " + playerScore);
        scoreManager.InputPlayerName();
    }

    public void SaveScore()
    {
        Debug.Log("SaveScore");
        Score score = new Score();

        score.name = scoreManager.inputText.text;
        score.score = playerScore;
        SaveToScoreboard(score);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }


    public void InputPlayerName()
    {
        canvasTransform.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Input Name");
    }


    static string Path()
    {
        string path = Application.persistentDataPath + "/scoreboard.json";
        return path;
    }

    public void SaveToScoreboard(Score score)
    {
        //Creates a new file. Might want to change it to look for an already existing file if the file already exists
        //And if the file does exist, load the data from it

        // serializes (converts) Score "data" to json format
        wrapper.scoreboard.Add(score);
        string playerScoreData = JsonUtility.ToJson(wrapper, true);

        //Saves data
        File.WriteAllText(Path(), playerScoreData);
        Debug.Log("Player score was saved to: " + Path());
        Debug.Log("Scoreboard now contains " + wrapper.scoreboard.Count + " scores");

        SortScoreboard();
    }
     

    public void LoadScoreBoard()
    {
        //load all data from "path"
        
        if(File.Exists(Path()))
        {
            Debug.Log("Loading data...");
            scoreboard.Clear();
            string data = File.ReadAllText(Path());
            wrapper = JsonUtility.FromJson<JsonWrapper>(data);

            if (wrapper != null)
            {
                int scoreboardLenght = wrapper.scoreboard.Count;
                for (int i = 0; i < scoreboardLenght; i++)
                {
                    scoreboard.Add(wrapper.scoreboard[i]);
                    Debug.Log("Score loaded: " + scoreboard[i].name + " has " + scoreboard[i].score + " points.");
                }
            }
            else //Do this only if LoadWrapper == null
            {
                Debug.Log("No scores found. New scoreboard wrapper was created");
                wrapper = new JsonWrapper();
            }
        }
        else
        {
            Debug.Log("Unable to read data. File not found in " + Path());
        }
    }

    public static void SortScoreboard()
    {
        //Should loop through the list. Sorts scores with highest score at the top. Should resave scores as well using saveToScoreboard() function

        //for (int i = 0; i < scoreboard.Count; i++)
        //{
            //Compare to next index. if smaller, move down.
        //}
        //scoreboard.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public void DisplayScoreboard()
    {
        leaderboard.text = "Leaderboard: \n\n";
        for (int i = 0; i < scoreboard.Count; i++)
        {
            //Add cap so that it will stop adding scores to text.text after the 10th score
            leaderboard.text += i +1 + ". " + scoreboard[i].name + ":\t" + scoreboard[i].score + "\n";
        }
    }
}

