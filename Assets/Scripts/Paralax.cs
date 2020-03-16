using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    Camera main_camera;
    private float image_pos, length;
    public float speed;
    float camera_half_width;
    float sprite_width;
    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main; //Store Camera.main reference in a variable to use below
        camera_half_width = main_camera.orthographicSize * main_camera.aspect; //Store the screen's half-width value in a variable

        image_pos = transform.position.x;
        sprite_width = GetComponent<SpriteRenderer>().bounds.size.x;
        length = sprite_width * 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x < -camera_half_width - sprite_width / 2)
        {
            transform.position = new Vector3(transform.position.x + length - 0.1f, transform.position.y,transform.position.z); // small annoying offset 
        }
        else
        {
            transform.position = transform.position + new Vector3(-speed * Time.fixedDeltaTime, 0);
        }
    }
}
