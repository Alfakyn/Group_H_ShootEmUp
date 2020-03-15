using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeParallax : MonoBehaviour
{
    public float scrollSpeed;
    public int movement;
    Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            movement++;
            float newPos = Mathf.Repeat(movement * scrollSpeed, 16);
            transform.position = startPos + Vector2.left * newPos;
    }
}