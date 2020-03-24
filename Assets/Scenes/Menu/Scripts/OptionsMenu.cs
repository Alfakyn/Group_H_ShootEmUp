using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;
    GameObject canvasPauseMenu;
    GameObject mainMenu;
    public static OptionsMenu canvasOptionsMenu;

    private void Start()
    {
        //If there is no existing canvasOptionsMenu
        if (canvasOptionsMenu == null)
        {
            canvasOptionsMenu = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (canvasOptionsMenu != this)
        {
            Destroy(gameObject);
        }
    }

    public void ActivateOptionsMenu()
    {
        canvasOptionsMenu.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("OptionsMenu activated");
    }

    void DeactivateOptionsMenu()
    {
        canvasOptionsMenu.transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("OptionsMenu deactivated");
    }

    public void BackToMainMenu()
    {
        mainMenu = GameObject.FindGameObjectWithTag("CanvasMainMenu");
        //If there is no object of MainMenu (i.e. if we are in the Level scene)
        if (mainMenu == null)
        {
            Debug.Log("No MainMenu found");
            return;
        }
        //Sets MainMenu to active
        mainMenu.transform.GetChild(0).gameObject.SetActive(true);

        //Sets OptionsMenu to inactive
        DeactivateOptionsMenu();
        playButtonSound();
    }

    public void BackToPauseMenu()
    {
        canvasPauseMenu = GameObject.FindGameObjectWithTag("CanvasPauseMenu");
        //If there is no object of PauseMenu (i.e. if we are in the Menu scene)
        if (canvasPauseMenu == null)
        {
            Debug.Log("No PauseMenu found");
            return;
        }
        //Sets PauseMenu to active
        canvasPauseMenu.transform.GetChild(0).gameObject.SetActive(true);

        //Sets OptionsMenu to inactive
        DeactivateOptionsMenu();
        playButtonSound();
    }

    public void SetVolumeSFX(float volume)
    {
        mixerGroup.audioMixer.SetFloat("SFXVolume", (Mathf.Log10(volume) * 20)); //Represents parameter volume as a logaritmic value with the base of ten to make it match the Mixers' log value for volume control. (Turns it to a value between -80 and 0 on a Log scale)
    }
    public void SetVolumeMusic(float volume)
    {
        mixerGroup.audioMixer.SetFloat("MusicVolume", (Mathf.Log10(volume) * 20)); //Represents parameter volume as a logaritmic value with the base of ten to make it match the Mixers' log value for volume control. (Turns it to a value between -80 and 0 on a Log scale)   
    }
    public void playButtonSound()
    {
        SoundManager.playSFX(SoundManager.buttonClick);
    }
}
