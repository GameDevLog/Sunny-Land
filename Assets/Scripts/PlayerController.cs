using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 15;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (moveSpeed <= 0)
        {
            moveSpeed = 15;
        }
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float EPSILON = 0.1f;

        if (Math.Abs(horizontalMove) > EPSILON)
        {
            rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        }
    }
}
