using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class SubMarineBehaviour : MonoBehaviour
{
    //public PowerUpSpawner powerupspawner;

    public Rigidbody2D rigidbody2d;
    public UnityEngine.Experimental.Rendering.Universal.Light2D flashlight;
    float camera_half_height, camera_half_width;
    private Vector3 crosshair_target;
    public GameObject crosshair;

    Camera main_camera;

    const float MOVE_SPEED = 5f;

    private const float INVINCIBILITY_FRAMES = 3;
    private float invincibility_timer;
    private bool invincibility_cheat_on;

    public GameObject tutorialtext;



    bool submarine_covered_in_ink;
    const float INK_FALLOFF_TIME = 30;
    public float ink_timer = 0;

    public Image reload_image;

    public GameObject torpedo;
    public float torpedo_reload_interval;
    public float torpedo_reload_timer = 0;

    public GameObject bullet;
    public float bullet_reload_interval;
    public float bullet_reload_timer = 0;

    public GameObject[] chatBubbles;
    private int chat_counter;
    public bool chat_is_done;


    private int damage;
    private const int MAX_DAMAGE = 5;
    public Image[] hull_integrity_representation;
    public Sprite damaged_hull;
    public Sprite undamaged_hull;
    public Slider air_meter_bar;
    public float current_air;
    private float air_countdown_rate;
    public float oxygen_volume;

    public SpriteRenderer sprite_renderer;
    private float color_timer;

    // Start is called before the first frame update
    void Start()
    {
        damage = 0;
        Cursor.visible = false;
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

        submarine_covered_in_ink = false;

        invincibility_cheat_on = false;
        invincibility_timer = 0;

        chat_counter = 0;
        chat_is_done = false;
        ScoreManager.chat_is_done = false;
        WaveSpawner.chat_is_done = false;

        tutorialtext.SetActive(true);

        air_meter_bar.maxValue = current_air;
        air_meter_bar.value = current_air;
        air_countdown_rate = 0.1f;

        color_timer = 1.0f;
    }

    //Update is called once per frame
    void Update()
    {
        if(chat_is_done == false)
        {
            checkChat();
        }
        shootTorpedo();
        moveCrosshair();
        checkCheats();
    }

    private void FixedUpdate()
    {
        checkInvincibility();
        shootGun();
        displayHurtColor();
        checkInk();
        if (chat_is_done == true)
        {
            checkAir();
        }
        moveSubmarine();
    }

    void checkCheats()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            Vector2 cheatpos = rigidbody2d.position;
            cheatpos.x = camera_half_width - 20;

            if (rigidbody2d.position.x >= cheatpos.x)
            {
                invincibility_cheat_on = true;
                air_countdown_rate = 0;
                Debug.Log("Cheat on");
            }
        }
    }
    void checkInvincibility()
    {
        if (invincibility_timer < INVINCIBILITY_FRAMES && invincibility_cheat_on == false)
        {
            invincibility_timer += Time.fixedDeltaTime;
        }
        else if ( invincibility_cheat_on == true)
        {
            invincibility_timer = 0;
        }

    }
    void checkChat()
    {
        if(Input.GetKeyDown(KeyCode.X) && chat_counter < chatBubbles.Length -1)
        {
            chatBubbles[chat_counter].SetActive(false);
            chat_counter++;
            chatBubbles[chat_counter].SetActive(true);

        }
        else if(Input.GetKeyDown(KeyCode.X) && chat_counter == chatBubbles.Length -1)
        {
            chatBubbles[chat_counter].SetActive(false);
            chat_is_done = true;
            ScoreManager.chat_is_done = true;
            WaveSpawner.chat_is_done = true;
            tutorialtext.SetActive(false);
           
        }
    }
    void moveCrosshair()
    {
        crosshair_target = main_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshair.transform.position = new Vector2(crosshair_target.x, crosshair_target.y);
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

    void checkAir()
    {
        for (int i = 0; i < hull_integrity_representation.Length; i++)
        { 
           if (i < damage)      
           {      
                hull_integrity_representation[i].sprite = damaged_hull;     
           }      
           else
           {      
              hull_integrity_representation[i].sprite = undamaged_hull;      
           }            
        }    
            
        if (current_air > 0)
        {
            current_air -= air_countdown_rate;
            air_meter_bar.value = current_air;
            if (current_air > air_meter_bar.maxValue)
            {
                current_air = air_meter_bar.maxValue;
            }
        }

        if (current_air <= 0.0f)
        {
            current_air = 0.0f;
            Debug.Log("Player has died");
            SoundManager.music.Stop();
            Cursor.visible = true;
            ScoreManager.scoreManager.StoreScore();
        }
    }

    void displayHurtColor()
    {
        if (color_timer < 1.0f)
        {
            sprite_renderer.color = new Color(1, color_timer, color_timer);
            color_timer += 0.01f;
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
        if (torpedo_reload_timer <= torpedo_reload_interval)
        {
            torpedo_reload_timer += Time.deltaTime;
            reload_image.fillAmount = torpedo_reload_timer / torpedo_reload_interval;
        }

        if (Input.GetKeyDown(KeyCode.Space) && torpedo_reload_timer >= torpedo_reload_interval)
        {
            Instantiate(torpedo, transform.position, transform.rotation);
            torpedo_reload_timer = 0;
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
        if (collision.tag == "Enemy" || collision.tag == "Spike")
        {
            if (invincibility_timer >= INVINCIBILITY_FRAMES)
            {
                if (damage < MAX_DAMAGE)
                {
                    damage++;
                    air_countdown_rate = air_countdown_rate * 1.5f;
                }
                else
                {
                    current_air -= 50;
                }
                sprite_renderer.color = new Color(1, 0, 0);
                color_timer = 0.0f;
                invincibility_timer = 0;
            }
        }
        if (collision.tag == "Ink")
        {
            submarine_covered_in_ink = true;
            ink_timer = 0f;
            SoundManager.playSFX(SoundManager.inkSplatter);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Oxygen")
        {
            current_air += oxygen_volume;
            Destroy(collision.gameObject);

        }
        if(collision.tag == "Repair")
        {
            if (damage > 0)
            {
                air_countdown_rate = air_countdown_rate / 1.5f;
                damage--;
            }
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
