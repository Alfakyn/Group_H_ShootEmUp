using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoBehaviour : MonoBehaviour
{
    public SubMarineBehaviour submarine;
    const float SPEED = 0.05f;
    Vector3 horizontal_speed;
    float camera_half_width;

    // Start is called before the first frame update
    void Start()
    {
        submarine = GetComponent<SubMarineBehaviour>();
        horizontal_speed.Set(SPEED, 0, 0);
        camera_half_width = Camera.main.orthographicSize * Camera.main.aspect;    //Multiply the half-height by the aspect ration to get the half-width
        //if (submarine.torpedo_index == 3)
        //{
        //    horizontal_speed.Set(SPEED / 2, SPEED / 2, 0);
        //    transform.rotation.Set(0, 0, 45, 0);
        //}
        //else if (submarine.torpedo_index == 4)
        //{
        //    horizontal_speed.Set(SPEED / 2, -SPEED / 2, 0);
        //    transform.rotation.Set(0, 0, -45, 0);
        //}
        // Tried to access the torpedo_index which indicate which shot is being fired and changes transform to create a spreadshot
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
            // value of gameobject remains unchanged
        }
       
    }
}
