using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolumeSFX(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("SFXVolume", (Mathf.Log10(volume) * 20)); //Represents parameter volume as a logaritmic value with the base of ten to make it match the Mixers' log value for volume control. (Turns it to a value between -80 and 0 on a Log scale)
    }
    public void SetVolumeMusic(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume", (Mathf.Log10(volume) * 20)); //Represents parameter volume as a logaritmic value with the base of ten to make it match the Mixers' log value for volume control. (Turns it to a value between -80 and 0 on a Log scale)
    }

}
