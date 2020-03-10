using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordfish1Behaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    const float HORIZONTAL_SPEED = 5.0f;
    const float VERTICAL_SPEED = 10.0f;
    private bool moving_up;

    float camera_half_width, camera_half_height;

    private Transform submarine_transform;

    public int bullet_damage;
    public int torpedo_damage;
    public int explosion_damage;
    private bool has_targeted_player = false;
    private Camera main_camera;
    private const float PLAYER_POSITION_OFFSET = 0.2f;
    public GameObject held_Powerup;
    public float drop_chance_percent;
    public int health_points;

    public float rangeOfChasing;
    private Vector3 moveDirection;


    // Start is called before the first frame update
    void Start()
    {        
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //main_camera.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

        submarine_transform = GameObject.Find("Submarine").transform;

        moving_up = (Random.value > 0.5f); //Random.value returns a number between 0 and 1. This line initializes a boolean randomly with true or false at ~50% chance each
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
            checkPosition();            
            moveSwordfish();
        }

        
        //To make the enemy chase after the player.
        if(Vector3.Distance(transform.position, SubMarineBehaviour.instance.transform.position) < rangeOfChasing)
        {
            moveDirection = SubMarineBehaviour.instance.transform.position - transform.position;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        moveDirection.Normalize();
        rigidbody2d.velocity = moveDirection * HORIZONTAL_SPEED;
    }

    void moveSwordfish()
    {
        //To-Do: pathing
        if (has_targeted_player == true)
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

    void checkPosition()
    {
        if (transform.position.y >= camera_half_height)
        {
            moving_up = false;
        }
        else if (transform.position.y <= -camera_half_height)
        {
            moving_up = true;
        }

        if (transform.position.y >= submarine_transform.position.y - PLAYER_POSITION_OFFSET && transform.position.y <= submarine_transform.position.y + PLAYER_POSITION_OFFSET)
        {
            has_targeted_player = true;
        }
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
        if(collision.tag == "Explosion")
        {
            health_points -= explosion_damage;
        }
        if (collision.tag == "Bullet")
        {
            //SoundManager.playSound(SoundManager.hitBullet); //If bullet leaves screen triggers sound
            health_points -= bullet_damage;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Player")
        {
            health_points = 0;
        }
    }
}
