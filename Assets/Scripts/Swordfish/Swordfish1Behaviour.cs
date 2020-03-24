using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordfish1Behaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    const float HORIZONTAL_SPEED = 5.0f;
    const float CHASING_SPEED = 2.5f;
    const float CHASING_OFFSET = 0.5f;

    private Transform submarine_transform;

    public int bullet_damage;
    public int torpedo_damage;
    private bool has_targeted_player;

    public GameObject held_Powerup;
    public float drop_chance_percent;
    public int health_points;

    public float rangeOfChasing;
    private Vector2 moveDirection;

    public SpriteRenderer sprite_renderer;
    private float color_timer;

    // Start is called before the first frame update
    void Start()
    {
        submarine_transform = GameObject.Find("Submarine").transform;

        has_targeted_player = false;

        color_timer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health_points <= 0)
        {
            Destroy(gameObject);
            ScoreManager.scoreManager.AddScore(10);
            SoundManager.playSFX(SoundManager.testSound); // ( ͡° ͜ʖ ͡°) N I C E ( ͡° ͜ʖ ͡°)
        }
        else
        {
            moveSwordfish();
        }
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
        if(collision.tag == "Explosion")
        {
            health_points -= health_points;
        }
        if (collision.tag == "Torpedo")
        {
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
        if (collision.tag == "Player")
        {
            health_points = 0;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Camera Collider")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
