using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    GameObject optionsMenuCanvas;
    public static bool isPausable = true;
    public static bool gameIsPaused = false;
    public SubMarineBehaviour subMarineBehaviour;

    private void Start()
    {
        optionsMenuCanvas = GameObject.FindGameObjectWithTag("CanvasOptionsMenu");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPausable == true)
        {
            if (gameIsPaused)
            {
                Resume();
                //If you press escape when in optionsMenu
                if (optionsMenuCanvas.gameObject.transform.GetChild(0).gameObject.activeSelf)
                {
                    optionsMenuCanvas.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else
            {
                Cursor.visible = true;
                Pause();
            }
        }
    }

    void Pause()
    {
        gameObject.GetComponent<Image>().enabled = true;
        pauseMenuUI.SetActive(true);
        subMarineBehaviour.enabled = false;
        SoundManager.music.Pause();
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        Cursor.visible = false;
        gameObject.GetComponent<Image>().enabled = false;
        pauseMenuUI.SetActive(false);
        subMarineBehaviour.enabled = true;
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
        ScoreManager.scoreManager.playerScore = 0;
        SoundManager.music.Stop();
        SceneManager.LoadScene(1);
    }

    public void SceneSwitch()
    {
        
        Resume();
        SoundManager.music.Stop();
        Destroy(ScoreManager.scoreManager.transform.gameObject);
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
    public void playButtonSound()
    {
        SoundManager.playSFX(SoundManager.buttonClick);
    }
}
