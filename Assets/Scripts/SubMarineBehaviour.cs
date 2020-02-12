using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMarineBehaviour : MonoBehaviour
{
    PowerUpSpawner powerupspawner;
    Vector3 horizontal_speed, vertical_speed;
    Vector3 spread_distance;
    float camera_half_height, camera_half_width;
    SpriteRenderer sprite_renderer;
    const float SPEED = 0.05f;
    public float torpedo_reload_time = 30;
    public float torpedo_reload_counter = 0;
    public int spread_shot_count;
    public int health_points;
    public int torpedo_index = 0;
    public Transform torpedo;

    // Start is called before the first frame update
    void Start()
    {
        powerupspawner = GetComponent<PowerUpSpawner>();
        horizontal_speed.Set(SPEED, 0, 0);
        vertical_speed.Set(0, SPEED, 0);
        spread_distance.Set(0, 3, 0);
        camera_half_height = Camera.main.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        torpedo_reload_counter += Time.deltaTime;
        moveSubmarine();
        shootTorpedo();
        if (health_points <= 0)
        {
            Debug.Log("Player has died");
        }
    }
    void moveSubmarine()
    {
        if (Input.GetKey(KeyCode.D) && transform.position.x + horizontal_speed.x + sprite_renderer.sprite.bounds.extents.x <= camera_half_width)
        {
            transform.position += horizontal_speed;
        }
        if (Input.GetKey(KeyCode.A) && transform.position.x - horizontal_speed.x - sprite_renderer.sprite.bounds.extents.x >= -camera_half_width)
        {
            transform.position -= horizontal_speed;
        }
        if (Input.GetKey(KeyCode.W) && transform.position.y + vertical_speed.y + sprite_renderer.sprite.bounds.extents.y <= camera_half_height)
        {
            transform.position += vertical_speed;
        }
        if (Input.GetKey(KeyCode.S) && transform.position.y - vertical_speed.y - sprite_renderer.sprite.bounds.extents.y >= -camera_half_height)
        {
            transform.position -= vertical_speed;
        }
    }
    void shootTorpedo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && torpedo_reload_counter >= torpedo_reload_time)
        {
            // multishot loop and reload value reset
            for (int i = 0; i < spread_shot_count; i++)
            {
                torpedo_index++;
                Instantiate(torpedo, transform.position, transform.rotation);
            }
            torpedo_reload_counter = 0;
            torpedo_index = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health_points--;
        }
        // Does not work, values of submarine remain unchanged 
        if (collision.gameObject.tag == "PowerUp")
        {
            PowerUpSpawner.Type type;
            PowerUpSpawner.Power power;
            type = powerupspawner.powerUps[powerupspawner.power_up_index].type;
            power = powerupspawner.powerUps[powerupspawner.power_up_index].power;
            for (int i = 0; i < 2; i++)
            {

                switch (type)
                {
                    case PowerUpSpawner.Type.AttackSpeed:
                        if (torpedo_reload_time > 3)
                        {
                            switch (power)
                            {
                                case PowerUpSpawner.Power.Minor:
                                    torpedo_reload_time -= 2;
                                    break;
                                case PowerUpSpawner.Power.Medium:
                                    torpedo_reload_time -= 4;
                                    break;
                                case PowerUpSpawner.Power.Strong:
                                    torpedo_reload_time -= 6;
                                    break;

                            }
                        }
                        break;
                    case PowerUpSpawner.Type.Health:
                        switch (power)
                        {
                            case PowerUpSpawner.Power.Minor:
                                health_points += 1;
                                break;
                            case PowerUpSpawner.Power.Medium:
                                health_points += 2;
                                break;
                            case PowerUpSpawner.Power.Strong:
                                health_points += 3;
                                break;

                        }
                        break;
                    case PowerUpSpawner.Type.WideShot:
                        if (spread_shot_count < 4)
                        {
                            spread_shot_count++;
                        }
                        break;
                    case PowerUpSpawner.Type.Null:

                        break;

                }
                type = powerupspawner.powerUps[powerupspawner.power_up_index].type_2;
                power = powerupspawner.powerUps[powerupspawner.power_up_index].power_2;


            }
        }

    }
}
