﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public float speed;
    float camera_half_width, camera_half_height;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        camera_half_height = Camera.main.orthographicSize;
        camera_half_width = camera_half_height * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
    }

    // Update is called once per frame
    void Update()
    {
        moveBullet();
        if (transform.position.x >= camera_half_width ||
            transform.position.x <= -camera_half_width ||
            transform.position.y >= camera_half_height ||
            transform.position.y <= -camera_half_width)
        {
            Destroy(gameObject, 1);
        }
    }

    void moveBullet()
    {
        rigidbody2d.velocity = transform.right * speed;
    }
}