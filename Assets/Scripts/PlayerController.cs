using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (moveSpeed <= 0)
        {
            moveSpeed = 400;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 400;
        }
    }

    private void FixedUpdate()
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

        // Move
        if (Math.Abs(horizontalMove) > EPSILON)
        {
            rb.velocity = new Vector2(horizontalMove * moveSpeed * Time.deltaTime, rb.velocity.y);
        }

        if ((Math.Abs(faceDirection) > EPSILON))
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
            anim.SetFloat("running", Math.Abs(faceDirection));
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
        }
    }
}
