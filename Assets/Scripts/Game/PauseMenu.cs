using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenu;
    public static bool isPausable = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPausable == true)
        {
            if (gameIsPaused)
            {
                Resume();
                //If you press escape when in optionsMenu
                if (optionsMenu.activeSelf)
                {
                    optionsMenu.SetActive(false);

                    //Makes all buttons in pauseMenu active again (for next time when opening the pauseMenu
                    pauseMenuUI.transform.GetChild(0).gameObject.SetActive(true);
                    pauseMenuUI.transform.GetChild(1).gameObject.SetActive(true);
                    pauseMenuUI.transform.GetChild(2).gameObject.SetActive(true);
                    pauseMenuUI.transform.GetChild(3).gameObject.SetActive(true);
                    pauseMenuUI.transform.GetChild(4).gameObject.SetActive(true);
                } 
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
