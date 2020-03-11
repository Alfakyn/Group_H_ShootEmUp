﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordfish1Behaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    const float HORIZONTAL_SPEED = 5.0f;
    const float CHASING_SPEED = 2.5f;
    const float CHASING_OFFSET = 0.5f;

    float camera_half_width, camera_half_height;

    private Transform submarine_transform;

    public int bullet_damage;
    public int torpedo_damage;
    private bool has_targeted_player = false;
    private Camera main_camera;
    private const float PLAYER_POSITION_OFFSET = 0.2f;
<<<<<<< Updated upstream:Assets/Scripts/Swordfish1Behaviour.cs

    public int health_points;
=======
    public GameObject held_Powerup;
    public float drop_chance_percent;
    public int health_points;

    public float rangeOfChasing;
    private Vector2 moveDirection;

    public SpriteRenderer sprite_renderer;
    private float color_timer;
>>>>>>> Stashed changes:Assets/Scripts/Swordfish/Swordfish1Behaviour.cs

    // Start is called before the first frame update
    void Start()
    {        
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //main_camera.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

        submarine_transform = GameObject.Find("Submarine").transform;

        color_timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health_points <= 0)
        {
            Destroy(gameObject);
            SoundManager.playSFX(SoundManager.testSound); // ( ͡° ͜ʖ ͡°) N I C E ( ͡° ͜ʖ ͡°)
        }
        else
        {        
            moveSwordfish();
        }
<<<<<<< Updated upstream:Assets/Scripts/Swordfish1Behaviour.cs
=======
    }
    private void FixedUpdate()
    {
        displayHurtColor();        
    }

    void displayHurtColor()
    {
        if (color_timer < 1.0f)
        {
            sprite_renderer.color = new Color(1, color_timer, color_timer);
            color_timer += 0.01f;
        }
>>>>>>> Stashed changes:Assets/Scripts/Swordfish/Swordfish1Behaviour.cs
    }
    void moveSwordfish()
    {

        if (has_targeted_player == false)
        {
            moveDirection = submarine_transform.position - transform.position;
            moveDirection.Normalize();
            rigidbody2d.velocity = moveDirection * CHASING_SPEED;

            if (transform.position.y < submarine_transform.position.y + CHASING_OFFSET &&
                transform.position.y > submarine_transform.position.y - CHASING_OFFSET)
            {
                has_targeted_player = true;
            }
        }
        else
        {
            rigidbody2d.velocity = -transform.right * HORIZONTAL_SPEED;
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
        if (collision.tag == "Bullet")
        {
            //SoundManager.playSound(SoundManager.hitBullet); //If bullet leaves screen triggers sound
            health_points -= bullet_damage;
            Destroy(collision.gameObject);

            sprite_renderer.color = new Color(1, 0, 0);
            color_timer = 0.0f;
        }
        if(collision.tag == "Player")
        {
            health_points = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Camera Collider")
        {
            Destroy(gameObject);
        }
    }
}
