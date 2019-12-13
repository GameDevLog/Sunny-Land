using System;
using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed;

    private Rigidbody2D rb;
    private bool faceLeft = true;
    private float leftX;
    private float rightX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);

        transform.DetachChildren();

        if (moveSpeed <= 0.1)
        {
            moveSpeed = 5;
        }
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (transform.position.x < leftX)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            if (transform.position.x > rightX)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;
            }
        }
    }
}
