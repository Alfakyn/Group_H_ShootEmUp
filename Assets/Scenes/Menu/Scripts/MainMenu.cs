using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SceneSwitch()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("GAME QUIT");
        Application.Quit();
    }

    public void playButtonSound()
    {
        SoundManager.playSound(SoundManager.testSound);
    }


}
