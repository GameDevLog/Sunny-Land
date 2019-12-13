using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Animator anim;

    private Boolean IsIdle
    {
        set => anim.SetBool("idle", value);
        get => anim.GetBool("idle");
    }

    private Boolean IsRunning
    {
        set => anim.SetBool("running", value);
        get => anim.GetBool("running");
    }

    private Boolean IsJumping
    {
        set => anim.SetBool("jumping", value);
        get => anim.GetBool("jumping");
    }

    private Boolean IsFalling
    {
        set => anim.SetBool("falling", value);
        get => anim.GetBool("falling");
    }

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
        SwitchAnim();
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
            this.IsRunning = true;
        }

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            this.IsJumping = true;
        }
    }

    private void SwitchAnim()
    {
        if (this.IsJumping)
        {
            if (rb.velocity.y < 0)
            {
                this.IsJumping = false;
                this.IsFalling = true; 
            }
        }
    }
}
