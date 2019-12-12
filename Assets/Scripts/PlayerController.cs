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
        // -1 ~ 1
        float horizontalMove = Input.GetAxis("Horizontal");
        // [-1, 0, 1]
        float faceDirection = Input.GetAxisRaw("Horizontal");
        float EPSILON = 0.1f;

        if (Math.Abs(horizontalMove) > EPSILON)
        {
            rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        }

        if ((Math.Abs(faceDirection) > EPSILON))
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
    }
}
