using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Json;

public class ScoreManager : MonoBehaviour
{
    public List<Score> scoreBoard = new List<Score>();

    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.K))
        {
            Score testScore1 = new Score();
            testScore1.score = 111;
            testScore1.name = "Simon1";

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

        //scoreBoard.Add(score);

        // serializes (converts) object data to json format
        string playerScoreData = JsonUtility.ToJson(score, true);

        //Saves data
        File.AppendAllText(Path(), playerScoreData);

        Debug.Log("Player score was saved to: " + Path());
        
    }
     

    void LoadScoreBoard()
    {
        //load all data from "path"
        
        if(File.Exists(Path()))
        {
            string data = File.ReadAllText(Path());
            scoreBoard[0] = JsonUtility.FromJson<Score>(data);
            Debug.Log(scoreBoard[1].name + scoreBoard[1].score);

        }
        else
        {
            Debug.Log("Unable to read data. File not found in " + Path());
            //scoreBoard = new Score();
        }
        
    }
}

