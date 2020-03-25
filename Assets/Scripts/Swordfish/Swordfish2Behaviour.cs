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

    public GameObject held_Powerup;
    public float drop_chance_percent;

    public SpriteRenderer sprite_renderer;
    private float color_timer;

    // Start is called before the first frame update
    void Start()
    {
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
            ScoreManager.scoreManager.AddScore(10);
            SoundManager.playSFX(SoundManager.testSound); // ( ͡° ͜ʖ ͡°) N I C E ( ͡° ͜ʖ ͡°)
            SoundManager.playSFX(SoundManager.enemyHit);
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

    void moveSwordfish()
    {
        rigidbody2d.velocity = -transform.right * HORIZONTAL_SPEED;
    }
    void displayHurtColor()
    {
        if (color_timer < 1.0f)
        {
            sprite_renderer.color = new Color(1, color_timer, color_timer);
            color_timer += 0.01f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Torpedo")
        {
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

            sprite_renderer.color = new Color(1, 0, 0);
            color_timer = 0.0f;
        }
        if (collision.tag == "Player")
        {
            health_points = 0;
        }
    }
}
