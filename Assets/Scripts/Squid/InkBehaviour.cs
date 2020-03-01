using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    public float speed;

    private void FixedUpdate()
    {
        moveInk();
    }

    void moveInk()
    {
        rigidbody2d.velocity = transform.right * -speed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag=="Camera Collider")
        {
            Destroy(gameObject);
        }
    }
}
