using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    Vector3 speed;
    float camera_half_width;
    float sprite_offset;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite_offset = 5f; // rough estimate
        speed.Set(-0.01f, 0, 0);
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
    }

    // Update is called once per frame
   private void FixedUpdate()
    {
        if (transform.position.x + sprite_offset <= -camera_half_width)
        {
            Destroy(gameObject);
        }
        moveTutorial();
    }
    void moveTutorial()
    {
        transform.position += speed;
    }
}
