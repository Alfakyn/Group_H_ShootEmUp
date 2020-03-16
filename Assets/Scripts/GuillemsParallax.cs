using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuillemsParallax : MonoBehaviour
{
    Camera main_camera;
    float camera_half_width;
    float sprite_half_width;
    public float movementSpeed;

    void Start()
    {
        main_camera = Camera.main; //Store Camera.main reference in a variable to use below
        camera_half_width = main_camera.orthographicSize * main_camera.aspect; //Store the screen's half-width value in a variable

        sprite_half_width = GetComponent<SpriteRenderer>().bounds.size.x / 2 * transform.localScale.x;
    }

    void Update()
    {

    if (transform.position.x < -camera_half_width - sprite_half_width) 

        transform.position = new Vector3(camera_half_width * 2, transform.position.y);
    
    else
    
        transform.position = transform.position + new Vector3(-movementSpeed * Time.deltaTime, 0);
    }
/*
Compare the X position of whatever you are moving with the camera's negative half-width (the leftmost part of the screen, basically)
You will also need to add the sprite's half width to that value since the position is calculated from the center of the object
The calculation should look somewhat like this:
if (object.transform.position.x < -camera_half_width - sprite_half_width)
{
	//reset position to the rightmost side of the screen
}
else
{
	//Move the object to the left with the desired speed
}
*/
}
