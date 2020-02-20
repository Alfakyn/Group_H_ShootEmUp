using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBehaviour : MonoBehaviour
{
    public int bullet_damage;
    public int torpedo_damage;
    float camera_half_width, camera_half_height;
    private Camera main_camera;
    float target_position;
    const float SPEED = 0.05f;
    
    Vector3 horizontal_speed, vertical_speed;

    public GameObject ink;
    public float ink_reload_timer = 0;
    public float ink_reload_interval;

    public int health_points;
    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //main_camera.orthographicSize returns the half-height of the camera in world units
        camera_half_width = main_camera.orthographicSize * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width
        horizontal_speed.Set(SPEED, 0,0);
        vertical_speed.Set(0, -SPEED * 1.5f,0);
        target_position = camera_half_width / 2f;
    }

    // Update is called once per frame
    void  Update()
    {
        if (health_points <= 0)
        {
            Destroy(gameObject);
            SoundManager.playSound(SoundManager.testSound);
        }
        if (ink_reload_timer > 0)
        {
            ink_reload_timer -= Time.deltaTime;
        }
        movesquid();
        shootInk();
        if (transform.position.x <= -camera_half_width)
        {
            Destroy(gameObject, 1);
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
        if (transform.position.x <= target_position)
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
        else
        {
            transform.position -= horizontal_speed;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            health_points -= bullet_damage;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Torpedo")
        {
            health_points -= torpedo_damage;
            Destroy(collision.gameObject);
            SoundManager.playSound(SoundManager.hitTorpedo);
        }
        if(collision.tag == "Player")
        {
            health_points -= health_points;
        }

    }
}
