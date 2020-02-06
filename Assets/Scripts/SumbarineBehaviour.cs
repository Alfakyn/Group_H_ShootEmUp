using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumbarineBehaviour : MonoBehaviour
{
    Vector3 horizontal_speed, vertical_speed;
    float camera_half_height, camera_half_width;
    SpriteRenderer sprite_renderer;
    const float SPEED = 0.05f;
    public Transform torpedo;

    // Start is called before the first frame update
    void Start()
    {
        horizontal_speed.Set(SPEED, 0, 0);
        vertical_speed.Set(0, SPEED, 0);
        camera_half_height = Camera.main.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSubmarine();
        shootTorpedo();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(torpedo, transform.position, transform.rotation);
        }
    }
}
