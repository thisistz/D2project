﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool b_spawn;
    float horizontalDirection;
    public float maxSpeedX = 5.0f;
    float maxSpeedY;
    public float speedX;
    public float speedY;
    [Range(400, 10000)]
    public float xForce;
    [Range(0, 200)]
    public float yForce;
    Rigidbody2D rb;
    [Range(0, 0.5f)]
    [Header("感應地板距離")]
    public float distance;
    [Header("偵測地板射線起點")]
    public Transform groundCheck;
    public Transform groundCheck2;
    public LayerMask groundLayer;
    public bool grounded;
    bool Isground
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 start2 = groundCheck2.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Vector2 end2 = new Vector2(start2.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            Debug.DrawLine(start2, end2, Color.blue);
            if (Physics2D.Linecast(start, end, groundLayer) || Physics2D.Linecast(start2, end2, groundLayer))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
            return grounded;
        }
    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void jump()
    {
        if (Isground && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * yForce, ForceMode2D.Impulse);
        }
    }

    void MovementX()
    {

        horizontalDirection = Input.GetAxis("Horizontal");
        rb.AddForce(new Vector2(horizontalDirection * xForce * Time.deltaTime,0),ForceMode2D.Force);

    }
    public void ControlSpeed()
    {
        speedX = rb.velocity.x;
        speedY = rb.velocity.y;
        float newspeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        rb.velocity = new Vector2(newspeedX, speedY);

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, speedY);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //move control
        MovementX();
        ControlSpeed();
        jump();


    }
}
