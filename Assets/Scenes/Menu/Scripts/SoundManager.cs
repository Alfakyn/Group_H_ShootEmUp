using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip testSound;  //Do this for every audio file
    public static AudioClip testMusic;
    public static AudioClip shootingTorpedo;
    public static AudioClip shootingBullet;

    static AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);    //Makes it so the SoundManager stays alive even after the scene is changed
    }

    void Start()
    {
        testSound = Resources.Load<AudioClip>("SFX/Noice");  //Instantiate every audio file
        testMusic = Resources.Load<AudioClip>("Music/testMusic");
        shootingTorpedo = Resources.Load<AudioClip>("SFX/SFX Weapons/Torpedo/Missile_sonic");
        shootingBullet = Resources.Load<AudioClip>("SFX/SFX Weapons/Bullet/secondary_scuba");

        audioSource = GetComponent<AudioSource>();
    }

    public static void playSound(AudioClip sound)
    {
        audioSource.PlayOneShot(sound);
    }
}

