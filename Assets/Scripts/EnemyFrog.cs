using System;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed;
    public float jumpForce;

    [Space]
    public LayerMask ground;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private bool faceLeft = true;
    private float leftX;
    private float rightX;

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
    
    public void JumpOn()
    {
        anim.SetTrigger("death");
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        transform.DetachChildren();
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

        if (moveSpeed <= 0.1)
        {
            moveSpeed = 5;
        }

        if (jumpForce <= 0.1)
        {
            jumpForce = 5;
        }
    }

    void Update()
    {
        SwitchAnim();
    }

    private void Movement()
    {
        if (faceLeft)
        {
            if (coll.IsTouchingLayers(ground))
            {
                IsJumping = true;
                rb.velocity = new Vector2(-moveSpeed, jumpForce);
            }

            if (transform.position.x < leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(ground))
            {
                IsJumping = true;
                rb.velocity = new Vector2(moveSpeed, jumpForce);
            }

            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }

    private void SwitchAnim()
    {
        if (IsJumping)
        {
            if (rb.velocity.y < 0)
            {
                IsJumping = false;
                IsFalling = true;
            }
        }
        else if (coll.IsTouchingLayers(ground) && IsFalling)
        {
            IsFalling = false;
        }
    }
}
