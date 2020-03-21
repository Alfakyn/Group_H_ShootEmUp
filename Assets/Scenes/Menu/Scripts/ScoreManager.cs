using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Json;

public class ScoreManager : MonoBehaviour
{
<<<<<<< HEAD
    public List<Score> scoreBoard = new List<Score>();

    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.K))
        {
            Score testScore1 = new Score();
            testScore1.score = 111;
            testScore1.name = "Simon1";
=======
    public List<Score> scoreboard = new List<Score>();
    public static JsonWrapper wrapper;
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
>>>>>>> parent of 0e4646e... Minor Fix for ScoreManager

            Score testScore2 = new Score();
            testScore2.score = 222;
            testScore2.name = "Simon2";

            SaveScore(testScore1);
            SaveScore(testScore2);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadScoreBoard();
        }

<<<<<<< HEAD
=======
    public void InputPlayerName()
    {
        canvasTransform.gameObject.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Input Name");
>>>>>>> parent of 0e4646e... Minor Fix for ScoreManager
    }
    static string Path()
    {
        string path = Application.persistentDataPath + "/scoreboard.json";
        return path;
    }

    public void SaveScore(Score score)
    {
        //Save data by adding it to "path"

        //Creates a new file. Might want to change it to look for an already existing file if the file already exists
        //FileStream stream = new FileStream(Path(), FileMode.Create);

<<<<<<< HEAD
        //scoreBoard.Add(score);

        // serializes (converts) object data to json format
        string playerScoreData = JsonUtility.ToJson(score, true);

        //Saves data
        File.AppendAllText(Path(), playerScoreData);
=======
        // serializes (converts) Score "data" to json format
        wrapper.scoreboard.Add(score);
        string playerScoreData = JsonUtility.ToJson(wrapper, true);
>>>>>>> parent of 0e4646e... Minor Fix for ScoreManager

        Debug.Log("Player score was saved to: " + Path());
        
    }
     

    void LoadScoreBoard()
    {
        //load all data from "path"
        
        if(File.Exists(Path()))
        {
<<<<<<< HEAD
=======
            Debug.Log("Loading data...");
            scoreboard.Clear();
>>>>>>> parent of 0e4646e... Minor Fix for ScoreManager
            string data = File.ReadAllText(Path());
            scoreBoard[0] = JsonUtility.FromJson<Score>(data);
            Debug.Log(scoreBoard[1].name + scoreBoard[1].score);

        }
        else
        {
            Debug.Log("Unable to read data. File not found in " + Path());
            //scoreBoard = new Score();
        }
<<<<<<< HEAD
        
=======
    }

    public void SortScoreboard()
    {
        scoreboard.Sort((a, b) => b.score.CompareTo(a.score));
        Debug.Log("Scoreboard Sorted");
    }

    public void DisplayScoreboard()
    {
        leaderboard.text = "Leaderboard: \n\n";
        for (int i = 0; i < scoreboard.Count; i++)
        {
            //Add cap so that it will stop adding scores to text.text after the 10th score
            leaderboard.text += i +1 + ". " + scoreboard[i].name + ":\t" + scoreboard[i].score + "\n";
        }
>>>>>>> parent of 0e4646e... Minor Fix for ScoreManager
    }
}

