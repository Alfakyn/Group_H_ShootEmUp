using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
<<<<<<< HEAD
    // Start is called before the first frame update

=======
    public GameObject optionsMenu;
>>>>>>> parent of 0e4646e... Minor Fix for ScoreManager

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        SoundManager.music.Pause();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        SoundManager.music.UnPause();
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void QuitGame()
    {
        Debug.Log("GAME QUIT");
        Application.Quit();
    }

    public void RestartLevel()
    {
        Resume();
        SoundManager.music.Stop();
        SceneManager.LoadScene(1);
    }

    public void SceneSwitch()
    {
        Resume();
        SoundManager.music.Stop();
        SceneManager.LoadScene(0);

    }
}
