using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Vector3 horizontal_speed;
    float camera_half_width, camera_half_height;
    const float SPEED = 0.01f;
    SpriteRenderer sprite_renderer;

    public int bullet_damage;
    public int torpedo_damage;

    public int health_points;

    // Start is called before the first frame update
    void Start()
    {
        horizontal_speed.Set(SPEED, 0, 0);
        sprite_renderer = GetComponent<SpriteRenderer>();
        camera_half_height = Camera.main.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
    }

    // Update is called once per frame
    void Update()
    {
        if (health_points <= 0)
        {
            Destroy(gameObject);
            SoundManager.playSound(SoundManager.testSound);
        }
        else
        {
            
            moveEnemy();
            if (transform.position.x /*- sprite_renderer.sprite.bounds.extents.x*/ <= -camera_half_width)
            {
                Destroy(gameObject, 1);
            }
        }
    }

    void moveEnemy()
    {
        transform.position -= horizontal_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Torpedo")
        {
            health_points -= torpedo_damage;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Bullet")
        {
            health_points -= bullet_damage;
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
