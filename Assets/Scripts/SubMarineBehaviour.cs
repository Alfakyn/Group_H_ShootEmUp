using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
public class SubMarineBehaviour : MonoBehaviour
{
    //public PowerUpSpawner powerupspawner;

    public Rigidbody2D rigidbody2d;
    public UnityEngine.Experimental.Rendering.Universal.Light2D flashlight;
    float camera_half_height, camera_half_width;

    Camera main_camera;

    const float MOVE_SPEED = 5f;
    public int health_points;

    bool submarine_covered_in_ink;
    const float INK_FALLOFF_TIME = 10;
    public float ink_timer = 0;

    public GameObject torpedo;
    public float torpedo_reload_interval;
    public float torpedo_reload_timer = 0;

    public GameObject bullet;
    public float bullet_reload_interval;
    public float bullet_reload_timer = 0;
<<<<<<< Updated upstream:Assets/Scripts/SubMarineBehaviour.cs
    
=======

    public Slider air_meter_bar;
    public float current_air;
    private  float air_countdown_rate;
    public float oxygen_volume;

    public SpriteRenderer sprite_renderer;
    private float color_timer;
>>>>>>> Stashed changes:Assets/Scripts/SubMarine/SubMarineBehaviour.cs

    // Start is called before the first frame update
    void Start()
    { 
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

        submarine_covered_in_ink = false;
    }

    //Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream:Assets/Scripts/SubMarineBehaviour.cs
        
        
        checkInk();
        shootTorpedo();
        shootGun();
=======
        shootTorpedo();
>>>>>>> Stashed changes:Assets/Scripts/SubMarine/SubMarineBehaviour.cs
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream:Assets/Scripts/SubMarineBehaviour.cs
=======
        shootGun();
        displayHurtColor();
        checkInk();
        checkAir();
>>>>>>> Stashed changes:Assets/Scripts/SubMarine/SubMarineBehaviour.cs
        moveSubmarine();

        if (health_points == 0)
        {
            Debug.Log("Player has died");
        }
    }
    void checkInk()
    {
        if (ink_timer < INK_FALLOFF_TIME)
        {
            ink_timer += Time.fixedDeltaTime;
        }
        else
        {
            submarine_covered_in_ink = false;
        }
        if (submarine_covered_in_ink == true)
        {
            flashlight.intensity = ink_timer / INK_FALLOFF_TIME;
        }
    }
    void moveSubmarine()
    {
        Vector2 pos = rigidbody2d.position;
        pos.x = Mathf.Clamp(pos.x, -camera_half_width, camera_half_width);
        pos.y = Mathf.Clamp(pos.y, -camera_half_height, camera_half_height);
        rigidbody2d.position = pos;
        rigidbody2d.velocity = (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))) * MOVE_SPEED;
    }
    void shootTorpedo()
    {
        if (torpedo_reload_timer > 0)
        {
            torpedo_reload_timer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && torpedo_reload_timer <= 0)
        {
            Instantiate(torpedo, transform.position, transform.rotation);
            torpedo_reload_timer = torpedo_reload_interval;
            SoundManager.playSFX(SoundManager.shootingTorpedo);
        }
    }
    void shootGun()
    {
        if (bullet_reload_timer > 0)
        {
            bullet_reload_timer -= Time.deltaTime;
        }

        if (Input.GetMouseButton(0) && bullet_reload_timer <= 0)
        {
            Vector2 submarine_screen_position = main_camera.WorldToScreenPoint(transform.localPosition);
            Vector2 mouse_screen_position = Input.mousePosition;
            Vector2 submarine_to_mouse = mouse_screen_position - submarine_screen_position;

            float angle_error_margin = Random.Range(-5.0f, 5.0f);
            float bullet_rotation_angle = Mathf.Atan2(submarine_to_mouse.y, submarine_to_mouse.x) * Mathf.Rad2Deg + angle_error_margin; ;

            Quaternion bullet_rotation = Quaternion.Euler(0.0f, 0.0f, bullet_rotation_angle);

            Instantiate(bullet, transform.position, bullet_rotation);
            bullet_reload_timer = bullet_reload_interval;
            SoundManager.playSFX(SoundManager.shootingBullet);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (health_points > 0)
            {
                health_points--;
            }
        }
        if(collision.tag == "Ink")
        {
            Debug.Log("Ink Collision");
            submarine_covered_in_ink = true;
            ink_timer = 0f;
            Destroy(collision.gameObject);
        }

        //Powerup
        {
            /* Does not work, values of submarine remain unchanged 
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
                */
        
        }
    }
}
