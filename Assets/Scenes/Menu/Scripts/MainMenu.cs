using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string konamiCode;

    private void Start()
    {
        konamiCode = "";
    }
    public void SceneSwitch()
    {
        SoundManager.music.Stop();
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            konamiCode += 'U';
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            konamiCode += 'L';
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            konamiCode += 'D';
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            konamiCode += 'R';
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            konamiCode += 'A';
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            konamiCode += 'B';
        }
        if (konamiCode.EndsWith("UUDDLRLRBA"))
        {
            Debug.Log("KONAMI_CODE ACTIVATED");
            SoundManager.testSound = Resources.Load<AudioClip>("SFX/Noice");
        }
    }
    public void QuitGame()
    {
        Debug.Log("GAME QUIT");
        Application.Quit();
    }
    public void playButtonSound()
    {
        SoundManager.playSFX(SoundManager.buttonClick);
    }
}
