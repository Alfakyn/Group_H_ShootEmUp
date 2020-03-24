using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    public void SetVolumeSFX(float volume)
    {
        mixerGroup.audioMixer.SetFloat("SFXVolume", (Mathf.Log10(volume) * 20)); //Represents parameter volume as a logaritmic value with the base of ten to make it match the Mixers' log value for volume control. (Turns it to a value between -80 and 0 on a Log scale)
    }
    public void SetVolumeMusic(float volume)
    {
        mixerGroup.audioMixer.SetFloat("MusicVolume", (Mathf.Log10(volume) * 20)); //Represents parameter volume as a logaritmic value with the base of ten to make it match the Mixers' log value for volume control. (Turns it to a value between -80 and 0 on a Log scale)   
    }
}
