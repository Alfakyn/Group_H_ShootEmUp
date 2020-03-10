using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    const float SPEED = 25f;
    
    public Transform aimRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make the submarine to be able to shoot at all direction.
        Vector3 bulletPosition = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        if(bulletPosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            aimRotation.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            aimRotation.localScale = Vector3.one;
        }
    }

    private void FixedUpdate()
    {
        moveBullet();
    }

    void moveBullet()
    {
        rigidbody2d.velocity = transform.right * SPEED;
    }
}
