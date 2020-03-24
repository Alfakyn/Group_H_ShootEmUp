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

    private Camera main_camera;
    float camera_half_width, camera_half_height;

    float target_position;
    const float HORIZONTAL_SPEED = 5.0f;
    const float VERTICAL_SPEED = 10.0f;

    public GameObject ink;
    public float ink_reload_timer = 0;
    public float ink_reload_interval;

    public int health_points;
    public GameObject held_Powerup;
    public float drop_chance_percent;

    public SpriteRenderer sprite_renderer;
    private float color_timer;

    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;
        camera_half_width = camera_half_height * main_camera.aspect;

        target_position = camera_half_width / 2f;

        moving_up = (Random.value > 0.5f); //Random.value returns a number between 0 and 1. This line initializes a boolean randomly with true or false at ~50% chance each

        color_timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health_points <= 0)
        {
            if (Random.Range(0.0f, 100.0f) < drop_chance_percent)
            {

                Instantiate(held_Powerup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
            ScoreManager.scoreManager.AddScore(20);
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
        displayHurtColor();
    }

    void displayHurtColor()
    {
        if (color_timer < 1.0f)
        {
            sprite_renderer.color = new Color(1, color_timer, color_timer);
            color_timer += 0.01f;
        }
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
                if (transform.position.y > camera_half_height)
                {
                    moving_up = false;
                }
            }
            else 
            {
                rigidbody2d.velocity = -transform.up * VERTICAL_SPEED;
                if (transform.position.y < -camera_half_height)
                {
                    moving_up = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            health_points -= bullet_damage;
            Destroy(collision.gameObject);

            sprite_renderer.color = new Color(1, 0, 0);
            color_timer = 0.0f;
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
}
