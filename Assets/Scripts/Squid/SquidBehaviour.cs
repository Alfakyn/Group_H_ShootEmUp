using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    private bool moving_up;

    public int bullet_damage;
    public int torpedo_damage;
    public int explosion_damage;

    float camera_half_width;
    private Camera main_camera;

    float target_position;
    const float HORIZONTAL_SPEED = 5.0f;
    const float VERTICAL_SPEED = 10.0f;

    public GameObject ink;
    public float ink_reload_timer = 0;
    public float ink_reload_interval;

    public int health_points;
    public GameObject held_Powerup;
    public float drop_chance_percent;

    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        camera_half_width = main_camera.orthographicSize * main_camera.aspect;    //Multiply the camera half-height by the aspect ration to get the half-width

        target_position = camera_half_width / 2f;

        moving_up = (Random.value > 0.5f); //Random.value returns a number between 0 and 1. This line initializes a boolean randomly with true or false at ~50% chance each
    }

    // Update is called once per frame
    void Update()
    {
        if (health_points <= 0)
        {
            if (Random.Range(0.0f, 100.0f) > drop_chance_percent)
            {
                Debug.Log("PowerUpSpawned");
                Instantiate(held_Powerup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
            SoundManager.playSFX(SoundManager.testSound);
        }
        if (ink_reload_timer > 0)
        {
            ink_reload_timer -= Time.deltaTime;
        }
        shootInk();
    }

    private void FixedUpdate()
    {
        movesquid();        
    }

    void shootInk()
    {
        if (ink_reload_timer <= 0)
        {
            Instantiate(ink, transform.position, transform.rotation);
            ink_reload_timer = ink_reload_interval;
        }
    }
    void movesquid()
    {
        if (transform.position.x > target_position)
        { 
            rigidbody2d.velocity = -transform.right * HORIZONTAL_SPEED; 
        }
        else 
        {
            if (moving_up == true) 
            {
                rigidbody2d.velocity = transform.up * VERTICAL_SPEED;
            }
            else 
            {
                rigidbody2d.velocity = -transform.up * VERTICAL_SPEED;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            health_points -= bullet_damage;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Explosion")
        {
            health_points -= explosion_damage;
        }
        if (collision.tag == "Torpedo")
        {
            health_points -= torpedo_damage;
            Destroy(collision.gameObject);
            SoundManager.playSFX(SoundManager.hitTorpedo);
        }
        if(collision.tag == "Player")
        {
            health_points -= health_points;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Camera Collider") 
        { 
            moving_up = !moving_up; 
        }
    }
}
