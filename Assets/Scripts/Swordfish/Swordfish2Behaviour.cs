using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordfish2Behaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    public int health_points;
    public int bullet_damage;
    public int torpedo_damage;
    public int explosion_damage;

    const float HORIZONTAL_SPEED = 5.0f;

    private Camera main_camera;
    float camera_half_width, camera_half_height;

    public GameObject held_Powerup;
    public float drop_chance_percent;
  
    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //main_camera.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

    }

    // Update is called once per frame
    void Update()
    {
        if (health_points <= 0)
        {

            if (Random.Range(0.0f, 100.0f) < drop_chance_percent)
            {
                Debug.Log("PowerUpSpawned");
                Instantiate(held_Powerup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
            SoundManager.playSFX(SoundManager.testSound); // ( ͡° ͜ʖ ͡°) N I C E ( ͡° ͜ʖ ͡°)
        }
        else
        {
            moveSwordfish();
        }
    }
    void moveSwordfish()
    {
        rigidbody2d.velocity = -transform.right * HORIZONTAL_SPEED;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Torpedo")
        {
            Debug.Log("Torpedo Collision");
            health_points -= torpedo_damage;
            Destroy(collision.gameObject);
            SoundManager.playSFX(SoundManager.hitTorpedo);
        }
        if (collision.tag == "Explosion")
        {
            health_points -= explosion_damage;
        }
        if (collision.tag == "Bullet")
        {
            //SoundManager.playSound(SoundManager.hitBullet); //If bullet leaves screen triggers sound
            health_points -= bullet_damage;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Player")
        {
            health_points = 0;
        }
    }
}
