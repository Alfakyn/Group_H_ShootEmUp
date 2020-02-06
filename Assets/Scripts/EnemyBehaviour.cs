using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    Vector3 horizontal_speed;
    const float SPEED = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        horizontal_speed.Set(SPEED, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
    }

    void moveEnemy()
    {
        transform.position -= horizontal_speed;
    }
}
