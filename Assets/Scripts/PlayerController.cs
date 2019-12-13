using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask ground;
    public int cherry;
    public Text cherryNum;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    private bool isHurt;

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

    private bool IsHurt
    {
        set => anim.SetBool("hurt", value);
        get => anim.GetBool("hurt");
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
        if (!isHurt)
        {
            Movement();
        }

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

        Running = Math.Abs(faceDirection);

        // Jump
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            IsJumping = true;
        }
    }

    private void SwitchAnim()
    {
        IsIdle = false;

        if (IsJumping)
        {
            if (rb.velocity.y < 0)
            {
                IsJumping = false;
                IsFalling = true;
            }
        }
        else if (isHurt)
        {
            IsHurt = true;
            Running = 0.0f;

            if (Math.Abs(rb.velocity.x) < 0.1f)
            {
                IsHurt = false;
                isHurt = false;
                IsIdle = true;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            IsFalling = false;
            IsIdle = true;
        }
    }

    // Collection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            Destroy(collision.gameObject);
            cherry++;
            cherryNum.text = cherry.ToString();
        }
    }

    // Enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (IsFalling)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                IsJumping = true;
            }
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                isHurt = true;
            }
        }
    }
}
