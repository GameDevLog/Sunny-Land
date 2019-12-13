using UnityEngine;

public class EnemyEagle : Enemy
{
    public Transform topPoint;
    public Transform bottomPoint;
    public float moveSpeed;

    private Rigidbody2D rb;
    private Collider2D coll;
    private bool isUp = true;
    private float topY;
    private float bottomY;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        topY = topPoint.position.y;
        bottomY = bottomPoint.position.y;
        Destroy(topPoint.gameObject);
        Destroy(bottomPoint.gameObject);

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
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
                
            if (transform.position.y > topY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
            
            if (transform.position.y < bottomY)
            {
                isUp = true;
            }
        }
    }
}
