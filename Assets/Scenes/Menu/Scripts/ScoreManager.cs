using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public List<Score> scoreboard = new List<Score>();
    public static Jsonwrapper wrapper;
    public Text leaderboard, inputText;
    GameObject currentScore;
    public Canvas canvas;
    public static ScoreManager scoreManager;
    public float playerScore = 0f;
    public int scoreBoardMax;
    private int maxCharName = 20;

    public static bool chat_is_done;
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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //Assign currentScore to TextMesh.text
            if (currentScore == null)
            {
                currentScore = GameObject.FindGameObjectWithTag("CanvasPauseMenu").transform.GetChild(3).gameObject;
            }
            //Then start counting points
            if (chat_is_done == true)
            {
                playerScore += (1 * Time.deltaTime);
            }
            currentScore.GetComponent<TextMeshProUGUI>().text = ("Score: " + (int)playerScore);
        }
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    public void StoreScore()
    {
        Debug.Log("SaveScore: " + playerScore);
        PauseMenu.isPausable = false;
        scoreManager.LoadScoreBoard();
        scoreManager.InputPlayerName();
    }

    public void SaveScore()
    {
        Score score = new Score();
        score.name = scoreManager.inputText.text;
        if (score.name.Length > maxCharName)
        {
            score.name = score.name.Substring(0, maxCharName);
        }
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
        //Create .jsonfil if it does not exist in "path"
        if (!File.Exists(Path()))
        {
            Debug.Log("Unable to read data. File not found in " + Path() + "\nNew file created");
            File.Create(Path());
            //If the wrapper is unasigned, then add a new wrapper to it
            if (wrapper == null)
            {
                Debug.Log("New scoreboard wrapper was created");
                wrapper = new Jsonwrapper();
            }
        }
        //If it does exist, load all data from "path"
        else
        {
            //If the wrapper is unasigned, then add a new wrapper to it
            //if (wrapper == null)
            //{
            //    Debug.Log("New scoreboard wrapper was created");
            //    wrapper = new Jsonwrapper();
            //}
            string data = File.ReadAllText(Path());
            wrapper = JsonUtility.FromJson<Jsonwrapper>(data);

            if (wrapper == null)
            {
                wrapper = new Jsonwrapper();
            }
            if (wrapper.scoreboard.Count != 0)
            {
                SortScoreboard();
            }
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
        leaderboard.text = "High Score: \n\n";
        //Cap so that scores will stop being added to leaderboard.text after the #scoreBoardMax score
        for (int i = 0; i < wrapper.scoreboard.Count; i++)
        {
            leaderboard.text += i + 1 + ". " + wrapper.scoreboard[i].name + ":\t" + wrapper.scoreboard[i].score + "\n";
        }
    }
}

