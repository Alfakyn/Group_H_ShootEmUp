using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufferfishBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;

    const float HORIZONTAL_SPEED = 2.0f;

    float camera_half_width, camera_half_height;

    private Camera main_camera;
    private const float PLAYER_POSITION_OFFSET = 0.2f;
    private int stamina;
    private const int STAMINA_MAX = 125;
    public float stamina_recharge_counter;
    private const float STAMINA_RECHARGE_TIME = 1.0f;

    public GameObject spike;
    private const float SPIKE_INTERVALL = 20.0f;
    private const int SPIKE_MAX = 18;

    public GameObject held_Powerup;
    public float drop_chance_percent;

    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        stamina = STAMINA_MAX;
        stamina_recharge_counter = 0.0f;
        camera_half_height = main_camera.orthographicSize;    //main_camera.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        movePufferfish();
    }
 
    void movePufferfish()
    {
        if (stamina > 0)
        {
            rigidbody2d.velocity = -transform.right * HORIZONTAL_SPEED;
            stamina--;
        }
        else if (stamina <= 0)
        {
            if (stamina_recharge_counter > STAMINA_RECHARGE_TIME)
            {
                Debug.Log("Stamina_recharging");
                stamina = STAMINA_MAX;
                stamina_recharge_counter = 0;
            }
            else if (stamina_recharge_counter < STAMINA_RECHARGE_TIME)
            {
                rigidbody2d.velocity = -transform.right * 0;
                stamina_recharge_counter += Time.fixedDeltaTime;
            }
        }
    }
    void Shootspikes()
    {
        Transform spike_transform = transform;
        float spike_rotation_pos = 0;
        for (int i = 0; i < SPIKE_MAX; i++)
        {
            spike_rotation_pos += SPIKE_INTERVALL;
            spike_transform.rotation *= Quaternion.Euler(0, 0, spike_rotation_pos);
            Instantiate(spike, transform.position,spike_transform.rotation);
        }
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Torpedo")
        {
            Debug.Log("Torpedo Collision");
            Destroy(collision.gameObject);
            Destroy(gameObject);
            SoundManager.playSFX(SoundManager.hitTorpedo);  
        }
        if (collision.tag == "Bullet")
        {
            if (Random.Range(0.0f, 100.0f) > drop_chance_percent)
            {
                Debug.Log("PowerUpSpawned");
                Instantiate(held_Powerup, transform.position, transform.rotation);
            }
            Shootspikes();
            //SoundManager.playSound(SoundManager.hitBullet); //If bullet leaves screen triggers sound
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            Shootspikes();
            Destroy(gameObject);
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

