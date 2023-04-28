using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    public Animator animator;
    bool facingRight = false;

    public Transform startPoint;
    public Transform endPoint;
    private Vector3 targetPos;

    private Vector2 startingPosition;
    private bool shouldMoveRight = true;
    public float moveDistance = 1.5f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;

        if(isInChaseRange && isInAttackRange)
        {
            animator.SetBool("canBeAttacked", true);
        }
        else
        {
            animator.SetBool("canBeAttacked", false);

            if(shouldMoveRight)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                if(transform.position.x >= startingPosition.x + moveDistance)
                {
                    shouldMoveRight = false;
                    
                    if (facingRight)
                    {
                        Flip();
                    }
                }
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                if(transform.position.x <= startingPosition.x - moveDistance)
                {
                    shouldMoveRight = true;
                    
                    if (!facingRight)
                    {
                        Flip();
                    }
                }
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.enemiesPaused) return;
        
        if(isInChaseRange && isInAttackRange)
        {
            MoveCharacter(movement);
            if (dir.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (dir.x < 0 && facingRight)
            {
                Flip();
            }
        }

        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
