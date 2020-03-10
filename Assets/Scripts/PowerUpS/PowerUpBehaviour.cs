using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    const float HORIZONTAL_SPEED = 5.0f;
    public Rigidbody2D rigidbody2d;
    float camera_half_width, camera_half_height;
    private Camera main_camera;
    // Start is called before the first frame update
    void Start()
    {
        main_camera = Camera.main;
        camera_half_height = main_camera.orthographicSize;    //main_camera.orthographicSize returns the half-height of the camera in world units
        camera_half_width = camera_half_height * main_camera.aspect;    //Multiply the half-height by the aspect ration to get the half-width

    }
    void moveObject()
    {
        rigidbody2d.velocity = -transform.right * HORIZONTAL_SPEED;
    }
    // Update is called once per frame
    void Update()
    {
        moveObject();

    }
}
