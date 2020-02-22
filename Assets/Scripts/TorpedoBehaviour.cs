using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    const float SPEED = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        moveTorpedo();
    }

    void moveTorpedo()
    {
        rigidbody2d.velocity = transform.right * SPEED;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Camera Collider")
        {
            Destroy(gameObject);
        }
    }
}
