﻿using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip testSound;  //Do this for every audio file
    static AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);    //Makes it so the SoundManager stays alive even after the scene is changed
    }

    void Start()
    {
        testSound = Resources.Load<AudioClip>("SFX/Noice");  //Instantiate every audio file

        audioSource = GetComponent<AudioSource>();
    }

    public static void playSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}