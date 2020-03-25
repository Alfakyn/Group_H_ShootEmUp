using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //Do this for every audio file
    public static AudioClip testSound;
    public static AudioClip menuMusic1;
    public static AudioClip gameMusic1;
    public static AudioClip shootingTorpedo;
    public static AudioClip shootingBullet;
    public static AudioClip hitTorpedo;
    public static AudioClip buttonClick;
    public static AudioClip pufferFishDeath;
    public static AudioClip creditsMusic;
    public static AudioClip inkSplatter;
    public static AudioClip enemyHit;
    public static AudioClip powerUp;

    public static AudioSource[] audioSources;
    public static AudioSource sfx;
    public static AudioSource music;

    void Awake()
    {
        //Makes it so the SoundManager stays alive even after the scene is changed
        DontDestroyOnLoad(transform.gameObject);    
    }

    void Start()
    {
        //Instantiate every audio file
        menuMusic1 = Resources.Load<AudioClip>("Music/Menu/01. Triumphal - AShamaluevMusic");
        gameMusic1 = Resources.Load<AudioClip>("Music/Game/Action Sport Rock Trailer - AShamaluevMusic");
        shootingTorpedo = Resources.Load<AudioClip>("SFX/Weapons/Torpedo/Missile_sonic");
        shootingBullet = Resources.Load<AudioClip>("SFX/Weapons/Bullet/secondary_scuba");
        hitTorpedo = Resources.Load<AudioClip>("SFX/Weapons/Torpedo/missile_explosion");
        buttonClick = Resources.Load<AudioClip>("SFX/Implemented resources 18th/Menu Selection Hover 2");
        pufferFishDeath = Resources.Load<AudioClip>("SFX/Enemies/Puffer/puffer_death_1");
        creditsMusic = Resources.Load<AudioClip>("SFX/Implemented resources 18th/Credits");
        inkSplatter = Resources.Load<AudioClip>("SFX/Implemented resources 18th/Ink Splatter");
        enemyHit = Resources.Load<AudioClip>("SFX/Implemented resources 18th/Enemy hit");
        powerUp = Resources.Load<AudioClip>("SFX/Power up bubble");

        audioSources = GetComponents<AudioSource>();
        sfx = audioSources[0];
        music = audioSources[1];
        music.loop = true;

        playMusic(menuMusic1);
    }

    public static void playSFX(AudioClip sound)
    {
        sfx.clip = sound;
        if (sound == null)
        {
            return;
        }
        if (sound.name == "puffer_death_1")
        {
            sfx.volume = 0.4f;
        }
        else
        {
            sfx.volume = 1.0f;
        }
        sfx.PlayOneShot(sound, sfx.volume);
    }
    public static void playMusic(AudioClip sound)
    {
        music.clip = sound;
        //Tweaks volume depending on which source it is
        if (sound.name == "Action Sport Rock Trailer - AShamaluevMusic")
        {
            music.volume = 0.3f;
        }
        else
        {
            music.volume = 1.0f;
        }
        music.Play();
    }
}

