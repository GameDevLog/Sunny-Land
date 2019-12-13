using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask ground;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;

    private bool IsIdle
    {
        set => anim.SetBool("idle", value);
        get => anim.GetBool("idle");
    }

    private float Running
    {
        set => anim.SetFloat("running", value);
        get => anim.GetFloat("running");
    }

    private bool IsJumping
    {
        set => anim.SetBool("jumping", value);
        get => anim.GetBool("jumping");
    }

    private bool IsFalling
    {
        set => anim.SetBool("falling", value);
        get => anim.GetBool("falling");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();

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
        }

        this.Running = Math.Abs(faceDirection);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            this.IsJumping = true;
        }
    }

    private void SwitchAnim()
    {
        this.IsIdle = false;

        if (this.IsJumping)
        {
            if (rb.velocity.y < 0)
            {
                this.IsJumping = false;
                this.IsFalling = true; 
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            this.IsFalling = false;
            this.IsIdle = true;
        }
    }
}
