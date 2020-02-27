using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public static PowerUpSpawner powerupspawner;
    Vector3 horizontal_speed;
    float camera_half_width, camera_half_height;
    const float SPEED = 0.01f;
    PowerUpSpawner.Type type;
    PowerUpSpawner.Type type_2;
    PowerUpSpawner.Power power;
    PowerUpSpawner.Power power_2;
    SpriteRenderer sprite_renderer;
    // Start is called before the first frame update
    void Start()
    {
        powerupspawner = GetComponent<PowerUpSpawner>();
        horizontal_speed.Set(SPEED, 0, 0);
        sprite_renderer = GetComponent<SpriteRenderer>();
        camera_half_height = Camera.main.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
        type = powerupspawner.powerUps[powerupspawner.power_up_index].type;
        type_2 = powerupspawner.powerUps[powerupspawner.power_up_index].type_2;
        power = powerupspawner.powerUps[powerupspawner.power_up_index].power;
        power_2 = powerupspawner.powerUps[powerupspawner.power_up_index].power_2;
    }
    void moveObject()
    {
        transform.position -= horizontal_speed;
    }
    // Update is called once per frame
    void Update()
    {
        moveObject();
        if (transform.position.x - sprite_renderer.sprite.bounds.extents.x <= -camera_half_width)
        {
            Destroy(gameObject, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Destroy powerup");
            Destroy(gameObject);
        }
    }
}
