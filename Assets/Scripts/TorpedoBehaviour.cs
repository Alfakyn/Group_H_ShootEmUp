using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoBehaviour : MonoBehaviour
{
    const float SPEED = 0.05f;
    Vector3 horizontal_speed;
    float camera_half_height, camera_half_width;

    // Start is called before the first frame update
    void Start()
    {
        horizontal_speed.Set(SPEED, 0, 0);
        camera_half_height = Camera.main.orthographicSize;    //Camera.main.orthographicSize returns the half-height of the camera in world units
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
    }

    // Update is called once per frame
    void Update()
    {
        moveTorpedo();
        if (transform.position.x >= camera_half_width)
        {
            Destroy(gameObject, 1);
        }
    }

    void moveTorpedo()
    {
        transform.position += horizontal_speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
