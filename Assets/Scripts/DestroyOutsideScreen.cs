using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutsideScreen : MonoBehaviour
{
    Camera main_camera;
    float camera_half_width, camera_half_height;

    SpriteRenderer sprite_renderer;
    float sprite_half_width, sprite_half_height;
    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;
        camera_half_width = camera_half_height * main_camera.aspect;

        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_half_width = sprite_renderer.bounds.size.x / 2;
        sprite_half_height = sprite_renderer.bounds.size.y / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > camera_half_height + sprite_half_height ||
            transform.position.y < -camera_half_height - sprite_half_height ||
            transform.position.x < -camera_half_width - sprite_half_width)
        {
            Destroy(gameObject);
        }
        if ((gameObject.tag == "Bullet" || gameObject.tag == "Torpedo") &&
            transform.position.x > camera_half_width + sprite_half_width)
        {
            Destroy(gameObject);
        }
    }
}
