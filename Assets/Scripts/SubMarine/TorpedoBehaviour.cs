using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    const float SPEED = 10.0f;

    public GameObject explosion;

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
    void destroyTorpedo()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            rigidbody2d.velocity = transform.right * 0;
        }        
    }
}
