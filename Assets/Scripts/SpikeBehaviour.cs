using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    const float SPEED = 5f;
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
        moveSpike();
    }

    void moveSpike()
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

