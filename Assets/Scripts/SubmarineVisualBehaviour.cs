using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineVisualBehaviour : MonoBehaviour
{
    public SpriteRenderer sprite;
    float flash_interval = 0.2f;
    float flash_timer = 0;
    int flash_count = 0;
    const int MAX_FLASH_COUNT = 6;

    const float INVINCIBILITY_FRAMES = 2;
    float invincibility_timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (invincibility_timer > 0)
        {
            invincibility_timer -= Time.deltaTime;
        }
        if (flash_timer > flash_interval && flash_count > 0)
        {
            if (flash_count % 2 != 1)
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.white;
            }
            flash_timer = 0;
            flash_count--;
        }
        else
        {
            flash_timer += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        while (invincibility_timer <= 0)
        {
            if (collision.tag == "Enemy")
            {
                flash_count = 6;
                invincibility_timer = INVINCIBILITY_FRAMES;
            }
        }
    }
}
