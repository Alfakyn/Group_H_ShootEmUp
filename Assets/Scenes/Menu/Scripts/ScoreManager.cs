using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public List<Score> scoreboard = new List<Score>();
    public static Jsonwrapper wrapper;
    public Text leaderboard, inputText;
    public Canvas canvas;
    public static ScoreManager scoreManager;
    public float playerScore = 0f;
    public int scoreBoardMax;

    void Awake()
    {   
        //instantiates a static scoreManager so that non static variables can be accessed
        scoreManager = this;
        //Makes it so the ScoreManager stays alive even after the scene is changed
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        //If it is Level scene
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            //Then start counting points
            playerScore += (1 * Time.deltaTime);
            Debug.Log("PlayerScore is: " + playerScore);
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
        Debug.Log("PlayerScore = " + playerScore);
    }

    public void StoreScore()
    {
        Debug.Log("SaveScore: " + playerScore);
        PauseMenu.isPausable = false;
        scoreManager.InputPlayerName();
    }

    public void SaveScore()
    {
        Score score = new Score();
        score.name = scoreManager.inputText.text;
        score.score = (int)playerScore;
        SaveToScoreboard(score);

        Time.timeScale = 1f;
        PauseMenu.isPausable = true;
        SceneManager.LoadScene(0);
        Destroy(transform.gameObject);
    }


    public void InputPlayerName()
    {
        scoreManager.canvas.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }


    static string Path()
    {
        string path = Application.persistentDataPath + "/scoreboard.json";
        return path;
    }

    public void SaveToScoreboard(Score score)
    {
        LoadScoreBoard();
        wrapper.scoreboard.Add(score);
        string playerScoreData = JsonUtility.ToJson(wrapper, true);

        //Saves data to json-file
        File.WriteAllText(Path(), playerScoreData);
        Debug.Log("Player score was saved to: " + Path());
        Debug.Log("Scoreboard now contains " + wrapper.scoreboard.Count + " scores");
    }


    public void LoadScoreBoard()
    {
        //load all data from "path"
        if (File.Exists(Path()))
        {
            //Do this only if LoadWrapper == null
            if (wrapper == null)
            {
                Debug.Log("New scoreboard wrapper was created");
                wrapper = new Jsonwrapper();
            }
            if (wrapper != null)
            {
                string data = File.ReadAllText(Path());
                wrapper.scoreboard.Clear();
                wrapper = JsonUtility.FromJson<Jsonwrapper>(data);
                SortScoreboard();
            }
        }
        else
        {
            Debug.Log("Unable to read data. File not found in " + Path());
        }
    }

    public void SortScoreboard()
    {
        //Sorts scores in order: Most points first
        wrapper.scoreboard.Sort((a, b) => b.score.CompareTo(a.score));

        //Deletes extra scores if there are too many saved scores in the scoreboard
        while (wrapper.scoreboard.Count > scoreBoardMax)
        {
            wrapper.scoreboard.RemoveAt(wrapper.scoreboard.Count - 1);
            Debug.Log("Score deleted. ScoreBoard only shows top " + scoreBoardMax);
        }
        string sortedScoreboard = JsonUtility.ToJson(wrapper, true);
        //Saves data to json-file
        File.WriteAllText(Path(), sortedScoreboard);
        Debug.Log("Scoreboard Sorted");
    }

    public void DisplayScoreboard()
    {
        leaderboard.text = "Leaderboard: \n\n";
        //Cap so that scores will stop being added to leaderboard.text after the 10th score
        for (int i = 0; i < wrapper.scoreboard.Count; i++)
        {
            leaderboard.text += i + 1 + ". " + wrapper.scoreboard[i].name + ":\t" + wrapper.scoreboard[i].score + "\n";
        }
    }
}

