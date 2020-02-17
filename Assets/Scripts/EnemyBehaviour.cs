using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Vector3 horizontal_speed, vertical_speed;
    float camera_half_width, camera_half_height;
    const float SPEED = 0.01f;
    SpriteRenderer sprite_renderer;
    private Transform submarine;
    public int bullet_damage;
    public int torpedo_damage;
    private bool has_targeted_player = false;

    public int health_points;

    // Start is called before the first frame update
    void Start()
    {
        has_targeted_player = false;
        horizontal_speed.Set(SPEED * 1.5f, 0, 0);
        vertical_speed.Set(0, -SPEED, 0);
        sprite_renderer = GetComponent<SpriteRenderer>();
        camera_half_height = Camera.main.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
    }

    // Update is called once per frame
    void Update()
    {
        submarine = GameObject.Find("Submarine").transform;
        if (health_points <= 0)
        {
            Destroy(gameObject);
            SoundManager.playSound(SoundManager.testSound);
        }
        else
        {
            if (transform.position.y >= submarine.position.y - 1f && transform.position.y <= submarine.position.y + 1f)
            {
                has_targeted_player = true;
            }
            moveEnemy();
            if (transform.position.x /*- sprite_renderer.sprite.bounds.extents.x*/ <= -camera_half_width)
            {
                Destroy(gameObject, 1);
            }
        }
    }

    void moveEnemy()
    {
        if (has_targeted_player == true)
        {
            transform.position -= horizontal_speed;
        }
        else
        {
            if (transform.position.y >= camera_half_height)
            {
                vertical_speed.Set(0, SPEED, 0);
            }
            else if (transform.position.y <= -camera_half_height)
            {
                vertical_speed.Set(0, -SPEED, 0);
            }
            transform.position -= vertical_speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Torpedo")
        {
            SoundManager.playSound(SoundManager.hitTorpedo);
            health_points -= torpedo_damage;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Bullet")
        {
            //SoundManager.playSound(SoundManager.hitBullet); If bullet leaves screen triggers sound
            health_points -= bullet_damage;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Player")
        {
            health_points = 0;
        }
    }
}
