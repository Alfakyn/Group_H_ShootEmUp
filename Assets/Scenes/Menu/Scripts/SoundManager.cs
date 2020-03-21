using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip testSound;  //Do this for every audio file
    public static AudioClip menuMusic1;
    public static AudioClip gameMusic1;
    public static AudioClip shootingTorpedo;
    public static AudioClip shootingBullet;
    public static AudioClip hitTorpedo;
    public static AudioClip buttonClick;

    public static AudioSource[] audioSources;
    public static AudioSource sfx;
    public static AudioSource music;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);    //Makes it so the SoundManager stays alive even after the scene is changed
    }

    void Start()
    {
        testSound = Resources.Load<AudioClip>("SFX/Noice");  //Instantiate every audio file
        menuMusic1 = Resources.Load<AudioClip>("Music/Menu/01. Triumphal - AShamaluevMusic");
        gameMusic1 = Resources.Load<AudioClip>("Music/Game/Action Sport Rock Trailer - AShamaluevMusic");
        shootingTorpedo = Resources.Load<AudioClip>("SFX/Weapons/Torpedo/Missile_sonic");
        shootingBullet = Resources.Load<AudioClip>("SFX/Weapons/Bullet/secondary_scuba");
        hitTorpedo = Resources.Load<AudioClip>("SFX/Weapons/Torpedo/missile_explosion");
        buttonClick = Resources.Load<AudioClip>("SFX/Implemented resources 18th/Menu Selection Hover 2");

        audioSources = GetComponents<AudioSource>();
        sfx = audioSources[0];
        music = audioSources[1];
        music.loop = true;

        playMusic(menuMusic1);
    }

    public static void playSFX(AudioClip sound)
    {
        sfx.PlayOneShot(sound);
    }
    public static void playMusic(AudioClip sound)
    {
        music.clip = sound;
        music.Play();
    }
}

